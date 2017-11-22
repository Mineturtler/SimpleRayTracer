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
        private vec3 _direction;

        public ViewPlane(Camera _c, int _resoWidth, int _resoHeight, float _foV, float _offset)
        {
            _c_pos = new vec3(_c.Position);
            vec3 _ori = new vec3(_c.Orientation);

            vec3 _direction = glm.normalize(_ori - _c_pos);
            this._direction = _direction;
            vec3 _u = glm.normalize(glm.cross(_direction, _c.Up));
            vec3 _v = (-1) * glm.normalize(glm.cross(_u, _direction));

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
        public vec4 Direction { get => new vec4(_direction, 0); }

    }
}
