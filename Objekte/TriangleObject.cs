using System;
using System.Collections.Generic;
using GlmNet;

namespace SimpleRayTracer.Objekte
{
    class TriangleObject : ObjectType
    {
        
        List<Triangle> _triangleList = new List<Triangle>();
        List<MaterialProperty> _materialList = new List<MaterialProperty>();

        vec4 upperRightPoint;
        vec4 lowerLeftPoint;


        public TriangleObject(int objID, mat4 transformationMatrix, List<Triangle> triangleList) : base(objID, transformationMatrix, new MaterialProperty())
        {
            copyTriangleList(triangleList);
            updateTriangles();
            generateHitBox();
        }

        /**/
        public TriangleObject(int objID, vec3 position, List<Triangle> triangleList) : base(objID, position, new MaterialProperty())
        {
            copyTriangleList(triangleList);
            updateTriangles();
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

       
        private void generateHitBox()
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
        }

        private static float getSmallestValue(float x, float y, float z, float w)
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

            if (hitAABB(ray))
            {
                foreach (Triangle triangle in _triangleList)
                {
                    float t;
                    vec4 _current_intersecPoint;

                    if (triangle.hasIntersectionPoint(ray, out _current_intersecPoint, out t))
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
            if (hitAABB(ray))
            {
                foreach (Triangle triangle in _triangleList)
                {
                    float t;
                    vec4 _current_intersecPoint;

                    if (triangle.hasIntersectionPoint(ray, out _current_intersecPoint, out t))
                        if (t < (1+Constants.Epsilon) && t > Constants.Epsilon)
                            return true;                    
                }
            }
            return false;
        }
        
        private void updateTriangles()
        {
            foreach(Triangle t in _triangleList)
            {
                t.updateTriangle(TransformationMatrix);
            }
            generateHitBox();
        }

        internal vec3 UpperRightPoint { get => new vec3(upperRightPoint); }
        internal vec3 LowerLeftPoint { get => new vec3(lowerLeftPoint); }
    }
}
