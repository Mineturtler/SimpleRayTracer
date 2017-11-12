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
        private float _halfPlaneWidth;
        private float _halfPlaneHeight;

        private vec3 _c_pos;
        private vec3 _u_increment;
        private vec3 _v_increment;

        private vec3 _topLeft_Point;

        private float foV = glm.radians(60);
        private const float cameraDist = 6f;


        public ViewPlane(Camera _c, int _resoWidth, int _resoHeight, float _foV, float _offset)
        {
            _c_pos = new vec3(_c.Position);
            vec3 _ori = new vec3(_c.Orientation);

            vec3 _direction = glm.normalize(_ori - _c_pos);
            vec3 _u = glm.normalize(glm.cross(_direction, _c.Up));
            vec3 _v = (-1)*glm.normalize(glm.cross(_u, _direction));

            _halfPlaneHeight = _offset * (float)glm.tan((glm.radians(_foV / 2)));
            _halfPlaneWidth = (_halfPlaneHeight * _resoWidth) / _resoHeight;

            _topLeft_Point = (_c_pos + _offset * _direction - _u * _halfPlaneWidth - _v * _halfPlaneHeight);
            _u_increment = (_u * _halfPlaneWidth * 2) / _resoWidth;
            _v_increment = (_v * _halfPlaneHeight * 2) / _resoHeight;
        }

        public vec4 U_Increment { get => new vec4(_u_increment, 0); }
        public vec4 V_Increment { get => new vec4(_v_increment, 0); }
        public vec4 TopLeft { get => new vec4(_topLeft_Point, 1); }
        public vec4 Camera_Pos { get => new vec4(_c_pos, 1); } 

        /*
        private ViewPlane(Camera mCamera, int resoWidth, int resoHeigth)
        {
            vec3 position = new vec3(mCamera.Position);
            vec3 target = new vec3(mCamera.Orientation);

            vec3 zaxis = glm.normalize(target - position);
            vec3 xaxis = glm.normalize(glm.cross(zaxis, up));
            vec3 yaxis = glm.normalize(glm.cross(zaxis, xaxis));
            
            halfPlaneWidth = cameraDist * glm.tan(foV);
            halfPlaneHeigth = (halfPlaneWidth * resoHeigth) / resoWidth;

            widthDirection = new vec4(xaxis,0);
            heigthDirection = new vec4(yaxis,0);

            center = new vec4(position + cameraDist * zaxis, 1);

            upperLeftPoint = new vec4(position + cameraDist * zaxis 
                                                        - halfPlaneWidth * xaxis 
                                                        - halfPlaneHeigth * yaxis, 1);

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
        */
    }
}
