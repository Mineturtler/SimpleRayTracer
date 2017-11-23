using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;

namespace SimpleRayTracer
{
    class Ray
    {
        private vec4 _start;
        private vec4 _end;
        private vec4 _direction;

        public Ray(vec4 start, vec4 direc)
        {
            _start = start;
            _direction = direc;
            _end = start + direc;
        }

        public vec4 getPointOnRay(float t)
        {
            return _start + t * _direction;
        }

        public vec4 StartingPoint { get => _start; }
        public vec4 EndPoint { get => _end; }
        public vec4 Direction { get => _direction; }
    }
}
