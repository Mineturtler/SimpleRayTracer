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
        private vec4 startingPoint;
        private vec4 direction;

        /// <summary>
        /// Constructs a Ray from a startingPoint and given direction
        /// </summary>
        /// <param name="start"></param>
        /// <param name="direction"></param>
        public Ray(vec4 start, vec4 direction)
        {
            this.startingPoint = start;
            this.direction = direction;
        }
        
        /// <summary>
        /// Constructs a Ray from a starting Point and a given Endpoint
        /// </summary>
        /// <param name="startingPoint"></param>
        /// <param name="endPoint"></param>
        public Ray(vec3 startingPoint, vec3 endPoint)
        {
            new Ray(new vec4(startingPoint, 1), glm.normalize(new vec4(endPoint[0] - startingPoint[0], 
                                                                       endPoint[1] - startingPoint[1], 
                                                                       endPoint[2] - startingPoint[2], 0)));
        }

        public vec4 StartingPoint
        {
            get { return startingPoint; }
        }

        public vec4 Direction
        {
            get { return direction; }
        }
    }
}
