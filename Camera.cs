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
        private mat4 transformation;

        private vec4 upperLeft;
        private vec4 upperRight;
        private vec4 lowerLeft;
        private vec4 lowerRight;

        public Camera(vec3 position, vec3 orientation)
        {
            vec3 up = new vec3(0,1,0);     //up definert "oben" in der Welt
            this.position = new vec4(position,1);
            this.orientation = new vec4(orientation,0);
            this.transformation = glm.lookAt(position, up, orientation);



        }

        private void calculateProjectionPlanePoints(float dist, float width, float height)
        {
            vec4 norm = glm.normalize(transformation * orientation);
            vec4 nPos = transformation * position;
            vec4 center = nPos + dist * norm;

            vec4 xDirec = new vec4(1, 0, 0, 0);
            vec4 yDirec = new vec4(0, 1, 0, 0);

            upperRight = transformation * ( center + height / 2 * yDirec + width / 2 * xDirec);
            upperLeft = transformation * (center + height / 2 * yDirec - width / 2 * xDirec);
            lowerRight = transformation * (center - height / 2 * yDirec + width / 2 * xDirec);
            lowerLeft = transformation * (center - height / 2 * yDirec - width / 2 * xDirec);
        }

        public mat4 Transformation
        {
            get { return transformation; }
        }
    }
}
