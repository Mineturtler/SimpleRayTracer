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
        private vec3 up;

        public Camera(vec3 position, vec3 orientation)
        {
            this.position = new vec4(position,1);
            this.orientation = new vec4(orientation,1);
            up = new vec3(0, 1, 0);
        }

        public vec4 Orientation { get => orientation; set => orientation = value; }

        public vec4 Position { get => position; set => position = value; }

        public vec3 Up { get => up; set => up = value; }
    }
}
