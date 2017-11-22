using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;

namespace SimpleRayTracer.Objekte
{
    class Triangle
    {
        private vec4 _p0;
        private vec4 _p1;
        private vec4 _p2;
        private vec4 _normal;

        MaterialProperty _material_property;

        public Triangle(vec4 p0, vec4 p1, vec4 p2, MaterialProperty materialProperty)
        {
            _p0 = p0;
            _p1 = p1;
            _p2 = p2;
            _normal = getTriangleNormal();
            _material_property = materialProperty;
        }

        public Triangle(vec4 p0, vec4 p1, vec4 p2, vec4 normal, MaterialProperty lightProps)
        {
            _p0 = p0;
            _p1 = p1;
            _p2 = p2;
            _normal = glm.normalize(normal);
            _material_property = lightProps;
        }


        private vec4 getTriangleNormal()
        {
            vec3 _d1 = new vec3(_p1 - _p0);
            vec3 _d2 = new vec3(_p2 - _p0);
            return glm.normalize(new vec4(glm.cross(_d1, _d2), 0));
        }

        public bool hasIntersectionPoint(Ray ray, out vec4 intersecPoint, out float t)
        {
            t = -1;
            intersecPoint = new vec4();

            vec3 _e1 = new vec3(_p1 - _p0);
            vec3 _e2 = new vec3(_p2 - _p0);
            vec3 _s = new vec3(ray.StartingPoint - _p0);

            float _x = glm.dot(glm.cross(_s, _e1), _e2);
            float _y = glm.dot(glm.cross(new vec3(ray.Direction), _e2), _s);
            float _z = glm.dot(glm.cross(_s, _e1), new vec3(ray.Direction));

            if (_y == 0 || _x == 0) // || _x == 0?
                return false;

            vec3 _solution = 1 / (glm.dot(glm.cross(new vec3(ray.Direction), _e2), _e1)) * new vec3(_x, _y, _z);

            if (_solution.y + _solution.z > 1)
                return false;
            if (_solution.y < 0 || _solution.z < 0)
                return false;

            t = _solution.x;
            intersecPoint = (1 - _solution.y - _solution.z) * _p0 + _solution.y * _p1 + _solution.z * _p2;
            vec4 test = Calculation.getPointOnRay(ray, t);

            return true;
        }

        public vec4 P1 { get => _p0; }
        public vec4 P2 { get => _p1; }
        public vec4 P3 { get => _p2; }
        public vec4 Normal { get => _normal; }
        public MaterialProperty LightProperties { get => _material_property; }
    }
}
