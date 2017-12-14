using GlmNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRayTracer.Objekte
{
    class AxisAlignedBoundingBox
    {
        private List<int> objIds = new List<int>();
        private vec3 lowerLeftPoint = new vec3();
        private vec3 upperRightPoint = new vec3();

        public AxisAlignedBoundingBox(params ObjectType[] objTypes)
        {
            saveObjIds(objTypes);
            generateHitBox(objTypes);
        }

        private void saveObjIds(ObjectType[] objTypes)
        {
            foreach (var _o in objTypes)
                objIds.Add(_o.IdNumber);
        }

        private void generateHitBox(ObjectType[] objTypes)
        {
            vec3 _currentLowerLeft = new vec3();
            vec3 _currentUpperRight = new vec3();
            bool first = true;

            foreach (var _o in objTypes)
            {
                var _triangleObj = (TriangleObject)_o;
                if (first)
                {
                    _currentLowerLeft = _triangleObj.LowerLeftPoint;
                    _currentUpperRight = _triangleObj.UpperRightPoint;
                    first = false;
                    continue;
                }
                _currentLowerLeft.x = (_currentLowerLeft.x < _triangleObj.LowerLeftPoint.x) ? _currentLowerLeft.x : _triangleObj.LowerLeftPoint.x;
                _currentLowerLeft.y = (_currentLowerLeft.y < _triangleObj.LowerLeftPoint.y) ? _currentLowerLeft.y : _triangleObj.LowerLeftPoint.y;
                _currentLowerLeft.z = (_currentLowerLeft.z < _triangleObj.LowerLeftPoint.z) ? _currentLowerLeft.z : _triangleObj.LowerLeftPoint.z;
                _currentUpperRight.x = (_currentUpperRight.x < _triangleObj.UpperRightPoint.x) ? _currentUpperRight.x : _triangleObj.UpperRightPoint.x;
                _currentUpperRight.x = (_currentUpperRight.y < _triangleObj.UpperRightPoint.y) ? _currentUpperRight.y : _triangleObj.UpperRightPoint.y;
                _currentUpperRight.x = (_currentUpperRight.z < _triangleObj.UpperRightPoint.z) ? _currentUpperRight.z : _triangleObj.UpperRightPoint.z;
            }

            upperRightPoint = _currentUpperRight;
            lowerLeftPoint = _currentLowerLeft;
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

        internal List<int> objIDs { get => objIds; }

    }
}
