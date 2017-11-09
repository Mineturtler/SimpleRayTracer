using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;

namespace SimpleRayTracer
{
    class ViewPlane
    {
        private float halfPlaneWidth;
        private float halfPlaneHeigth;
        private float foV = glm.radians(60);
        private const float cameraDist = 6f;
        private vec3 up = new vec3(0, 1, 0);
        private vec4 widthDirection;
        private vec4 heigthDirection;
        private vec4 upperLeftPoint;
        private vec4 center;
        private mat4 viewMatrix;

        private ViewPlane(Camera mCamera, int resoWidth, int resoHeigth)
        {
            vec3 position = new vec3(mCamera.Position);
            vec3 target = new vec3(mCamera.Orientation);

            vec3 zaxis = glm.normalize(position - target);
            vec3 xaxis = glm.normalize(glm.cross(zaxis, up));
            vec3 yaxis = glm.cross(xaxis, zaxis);
            
            halfPlaneWidth = cameraDist * glm.tan(foV);
            halfPlaneHeigth = (halfPlaneWidth * resoHeigth) / resoWidth;

            widthDirection = new vec4(xaxis,0);
            heigthDirection = new vec4(yaxis,0);

            center = new vec4(cameraDist * zaxis, 1);

            upperLeftPoint = new vec4(new vec3(0, 0, 0) + cameraDist * zaxis 
                                                        - halfPlaneWidth * xaxis 
                                                        + halfPlaneHeigth * yaxis, 1);

            vec4 coloumn1 = new vec4(xaxis.x, yaxis.x, zaxis.x, 0);
            vec4 coloumn2 = new vec4(xaxis.y, yaxis.y, zaxis.y, 0);
            vec4 coloumn3 = new vec4(xaxis.z, yaxis.z, zaxis.z, 0);
            vec4 coloumn4 = new vec4(-glm.dot(xaxis, position), -glm.dot(yaxis, position), -glm.dot(zaxis, position), 1);


            viewMatrix = new mat4(coloumn1, coloumn2, coloumn3, coloumn4);
            
        }

        public static ViewPlane createViewPlane(Camera mCamera, int resolutionWidth, int resolutionHeigth)
        {
            return new ViewPlane(mCamera, resolutionWidth, resolutionHeigth);
        }

        public float PlaneWidth
        {
            get { return 2*halfPlaneWidth; }
        }

        public float PlaneHeigth
        {
            get { return 2*halfPlaneHeigth; }
        }

        public vec4 WidthDirection
        {
            get { return widthDirection; }
        }

        public vec4 HeigthDirection
        {
            get { return heigthDirection; }
        }

        public vec4 UpperLeftPoint
        {
            get { return upperLeftPoint; }
        }

        public vec4 Center
        {
            get { return center; }
        }

        public mat4 ViewMatrix
        {
            get { return viewMatrix; }
        }
    }
}
