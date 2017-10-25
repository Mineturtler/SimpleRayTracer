using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;

namespace SimpleRayTracer
{
    class Ray
    {
        private vec3 startingPoint;
        private vec3 direction;

        public Ray(vec3 start, vec3 direction)
        {
            this.startingPoint = start;
            this.direction = direction;
        }

        public vec3 StartingPoint
        {
            get { return startingPoint; }
        }

        public vec3 Direction
        {
            get { return direction; }
        }
    }
}
