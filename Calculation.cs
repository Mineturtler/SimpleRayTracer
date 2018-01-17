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

        /// <summary>
        /// Calculates the rotation matrix around a given axis with a angle in degree
        /// </summary>
        /// <param name="rotationAxis"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static mat3 calculateRotationMatrix(vec4 rotationAxis, float angle)
        {
            mat3 m = new mat3(1);
            float s = glm.sin(glm.radians(angle));
            s = (Math.Abs(s) < Constants.Epsilon) ? 0 : s;
            float c = glm.cos(glm.radians(angle));
            c = (Math.Abs(c) < Constants.Epsilon) ? 0 : c;
            float t = 1 - glm.cos(glm.radians(angle));
            t = (Math.Abs(t) < Constants.Epsilon) ? 0 : t;

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

        public static mat4 calculateTransformationMatrix(vec4 position, mat3 rotation, vec3 scale)
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

        public static Color createColour(vec3 colourValues)
        {
            int r = (int)Math.Round(colourValues.x * 255);
            if (r < 0) r = 0;
            if (r > 255) r = 255;
            int g = (int)Math.Round(colourValues.y * 255);
            if (g < 0) g = 0;
            if (g > 255) g = 255;
            int b = (int)Math.Round(colourValues.z * 255);
            if (b < 0) b = 0;
            if (b > 255) b = 255;
            return Color.FromArgb(r, g, b);
        }

        internal static Color calculateShadowColor(SceneManager sManager, vec4 intersectionPoint, MaterialProperty material)
        {
            vec3 _currentColor = material.AmbientColour;
            _currentColor = _currentColor * Light.LightAmbient;
            return createColour(_currentColor);
        }

        internal static bool isPointInShadow(SceneManager sManager, vec4 intersectionPoint)
        {
            foreach(var l in sManager.LightList)
            {
                if (isVisibleToLight(sManager, l, intersectionPoint))
                    return false;
            }
            return true;
        }

        private static bool isVisibleToLight(SceneManager sManager, Light l, vec4 intersectionPoint)
        {
            /*
                1) Ray von der Lichtquelle zur Oberfläche
                2) Mit AABB Gruppen schneiden
                3) Mit den restlichen Elementen
                4) Falls ein Objekt zwischen Punkt - Quelle -> false
                5) Falls kein Objekt zwischen Punkt - Quelle -> true
             */

            Ray _ray = new Ray(l.Position, intersectionPoint - l.Position);
            /**/
            foreach(var _group in sManager.AABBList)
            {
                if (!_group.hitAABB(_ray)) continue;
                foreach (var _element in _group.ElementList)
                    if (_element.hasAnyIntersectionPoint(_ray))
                        return false;
            }

            foreach (var _element in sManager.ObjectList)
            {
                if (_element.Value.PartOfGroup) continue;
                if(_element.Value.hasAnyIntersectionPoint(_ray))
                    return false;
            }

            return true;
        }
        
        internal static mat4 multiplyTwoMatrices(mat4 A, mat4 B)
        {
            mat4 C = new mat4(1);
            for(int i = 0; i < 4; i++)
                for(int j = 0; j < 4; j++)
                {
                    C[j,i] = glm.dot(new vec4(A[0, i], A[1, i], A[2, i], A[3, i]), B[j]);
                }

            return C;
        }

        internal static mat4 transposeMatrix(mat4 A)
        {
            mat4 C = new mat4(1);
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    C[i, j] = A[j, i];
            return C;
        }

        internal static vec4 getHightestCombination(params vec4[] vectors)
        {
            vec4 c = vectors[0];
            foreach(var v in vectors)
            {
                c.x = (v.x > c.x) ? v.x : c.x;
                c.y = (v.y > c.y) ? v.y : c.y;
                c.z = (v.z > c.z) ? v.z : c.z;
            }
            return c;
        }

        internal static vec4 getLowestCombination(params vec4[] vectors)
        {
            vec4 c = vectors[0];
            foreach (var v in vectors)
            {
                c.x = (v.x < c.x) ? v.x : c.x;
                c.y = (v.y < c.y) ? v.y : c.y;
                c.z = (v.z < c.z) ? v.z : c.z;
            }
            return c;
        }

        internal static vec4 multiplyScalarToVector(vec4 v, float lambda)
        {
            return new vec4(lambda * v.x, lambda * v.y, lambda * v.z, v.w);
        }
    }
}
