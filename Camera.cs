using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;

namespace SimpleRayTracer
{
    class Camera
    {
        private vec3 position;
        private vec3 orientation;
        private mat4 transformation;

        public Camera(vec3 position, vec3 orientation)
        {
            vec3 up = new vec3(0,1,0);     //up definert "oben" in der Welt
            this.position = position;
            this.orientation = orientation;
            this.transformation = glm.lookAt(position, up, orientation);
        }

        public mat4 Transformation
        {
            get { return transformation; }
        }
    }
}
