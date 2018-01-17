using System;
using System.Collections.Generic;
using GlmNet;

namespace SimpleRayTracer.Objekte
{
    class TriangleObject : ObjectType
    {
        
        List<Triangle> _triangleList = new List<Triangle>();

        vec4 upperRightPoint;
        vec4 lowerLeftPoint;

        /**
         * Visual representation of the corners 
         * in an AABB
         * 
         *       7------8
         *      /|     /| 
         *     3-+----4 |
         *     | 5----|-6
         *     |/     | /
         *     1------2/
         * 
        **/

        private vec4 corner1 = new vec4();
        private vec4 corner2 = new vec4();
        private vec4 corner3 = new vec4();
        private vec4 corner4 = new vec4();
        private vec4 corner5 = new vec4();
        private vec4 corner6 = new vec4();
        private vec4 corner7 = new vec4();
        private vec4 corner8 = new vec4();


        public TriangleObject(int objID, mat4 transformationMatrix, List<Triangle> triangleList) : base(objID, transformationMatrix, new MaterialProperty())
        {
            copyTriangleList(triangleList);
            generateHitBox();
        }
        
        public TriangleObject(int objID, vec4 position, List<Triangle> triangleList) : base(objID, position, new MaterialProperty())
        {
            copyTriangleList(triangleList);
            generateHitBox();
        }

        private void copyTriangleList(List<Triangle> triangleList)
        {
            foreach (Triangle t in triangleList)
            {
                var _t = new Triangle(t.P0, t.P1, t.P2, t.Normal, t.MaterialProperty);
                _triangleList.Add(_t);
            }
                
        }

        internal void generateHitBox()
        {
            vec4 _vector = _triangleList[0].P0;
            float _lowerX = _vector.x;
            float _lowerY = _vector.y;
            float _lowerZ = _vector.z;
            float _upperX = _lowerX;
            float _upperY = _lowerY;
            float _upperZ = _lowerZ;

            foreach (Triangle t in _triangleList)
            {
                _lowerX = getSmallestValue(_lowerX, t.P0.x, t.P1.x, t.P2.x);
                _lowerY = getSmallestValue(_lowerY, t.P0.y, t.P1.y, t.P2.y);
                _lowerZ = getSmallestValue(_lowerZ, t.P1.z, t.P1.z, t.P2.z);

                _upperX = -1 * getSmallestValue(-1 * _upperX, -1 * t.P0.x, -1 * t.P1.x, -1 * t.P2.x);
                _upperY = -1 * getSmallestValue(-1 * _upperY, -1 * t.P0.y, -1 * t.P1.y, -1 * t.P2.y);
                _upperZ = -1 * getSmallestValue(-1 * _upperZ, -1 * t.P0.z, -1 * t.P1.z, -1 * t.P2.z);
            }

            upperRightPoint = new vec4(_upperX + Constants.Epsilon, _upperY + Constants.Epsilon, _upperZ + Constants.Epsilon, 1);
            lowerLeftPoint = new vec4(_lowerX - Constants.Epsilon, _lowerY - Constants.Epsilon, _lowerZ - Constants.Epsilon, 1);

            setCornerValues();
        }

        private void setCornerValues()
        {
            corner1 = new vec4(lowerLeftPoint.x, lowerLeftPoint.y, upperRightPoint.z, 1);
            corner2 = new vec4(upperRightPoint.x, lowerLeftPoint.y, upperRightPoint.z, 1);
            corner3 = new vec4(lowerLeftPoint.x, upperRightPoint.y, upperRightPoint.z, 1);
            corner4 = upperRightPoint;
            corner5 = lowerLeftPoint;
            corner6 = new vec4(upperRightPoint.x, lowerLeftPoint.y, lowerLeftPoint.z, 1);
            corner7 = new vec4(lowerLeftPoint.x, upperRightPoint.y, lowerLeftPoint.z, 1);
            corner8 = new vec4(upperRightPoint.x, upperRightPoint.y, lowerLeftPoint.z, 1);
        }

        private float getSmallestValue(float x, float y, float z, float w)
        {
            return Math.Min(Math.Min(x, y), Math.Min(z, w));
        }

        
        public override bool hasIntersectionPoint(Ray ray, out vec4 intersecPoint, out vec4 normal, out float closestT, out MaterialProperty materialProperty)
        {
            closestT = Constants.Max_camera_distance;
            intersecPoint = new vec4();
            normal = new vec4();
            materialProperty = new MaterialProperty();
            bool foundValue = false;
           
            var _ray = ray.transformRay(Inverse);
            
            if (hitAABB(_ray))
            {
                foreach (Triangle triangle in _triangleList)
                {
                    float t;
                    vec4 _current_intersecPoint;

                    if (triangle.hasIntersectionPoint(_ray, out _current_intersecPoint, out t))
                    {
                        if (t > 0 && t < closestT)
                        {
                            materialProperty = triangle.MaterialProperty;
                            normal = triangle.Normal;
                            closestT = t;
                            intersecPoint = _current_intersecPoint;
                            foundValue = true;
                        }
                    }
                }
            }
            intersecPoint = TransformationMatrix * intersecPoint;
            normal = glm.normalize(TransformationMatrix *  normal);
            return foundValue;
        }

        private bool hitAABB(Ray ray)
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

        internal override bool hasAnyIntersectionPoint(Ray ray)
        {
            var _ray = ray.transformRay(Inverse);
            var distance = _ray.Length - Constants.Epsilon;
            _ray.normalize();

            if (!hitAABB(_ray)) return false;
            foreach(Triangle _triangle in _triangleList)
            {
                float t;
                vec4 intersec;
                if (!_triangle.hasIntersectionPoint(_ray, out intersec, out t)) continue;
                if (t > 0 && t < distance) return true;
            }
            return false;
        }

        public override string ToString()
        {
            return "TriangleObject";
        }

        internal vec4 UpperRightPoint { get => upperRightPoint; }
        internal vec4 LowerLeftPoint { get => lowerLeftPoint; }
        internal vec4 Corner1 { get => corner1; }
        internal vec4 Corner2 { get => corner2; }
        internal vec4 Corner3 { get => corner3; }
        internal vec4 Corner4 { get => corner4; }
        internal vec4 Corner5 { get => corner5; }
        internal vec4 Corner6 { get => corner6; }
        internal vec4 Corner7 { get => corner7; }
        internal vec4 Corner8 { get => corner8; }

    }
}
