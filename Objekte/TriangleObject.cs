using System;
using System.Collections.Generic;
using GlmNet;

namespace SimpleRayTracer.Objekte
{
    class TriangleObject : ObjectType
    {
        
        List<Triangle> _triangleList = new List<Triangle>();
        List<MaterialProperty> _materialList = new List<MaterialProperty>();

        mat4 _transformation;
        vec4 upperRightPoint;
        vec4 lowerLeftPoint;


        public TriangleObject(int objID, mat4 transformationMatrix, string filePath, string objectName) : base(objID, mat4.identity(), new MaterialProperty())
        {
            _transformation = transformationMatrix;
            _triangleList = Parser.getTriangleListFromFile(filePath, objectName);
            generateHitBox();
        }

        public TriangleObject(int objID, Triangle triangle) : base(objID, mat4.identity(), new MaterialProperty())
        {
            _triangleList.Add(triangle);
            _transformation = mat4.identity();
            generateHitBox();
        }

        private void generateHitBox()
        {
            vec4 _vector = _triangleList[0].P1;
            float _lowerX = _vector.x;
            float _lowerY = _vector.y;
            float _lowerZ = _vector.z;
            float _upperX = _lowerX;
            float _upperY = _lowerY;
            float _upperZ = _lowerZ;

            foreach (Triangle t in _triangleList)
            {
                _lowerX = getSmallestValue(_lowerX, t.P1.x, t.P2.x, t.P3.x);
                _lowerY = getSmallestValue(_lowerY, t.P1.y, t.P2.y, t.P3.y);
                _lowerZ = getSmallestValue(_lowerZ, t.P2.z, t.P2.z, t.P3.z);

                _upperX = -1 * getSmallestValue(-1 * _upperX, -1 * t.P1.x, -1 * t.P2.x, -1 * t.P3.x);
                _upperY = -1 * getSmallestValue(-1 * _upperY, -1 * t.P1.y, -1 * t.P2.y, -1 * t.P3.y);
                _upperZ = -1 * getSmallestValue(-1 * _upperZ, -1 * t.P1.z, -1 * t.P2.z, -1 * t.P3.z);
            }

            upperRightPoint = new vec4(_upperX, _upperY, _upperZ, 1);
            lowerLeftPoint = new vec4(_lowerX, _lowerY, _lowerZ, 1);
        }

        private static float getSmallestValue(float x, float y, float z, float w)
        {
            return Math.Min(Math.Min(x, y), Math.Min(z, w));
        }

        
        public override bool hasIntersectionPoint(Ray ray, out vec4 intersecPoint, out vec4 normal, out float closestT, out MaterialProperty materialProperty)
        {
            closestT = Constants.Max_camera_distance; //max Distance
            intersecPoint = new vec4();
            normal = new vec4();
            materialProperty = new MaterialProperty();

            if (hitAABB(ray))
            {
                foreach (Triangle triangle in _triangleList)
                {
                    float t;

                    if (triangle.hasIntersectionPoint(ray, out intersecPoint, out t))
                    {
                        if (t > 0 && t < closestT)
                        {
                            materialProperty = triangle.LightProperties;
                            normal = triangle.Normal;
                            closestT = t;
                        }
                    }
                }
            }
            if (closestT != Constants.Max_camera_distance) return true;
            return false;
        }

        private bool hitAABB(Ray ray)
        {
            if (_triangleList.Count <= 10000) return true;

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

            if (_t_max < 0)
                return false;
            if (_t_min > _t_max)
                return false;
            return true;
        }

        public static bool testHitBox(Ray ray, vec3 upperRightPoint, vec3 lowerLeftPoint)
        {
            vec4 _dir = glm.normalize(ray.Direction);
            vec4 _origin = ray.StartingPoint;

            float _tx_min = (lowerLeftPoint.x - _origin.x) / _dir.x;
            float _tx_max = (upperRightPoint.x - _origin.x) / _dir.x;
            float _ty_min = (lowerLeftPoint.y - _origin.y) / _dir.y;
            float _ty_max = (upperRightPoint.y - _origin.y) / _dir.y;
            float _tz_min = (lowerLeftPoint.z - _origin.z) / _dir.z;
            float _tz_max = (upperRightPoint.z - _origin.z) / _dir.z;

            float _t_max = Math.Max(Math.Max(Math.Min(_tx_min, _tx_max), Math.Min(_ty_min, _ty_max)), Math.Min(_tz_min, _tz_max));
            float _t_min = Math.Min(Math.Min(Math.Max(_tx_min, _tx_max), Math.Max(_ty_min, _ty_max)), Math.Max(_tz_min, _tz_max));

            bool _one = _tx_min < _ty_max;
            bool _two = _tx_min < _tz_max;
            bool _three = _ty_min < _tx_max;
            bool _four = _ty_min < _tz_max;
            bool _five = _tz_min < _tx_max;
            bool _six = _tz_min < _ty_max;


            /*
            if (_one && _two)
                return true;
            if (_three && _four)
                return true;
            if (_five && _six)
                return true;
            if (_one && _two && _three && _four && _five && _six)
                return true;

            return false;
            */
            if (_t_max < 0)
                return false;
            if (_t_min > _t_max)
                return false;
            return true;
        }
    }
}
