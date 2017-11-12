using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;

namespace SimpleRayTracer.Objekte
{
    class Triangle : ObjectType
    {
        private vec4 _p1;
        private vec4 _p2;
        private vec4 _p3;

        public Triangle(int objID, mat4 transMatrix, LightProperties lightProp) : base(objID, transMatrix, lightProp)
        {

        }

        public override vec4 getNormalAt(vec4 pos)
        {
            throw new NotImplementedException();
        }

        public override bool hasIntersectionPoint(Ray ray, out vec4 intersecPoint, out float t)
        {
            throw new NotImplementedException();
        }
    }
}
