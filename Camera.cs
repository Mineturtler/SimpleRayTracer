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
        private vec3 up = new vec3(0, 1, 0);

        public Camera(vec3 position, vec3 orientation)
        {
                //up definert "oben" in der Welt
            this.position = new vec4(position,1);
            this.orientation = new vec4(orientation,0);
            this.transformation = glm.lookAt(position, up, orientation);
        }

        private mat4 getTransformation(vec4 position, vec4 orientation, vec3 up)
        {
            vec3 pos = new vec3(position.x, position.y, position.z);
            vec3 ori = new vec3(orientation.x, orientation.y, orientation.z);
            return glm.lookAt(pos, ori, up);
        }


        /* Redundant: Projektionsebene erst bei der Berechnung erstellen.
         * Entsprechend ein Gitter anhand der Auflösung aufstellen
        private vec4 upperLeft;
        private vec4 upperRight;
        private vec4 lowerLeft;
        private vec4 lowerRight;
        private float alpha = glm.radians(45); // pi/4, tan(pi/4) = 1 -> hoehe = dist
        private float beta = glm.radians(60); // nicht weiter benötigt: breite = aspectRatio * hoehe
        */
        /*
        private void calculateProjectionPlanePoints(float dist)
        {
            vec4 norm = glm.normalize(glm.inverse(transformation) * orientation);
            vec4 nPos = glm.inverse(transformation) * position;
            vec4 center = nPos + dist * norm;

            float height = glm.tan(alpha) * dist;
            float width = glm.tan(beta) * dist;

            vec4 xDirec = new vec4(1, 0, 0, 0);
            vec4 yDirec = new vec4(0, 1, 0, 0);

            upperRight = transformation * ( center + height / 2 * yDirec + width / 2 * xDirec);
            upperLeft = transformation * (center + height / 2 * yDirec - width / 2 * xDirec);
            lowerRight = transformation * (center - height / 2 * yDirec + width / 2 * xDirec);
            lowerLeft = transformation * (center - height / 2 * yDirec - width / 2 * xDirec);
        }
        */
        public vec3 Up
        {
            get { return up; }
        }

        public mat4 Transformation
        {
            get { return transformation; }
        }

        public vec4 Orientation
        {
            get { return orientation; }
            set
            {
                this.orientation = value;
                transformation = getTransformation(position,value, up);
            }
        }

        public vec4 Position
        {
            get { return position; }
            set
            {
                this.position = value;
                this.transformation = getTransformation(value, orientation, up);
            }
        }
    }
}
