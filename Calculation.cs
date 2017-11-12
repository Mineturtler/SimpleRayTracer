using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;
using System.Drawing;

namespace SimpleRayTracer
{
    class Calculation
    {
        private Calculation() { }

        public static mat3 calculateRotationMatrix(vec3 rotationAxis, float angle)
        {
            mat3 m = new mat3(1);
            float s = glm.sin(glm.radians(angle));
            float c = glm.cos(glm.radians(angle));
            float t = 1 - glm.cos(glm.radians(angle));

            float x = rotationAxis[0];
            float y = rotationAxis[1];
            float z = rotationAxis[2];

            m[0, 0] = t * x * x + c;
            m[0, 1] = t * x * y + s * z;
            m[0, 2] = t * x * z - s * y;
            m[1, 0] = t * x * y - s * z;
            m[1, 1] = t * y * y + c;
            m[1, 2] = t * z * y + x * s;
            m[2, 0] = t * x * z + s * y;
            m[2, 1] = t * y * z - s * x;
            m[2, 2] = t * z * z + c;

            return m;
        }

        public static mat4 calculateTransformationMatrix(vec3 position, mat3 rotation, vec3 scale)
        {
            mat4 transformation = new mat4(1);
            
            transformation[0, 0] = rotation[0, 0] * scale[0];
            transformation[0, 1] = rotation[0, 1];
            transformation[0, 2] = rotation[0, 2];
            transformation[0, 3] = 0;
            transformation[1, 0] = rotation[1, 0];
            transformation[1, 1] = rotation[1, 1] * scale[1];
            transformation[1, 2] = rotation[1, 2];
            transformation[1, 3] = 0;
            transformation[2, 0] = rotation[2, 0];
            transformation[2, 1] = rotation[2, 1];
            transformation[2, 2] = rotation[2, 2] * scale[2];
            transformation[2, 3] = 0;
            transformation[3, 0] = position[0];
            transformation[3, 1] = position[1];
            transformation[3, 2] = position[2];
            transformation[3, 3] = 1;
            
            return transformation;
        }

        public static float scalarProduct(vec4 a, vec4 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }

        public static Color createColour(vec3 colourValues)
        {   
            int r = (int)Math.Round(colourValues.x*255);
            if (r < 0) r = 0;
            if (r > 255) r = 255;
            int g = (int) Math.Round(colourValues.y*255);
            if (g < 0) g = 0;
            if (g > 255) g = 255;
            int b = (int) Math.Round(colourValues.z*255);
            if (b < 0) b = 0;
            if (b > 255) b = 255;
            return Color.FromArgb(r,g,b);
        }

        public static vec4 getPointOnRay(Ray ray, float t)
        {
            return ray.StartingPoint + t * ray.Direction;
        }
    }
}
