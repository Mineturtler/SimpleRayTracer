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
        abstract public vec4 getNormalAt(vec4 pos);

        public vec4 getNormalAt(vec3 pos)
        {
            return getNormalAt(new vec4(pos, 1));
        }

        public vec4 getNormalAt(float x, float y, float z)
        {
            return getNormalAt(new vec4(x, y, z, 0));
        }

        public vec4 getNormalisedNormalAt(vec4 pos)
        {
            return glm.normalize(getNormalAt(pos));
        }

        public vec4 getNormalisedNormalAt(vec3 pos)
        {
            return glm.normalize(getNormalAt(pos));
        }

        public vec4 getNormalisedNormalAt(float x, float y, float z)
        {
            return glm.normalize(getNormalAt(x, y, z));
        }

        abstract public vec4 getIntersectionPoint(Ray ray);

        public vec4 getIntersectionPoint(vec4 startingPoint, vec4 direction)
        {
            return getIntersectionPoint(new Ray(startingPoint, direction));
        }

        public vec4 getIntersectionPoint(vec3 startingPoint, vec3 direction)
        {
            return getIntersectionPoint(new vec4(startingPoint, 1), new vec4(direction, 0));
        }

        abstract public bool hasIntersectionPoint(Ray ray);

        public bool hasIntersectionPoint(vec4 startingPoint, vec4 direction)
        {
            return hasIntersectionPoint(new Ray(startingPoint, direction));
        }
    }
}
