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


        public TriangleObject(int objID, mat4 transformationMatrix, string filePath, string objName, string mtlName) : base(objID, mat4.identity(), new MaterialProperty())
        {
            _triangleList = Parser.getTriangleListFromFile(filePath, objName, TransformationMatrix);
            generateHitBox();
        }

        public TriangleObject(int objID, vec3 position, string filePath, string objectName) : base(objID, position, new MaterialProperty())
        {
            _triangleList = Parser.getTriangleListFromFile(filePath, objectName, TransformationMatrix);
            generateHitBox();
        }

        public TriangleObject(int objID, vec3 position, vec3 scale, string filePath, string objectName) : base (objID, position, scale, new MaterialProperty())
        {
            _triangleList = Parser.getTriangleListFromFile(filePath, objectName, TransformationMatrix);
            generateHitBox();
        }

        public TriangleObject(int objID, vec3 position, vec3 rotationAxis, float angle, vec3 scale, string filePath, string objectName) : base(objID,position,rotationAxis,angle, scale, new MaterialProperty())
        {
            _triangleList = Parser.getTriangleListFromFile(filePath, objectName, TransformationMatrix);
            generateHitBox();
        }

        public TriangleObject(int objID, vec3 position, vec3 rotationAxis, float angle, string filePath, string objectName) : base(objID, position, rotationAxis, angle, new vec3(1,1,1), new MaterialProperty())
        {
            _triangleList = Parser.getTriangleListFromFile(filePath, objectName, TransformationMatrix);
            generateHitBox();
        }

        private TriangleObject(int objId, List<Triangle> triangles) : base(objId, mat4.identity(), new MaterialProperty())
        {
            _triangleList = triangles;
            generateHitBox();
        }

        public TriangleObject(int objID, vec3 position, vec3 scale, string filePath, string objName, string mtlName) : base(objID, position, scale, new MaterialProperty())
        {
            _triangleList = Parser.getTriangleListFromFile(filePath, objName, mtlName,TransformationMatrix);
            generateHitBox();
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
            closestT = Constants.Max_camera_distance + 1; //max Distance
            intersecPoint = new vec4();
            normal = new vec4();
            materialProperty = new MaterialProperty();

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
                        }
                    }
                }
            }
            if (closestT <= Constants.Max_camera_distance) return true;
            return false;
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
        
        internal override void moveObjectInDirection(vec3 direction)
        {
            base.moveObjectInDirection(direction);
            updateTriangles();
        }

        internal override void scaleObject(vec3 scaling)
        {
            base.scaleObject(scaling);
            updateTriangles();
        }

        internal override void rotateAroundAxisWithAngle(vec3 rotationAxis, float angle)
        {
            TransformationMatrix = Calculation.calculateTransformationMatrix(new vec3(0, 0, 0), Calculation.calculateRotationMatrix(rotationAxis, angle), new vec3(1, 1, 1));
            updateTriangles();
        }

        private void updateTriangles()
        {
            foreach(Triangle t in _triangleList)
            {
                t.updateTriangle(TransformationMatrix);
            }
            generateHitBox();
        }

        public static TriangleObject mergeTriangleObjects(int objId, params TriangleObject[] list)
        {
            List<Triangle> _triangles = new List<Triangle>();
            foreach (var _to in list)
                foreach (var triangle in _to._triangleList)
                    _triangles.Add(triangle);

            return new TriangleObject(objId, _triangles);
        }
    }
}
