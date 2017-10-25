using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;

namespace SimpleRayTracer
{
    abstract class ObjectType
    {
        abstract public vec3 getNormalAt(vec3 pos);

        public vec3 getNormalAt(float x, float y, float z)
        {
            return getNormalAt(new vec3(x, y, z));
        }

        abstract public vec3 getIntersectionPoint(Ray ray);

        public vec3 getIntersectionPoint(vec3 startingPoint, vec3 direction)
        {
            return getIntersectionPoint(new Ray(startingPoint, direction));
        }

    
    
    }
}
