using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;
using System.Drawing;

namespace SimpleRayTracer
{
    class Light
    {
        private vec4 position;
        private vec3 lightColor = new vec3(0.7f, 0.7f, 0.7f);
        private static vec3 lightAmbient = new vec3(0.25f, 0.25f, 0.25f);

        public Light(vec4 position)
        {
            this.position = position;
        }

        public vec4 Position { get => position; set => position = value; }

        public static vec3 LightAmbient { get => lightAmbient; }
        public vec3 LightColour { get => lightColor; }
    }
}
