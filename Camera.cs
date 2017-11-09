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
        private vec4 position;
        private vec4 orientation;
        private vec4 direction;
        private vec3 up = new vec3(0, 1, 0);

        public Camera(vec3 position, vec3 orientation)
        {
                //up definert "oben" in der Welt
            this.position = new vec4(position,1);
            this.orientation = new vec4(orientation,1);
            this.direction = new vec4(orientation - position, 0);
        }

        public Camera(vec4 position, vec4 direction)
        {
            this.position = position;
            this.direction = direction;
        }

        private mat4 getTransformation(vec4 position, vec4 orientation, vec3 up)
        {
            vec3 pos = new vec3(position.x, position.y, position.z);
            vec3 ori = new vec3(orientation.x, orientation.y, orientation.z);
            return glm.lookAt(pos, ori, up);
        }

        public vec3 Up
        {
            get { return up; }
        }

        public vec4 Orientation
        {
            get { return orientation; }
            set
            {
                this.orientation = value;
            }
        }

        public vec4 Direction
        {
            get { return direction; }
            set
            {
                this.direction = value;
            }
        }

        public vec4 Position
        {
            get { return position; }
            set
            {
                this.position = value;
            }
        }
    }
}
