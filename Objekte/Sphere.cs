using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;

namespace SimpleRayTracer.Objekte
{
    class Sphere : ObjectType
    {
        public Sphere(int idNumber, mat4 transformationMatrix) : base(idNumber, transformationMatrix) { }

        public Sphere(int idNumber, vec3 position ) : base(idNumber, position) { }

        public Sphere(int idNumber, vec3 position, vec3 scale) : base(idNumber, position, scale) { }

        public Sphere(int idNumber, vec3 position, mat3 rotation, vec3 scale ) : base(idNumber, position, rotation, scale) { }

        public Sphere(int idNumber, vec3 position, vec3 rotationAxis, float angle, vec3 scale) : base(idNumber, position, rotationAxis, angle, scale) { }

        public override bool hasIntersectionPoint(Ray ray)
        {
            vec4 direction = ray.Direction;
            vec4 position = ray.StartingPoint;
            float a = direction.x * direction.x + direction.y * direction.y + direction.z * direction.z;
            float b = 2 * position.x * direction.x + 2 * position.y * direction.y + 2 * position.z * direction.z;
            float c = position.x * position.x + position.y * position.y + position.z * position.z - 1;
            float rootValue = b * b - 4 * a * c;
            if (rootValue < 0)
                return false;
            return true;
        }

        public override float getIntersectionParameter(Ray ray)
        {
            vec4 direction = ray.Direction;
            vec4 position = ray.StartingPoint;
            float a = direction.x * direction.x + direction.y * direction.y + direction.z * direction.z;
            float b = 2 * position.x * direction.x + 2 * position.y * direction.y + 2 * position.z * direction.z;
            float c = position.x * position.x + position.y * position.y + position.z * position.z - 1;

            float rootValue = b * b - 4 * a * c;

            if (rootValue < 0)
                return -1;

            float t1 = (-b + (float)Math.Sqrt(rootValue)) / (2 * a);
            float t2 = (-b - (float)Math.Sqrt(rootValue)) / (2 * a);

            float t;
            if (t1 < 0)
                t = t2;
            else if (t2 < 0)
                t = t1;
            else if (t1 < t2)
                t = t1;
            else
                t = t2;
            return t;
        }

        public override vec4 getNormalAt(vec4 pos)
        {
            return new vec4(2 * pos[0], 2 * pos[1], 2 * pos[2], 0);
        }
    }
}
