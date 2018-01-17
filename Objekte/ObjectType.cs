using System.Drawing;
using GlmNet;
using System;
using System.Collections.Generic;

namespace SimpleRayTracer
{
    abstract class ObjectType
    {
        private mat4 transformationMatrix;
        private mat4 inverse;
        private int idNumber;
        private MaterialProperty _object_material;
        private bool shadows = true;
        private bool partOfGroup = false;

        public ObjectType(int idNumber, mat4 transformationMatrix, MaterialProperty material)
        {
            this.idNumber = idNumber;
            this.transformationMatrix = transformationMatrix;
            this.inverse = glm.inverse(transformationMatrix);
            this._object_material = material;
        }

        public ObjectType(int idNumber, vec4 position, mat3 rotation, vec3 scale, MaterialProperty material)
        {
            this.transformationMatrix = Calculation.calculateTransformationMatrix(position, rotation, scale);
            this.inverse = glm.inverse(transformationMatrix);
            this._object_material = material;
        }

        public ObjectType(int idNumber, vec4 position, vec4 rotationAxis, float angle, vec3 scale, MaterialProperty material)
        {
            this.idNumber = idNumber;
            this.transformationMatrix = Calculation.calculateTransformationMatrix(position, Calculation.calculateRotationMatrix(rotationAxis, angle), scale);
            this.inverse = glm.inverse(transformationMatrix);
            this._object_material = material;
        }

        public ObjectType(int idNumber, vec4 position, vec3 scale, MaterialProperty material)
        {
            this.idNumber = idNumber;
            this.transformationMatrix = Calculation.calculateTransformationMatrix(position, Calculation.calculateRotationMatrix(new vec4(0, 0, 0,1), 0), scale);
            this.inverse = glm.inverse(transformationMatrix);
            this._object_material = material;
        }

        public ObjectType(int idNumber, vec4 position, MaterialProperty material)
        {
            this.idNumber = idNumber;
            this.transformationMatrix = Calculation.calculateTransformationMatrix(position, mat3.identity(), new vec3(1));
            this.inverse = glm.inverse(transformationMatrix);
            this._object_material = material;
        }

        public ObjectType(int idNumber, vec4 position, vec4 rotationAxis, float angle, MaterialProperty material)
        {
            this.idNumber = idNumber;
            this.transformationMatrix = Calculation.calculateTransformationMatrix(position, Calculation.calculateRotationMatrix(rotationAxis, angle), new vec3(1));
            this._object_material = material;
            this.inverse = glm.inverse(transformationMatrix);
        }


        internal Color getIlluminationAt(SceneManager sManager, vec4 intersectionPoint, vec4 direction, vec4 normal, MaterialProperty material)
        {
            vec4 _intersection = intersectionPoint + Constants.Epsilon * normal;
            
            if(shadows)
            {
                if (Calculation.isPointInShadow(sManager, _intersection))
                {
                    return Calculation.calculateShadowColor(sManager, _intersection, material);
                }
                else
                    return getPhongAt(sManager.LightList, direction, _intersection, normal, material);
            }
            else
                return getPhongAt(sManager.LightList, direction, _intersection, normal, material);
        }

        internal Color getPhongAt(List<Light> lightList, vec4 incomingDirection, vec4 surfacePoint, vec4 surfaceNormal, MaterialProperty material)
        {
            vec3 _color = material.AmbientColour * Light.LightAmbient;
            vec4 _viewDirection = (-1) * glm.normalize(incomingDirection);
            foreach(Light l in lightList)
            {
                vec4 _lightDirection = glm.normalize(l.Position - surfacePoint);
                vec4 _reflectionDirection = 2 * glm.dot(surfaceNormal, _lightDirection) * surfaceNormal - _lightDirection;

                float _diffuseQuota = Math.Max(0, glm.dot(surfaceNormal, _lightDirection));
                float _specularQuota = (float) Math.Pow(Math.Max(0, glm.dot(_reflectionDirection, _viewDirection)),material.SpecConst);

                _color += _color + l.LightColour * (material.DiffuseColour * _diffuseQuota + material.SpecularColour * _specularQuota);
            }

            return Calculation.createColour(_color);
        }
        
        internal virtual void updateObject(vec4 translate, vec4 rotationAxis, float angle, vec3 scaling)
        {
            mat4 _transformation = Calculation.calculateTransformationMatrix(translate, Calculation.calculateRotationMatrix(rotationAxis, angle), scaling);
            mat4 newMat = Calculation.multiplyTwoMatrices(_transformation, transformationMatrix);
            transformationMatrix = newMat;
            inverse = glm.inverse(newMat);
        }

        internal void updateObject(mat4 transformationMatrix)
        {
            mat4 newMat = Calculation.multiplyTwoMatrices(transformationMatrix, this.transformationMatrix);
            this.transformationMatrix = newMat;
            inverse = glm.inverse(newMat);
        }

        internal abstract bool hasAnyIntersectionPoint(Ray ray);
        abstract public bool hasIntersectionPoint(Ray ray, out vec4 intersecPoint, out vec4 normal, out float t, out MaterialProperty materialProperty);

        public bool PartOfGroup { get => partOfGroup; set => partOfGroup = value; }
        public mat4 TransformationMatrix
        { get => transformationMatrix;
          set { transformationMatrix = value; inverse = glm.inverse(transformationMatrix); }
        }
        public mat4 Inverse { get => inverse; }
        public int IdNumber { get => idNumber; }
        public MaterialProperty ObjectMaterial { get => _object_material; }
    }
}
