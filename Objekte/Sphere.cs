using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;
using System.Drawing;

namespace SimpleRayTracer.Objekte
{
    class Sphere : ObjectType
    {
        public Sphere(int idNumber, mat4 transformationMatrix, LightProperties objectLight) : base(idNumber, transformationMatrix, objectLight) { }

        public Sphere(int idNumber, vec3 position, LightProperties objectLight) : base(idNumber, position, objectLight) { }

        public Sphere(int idNumber, vec3 position, vec3 scale, LightProperties objectLight) : base(idNumber, position, scale, objectLight) { }

        public Sphere(int idNumber, vec3 position, mat3 rotation, vec3 scale, LightProperties objectLight) : base(idNumber, position, rotation, scale, objectLight) { }

        public Sphere(int idNumber, vec3 position, vec3 rotationAxis, float angle, vec3 scale, LightProperties objectLight) : base(idNumber, position, rotationAxis, angle, scale, objectLight) { }

        public override bool hasIntersectionPoint(Ray ray, out vec4 intersectionPoint, out float t)
        {
            mat4 _trans = glm.inverse(TransformationMatrix);
            vec4 _pos = _trans * ray.StartingPoint;
            vec4 _end = _trans * ray.EndPoint;
            vec4 _dir = glm.normalize(_end - _pos);
                        
            intersectionPoint = new vec4();
            t = -1;

            float _a = _dir.x * _dir.x + _dir.y * _dir.y + _dir.z * _dir.z;
            float _b = 2 * _pos.x * _dir.x + 2 * _pos.y * _dir.y + 2 * _pos.z * _dir.z;
            float _c = _pos.x * _pos.x + _pos.y * _pos.y + _pos.z * _pos.z - 1;
            float _root = _b * _b - 4 * _a * _c;

            if (_root < 0)
                return false;

            float _t1 = (-_b + (float)Math.Sqrt(_root)) / (2 * _a);
            float _t2 = (-_b - (float)Math.Sqrt(_root)) / (2 * _a);

            if (_t1 < 0)
                t = _t2;
            else if (_t2 < 0)
                t = _t1;
            else if (_t1 < _t2)
                t = _t1;
            else
                t = _t2;

            intersectionPoint = Calculation.getPointOnRay(new Ray(_pos, _dir), t);
            return true;
        }

        public override vec4 getNormalAt(vec4 pos)
        {
            return new vec4(2 * pos[0], 2 * pos[1], 2 * pos[2], 0);
        }
    }
}
