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
        
        public Sphere()
        {
            
        }

        public override vec4 getIntersectionPoint(Ray ray)
        {
            vec4 direction = ray.Direction;
            vec4 position = ray.StartingPoint;
            float a = direction.x * direction.x + direction.y * direction.y + direction.z * direction.z;
            float b = 2 * position.x * direction.x + 2 * position.y * direction.y + 2 * position.z * direction.z;
            float c = position.x * position.x + position.y * position.y + position.z * position.z - 1;

            float rootValue = b * b - 4 * a * c;

            if (rootValue < 0)
                return new vec4(0, 0, 0, 0);

            float t1 = (-b + (float)Math.Sqrt(rootValue)) / (2 * a);
            float t2 = (-b - (float)Math.Sqrt(rootValue)) / (2 * a);

            if (t1 < 0 && t2 < 0)
            {
                Console.WriteLine("schnittpunkte gefunden, befinden sich hinter der Kamera");
                return new vec4(0, 0, 0, 0);
            }

            float t;
            if (t1 < 0)
                t = t2;
            else if (t2 < 0)
                t = t1;
            else if (t1 < t2)
                t = t1;
            else
                t = t2;

            return new vec4(position.x + t * direction.x, position.y + t * direction.y, position.z + t * direction.z, 1);
        }

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

        public override vec4 getNormalAt(vec4 pos)
        {
            return new vec4(2 * pos[0], 2 * pos[1], 2 * pos[2], 0);
        }
    }
}
