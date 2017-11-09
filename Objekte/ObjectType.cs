using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;

namespace SimpleRayTracer
{
    abstract class ObjectType
    {
        private mat4 transformationMatrix;
        private int idNumber;

        public ObjectType(int idNumber, mat4 transformationMatrix)
        {
            this.idNumber = idNumber;
            this.transformationMatrix = transformationMatrix;
        }

        public ObjectType(int idNumber, vec3 position, mat3 rotation, vec3 scale)
        {
            this.idNumber = idNumber;
            this.transformationMatrix = Calculation.calculateTransformationMatrix(position, rotation, scale);
        }

        public ObjectType(int idNumber, vec3 position, vec3 rotationAxis, float angle, vec3 scale)
        {
            this.idNumber = idNumber;
            this.transformationMatrix = Calculation.calculateTransformationMatrix(position, Calculation.calculateRotationMatrix(rotationAxis, angle), scale);
        }
        public ObjectType(int idNumber, vec3 position, vec3 scale)
        {
            this.idNumber = idNumber;
            this.transformationMatrix = Calculation.calculateTransformationMatrix(position, Calculation.calculateRotationMatrix(new vec3(0, 0, 0), 0), scale);
        }
        public ObjectType(int idNumber, vec3 position)
        {
            this.idNumber = idNumber;
            this.transformationMatrix = Calculation.calculateTransformationMatrix(position, mat3.identity(), new vec3(1, 1, 1));
        }

        abstract public vec4 getNormalAt(vec4 pos);

        public vec4 getNormalAt(vec3 pos)
        {
            return getNormalAt(new vec4(pos, 1));
        }

        public vec4 getNormalAt(float x, float y, float z)
        {
            return getNormalAt(new vec4(x, y, z, 0));
        }

        public vec4 getNormalisedNormalAt(vec4 pos)
        {
            return glm.normalize(getNormalAt(pos));
        }

        public vec4 getNormalisedNormalAt(vec3 pos)
        {
            return glm.normalize(getNormalAt(pos));
        }

        public vec4 getNormalisedNormalAt(float x, float y, float z)
        {
            return glm.normalize(getNormalAt(x, y, z));
        }

        public vec4 getIntersectionPoint(Ray ray)
        {
            float t = getIntersectionParameter(ray);
            return getIntersectionPoint(ray, t);
        }

        public vec4 getIntersectionPoint(Ray ray, float t)
        {
            vec4 direction = ray.Direction;
            vec4 position = ray.StartingPoint;
            return new vec4(position.x + t * direction.x, position.y + t * direction.y, position.z + t * direction.z, 1);
        }

        public vec4 getIntersectionPoint(vec4 startingPoint, vec4 direction)
        {
            return getIntersectionPoint(new Ray(startingPoint, direction));
        }

        public vec4 getIntersectionPoint(vec3 startingPoint, vec3 direction)
        {
            return getIntersectionPoint(new vec4(startingPoint, 1), new vec4(direction, 0));
        }

        abstract public bool hasIntersectionPoint(Ray ray);

        public bool hasIntersectionPoint(vec4 startingPoint, vec4 direction)
        {
            return hasIntersectionPoint(new Ray(startingPoint, direction));
        }

        abstract public float getIntersectionParameter(Ray ray);

        public float getIntersectionParameter(vec4 startingPoint, vec4 direction)
        {
            return getIntersectionParameter(new Ray(startingPoint, direction));
        }

        public mat4 TransformationMatrix
        {
            get { return transformationMatrix; }
            set { this.transformationMatrix = value; }
        }

        public int IdNumber
        {
            get { return idNumber; }
        }

    }
}
