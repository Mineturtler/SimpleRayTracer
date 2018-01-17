using GlmNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRayTracer.Objekte
{
    class AABB
    {
        private List<ObjectType> elementList = new List<ObjectType>();
        private vec4 lowerLeftPoint = new vec4();
        private vec4 upperRightPoint = new vec4();


        public AABB(params ObjectType[] objTypes)
        {
            elementList.AddRange(objTypes);
            generateHitBox();
        }

        internal void generateHitBox()
        {
            vec4 currentLowerLeft = new vec4();
            vec4 currentUpperRight = new vec4();
            var first = true;
            foreach(var _o in elementList)
            {
                if (!_o.ToString().Equals("TriangleObject")) continue;
                var _triangleObj = (TriangleObject)_o;
                
                var corner1 = _o.TransformationMatrix * _triangleObj.Corner1;
                var corner2 = _o.TransformationMatrix * _triangleObj.Corner2;
                var corner3 = _o.TransformationMatrix * _triangleObj.Corner3;
                var corner4 = _o.TransformationMatrix * _triangleObj.Corner4;
                var corner5 = _o.TransformationMatrix * _triangleObj.Corner5;
                var corner6 = _o.TransformationMatrix * _triangleObj.Corner6;
                var corner7 = _o.TransformationMatrix * _triangleObj.Corner7;
                var corner8 = _o.TransformationMatrix * _triangleObj.Corner8;

                if (first)
                {
                    currentLowerLeft = Calculation.getLowestCombination(corner1, corner2, corner3, corner4, corner5, corner6, corner7, corner8);
                    currentUpperRight = Calculation.getHightestCombination(corner1, corner2, corner3, corner4, corner5, corner6, corner7, corner8);
                    first = false;
                    continue;
                }
                currentLowerLeft = Calculation.getLowestCombination(currentLowerLeft, corner1, corner2, corner3, corner4, corner5, corner6, corner7, corner8);
                currentUpperRight = Calculation.getHightestCombination(currentUpperRight, corner1, corner2, corner3, corner4, corner5, corner6, corner7, corner8);
            }
            lowerLeftPoint = currentLowerLeft;
            upperRightPoint = currentUpperRight;
        }
        
        private void updateHitBox()
        {
            generateHitBox();
        }

        internal void transformAABB(mat4 transformationMatrix)
        {
            foreach(var _o in elementList)
            {
                _o.updateObject(transformationMatrix);
            }
            updateHitBox();
        }


        internal bool hitAABB(Ray ray)
        {
            vec4 _dir = ray.Direction;
            vec4 _origin = ray.StartingPoint;

            float _tx_min = (lowerLeftPoint.x - _origin.x) / _dir.x;
            float _tx_max = (upperRightPoint.x - _origin.x) / _dir.x;
            float _ty_min = (lowerLeftPoint.y - _origin.y) / _dir.y;
            float _ty_max = (upperRightPoint.y - _origin.y) / _dir.y;
            float _tz_min = (lowerLeftPoint.z - _origin.z) / _dir.z;
            float _tz_max = (upperRightPoint.z - _origin.z) / _dir.z;

            float _t_max = Math.Max(Math.Max(Math.Min(_tx_min, _tx_max), Math.Min(_ty_min, _ty_max)), Math.Min(_tz_min, _tz_max));
            float _t_min = Math.Min(Math.Min(Math.Max(_tx_min, _tx_max), Math.Max(_ty_min, _ty_max)), Math.Max(_tz_min, _tz_max));

            //Warum genau anders rum?
            if (_t_max < 0)
                return true;
            if (_t_min > _t_max)
                return true;

            return false;
        }
        
        internal List<ObjectType> ElementList { get => elementList; }

    }
}
