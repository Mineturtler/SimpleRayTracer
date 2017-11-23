using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;

namespace SimpleRayTracer.Objekte
{
    class Plane : ObjectType
    {
        float _halfLength;
        float _halfHeight;
        vec4 _normal = new vec4(0, 1, 0, 0);
        vec4 _referencePoint = new vec4(0, 0, 0, 1);

        /// <summary>
        /// Creates a plane with given size vector. The default plane is the xz-plane with normal vector (0,1,0)
        /// with reference point (0,0,0)
        /// </summary>
        /// <param name="size">x-value: length, y-value: height</param>
        public Plane(vec2 size, int objId, mat4 transformationMatrix, MaterialProperty material) : base(objId, transformationMatrix, material)
        {
            _halfLength = 1 / 2f * size.x;
            _halfHeight = 1 / 2f * size.y;
            _normal = transformationMatrix * _normal;
            _referencePoint = transformationMatrix * _referencePoint;
        }

        public Plane(vec2 size, int objId, vec3 position, MaterialProperty material) : base(objId,position,material)
        {
            _halfLength = 1 / 2f * size.x;
            _halfHeight = 1 / 2f * size.y;
            _normal = TransformationMatrix * _normal;
            _referencePoint = TransformationMatrix * _referencePoint;
        }

        public override bool hasIntersectionPoint(Ray ray, out vec4 intersecPoint, out vec4 normal, out float t, out MaterialProperty materialProperty)
        {
            mat4 _trans = glm.inverse(TransformationMatrix);
            vec4 _start = _trans * ray.StartingPoint;
            vec4 _end = _trans * ray.EndPoint;
            vec4 _direction = _end - _start;
            vec4 _n = _trans * _normal;
            vec4 _r = _trans * _referencePoint;

            intersecPoint = new vec4();
            normal = _normal;
            t = -1;
            materialProperty = this.ObjectMaterial;

            if (glm.dot(_n, _direction) == 0)
                return false;

            t = glm.dot((_r - _start), _n) / glm.dot(_n, _direction);
            vec4 intersectionPoint_inObjectSpace = new Ray(_start, _direction).getPointOnRay(t);

            if (Math.Abs(intersectionPoint_inObjectSpace.x - _referencePoint.x) > _halfLength ||
                Math.Abs(intersectionPoint_inObjectSpace.z - _referencePoint.z) > _halfHeight)
                return false;

            intersecPoint = ray.getPointOnRay(t);
            return true;
        }

        internal override bool hasAnyIntersectionPoint(Ray ray)
        {
            mat4 _trans = glm.inverse(TransformationMatrix);
            vec4 _start = _trans * ray.StartingPoint;
            vec4 _end = _trans * ray.EndPoint;
            vec4 _direction = _end - _start;
            vec4 _n = _trans * _normal;
            vec4 _r = _trans * _referencePoint;
            
            float t = -1;

            if (glm.dot(_n, _direction) == 0)
                return false;

            t = glm.dot((_r - _start), _n) / glm.dot(_n, _direction);
            if (t == 1)
                Console.Write("jop");
            if (t < Constants.Epsilon)
                return false;

            vec4 intersectionPoint_inObjectSpace = new Ray(_start, _direction).getPointOnRay(t);

            if (Math.Abs(intersectionPoint_inObjectSpace.x - _referencePoint.x) > _halfLength ||
                Math.Abs(intersectionPoint_inObjectSpace.z - _referencePoint.z) > _halfHeight)
                return false;
            return true;
        }
    }
}
