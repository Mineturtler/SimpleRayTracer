using System.Drawing;
using GlmNet;
using System;
using System.Collections.Generic;

namespace SimpleRayTracer
{
    abstract class ObjectType
    {
        private mat4 transformationMatrix;
        private int idNumber;
        private MaterialProperty _object_material;
        private vec3 originalScaling = new vec3(1,1,1);
        private bool shadows = false;

        public ObjectType(int idNumber, mat4 transformationMatrix, MaterialProperty material)
        {
            this.idNumber = idNumber;
            this.transformationMatrix = transformationMatrix;
            this._object_material = material;
        }

        public ObjectType(int idNumber, vec3 position, mat3 rotation, vec3 scale, MaterialProperty material)
        {
            this.idNumber = idNumber;

            this.transformationMatrix = Calculation.calculateTransformationMatrix(position, rotation, scale);
            this._object_material = material;
            this.originalScaling = scale;
        }

        public ObjectType(int idNumber, vec3 position, vec3 rotationAxis, float angle, vec3 scale, MaterialProperty material)
        {
            this.idNumber = idNumber;
            this.transformationMatrix = Calculation.calculateTransformationMatrix(position, Calculation.calculateRotationMatrix(rotationAxis, angle), scale);
            this._object_material = material;
            this.originalScaling = scale;
        }

        public ObjectType(int idNumber, vec3 position, vec3 scale, MaterialProperty material)
        {
            this.idNumber = idNumber;
            this.transformationMatrix = Calculation.calculateTransformationMatrix(position, Calculation.calculateRotationMatrix(new vec3(0, 0, 0), 0), scale);
            this._object_material = material;
            this.originalScaling = scale;
        }

        public ObjectType(int idNumber, vec3 position, MaterialProperty material)
        {
            this.idNumber = idNumber;
            this.transformationMatrix = Calculation.calculateTransformationMatrix(position, mat3.identity(), new vec3(1, 1, 1));
            this._object_material = material;
        }

        public ObjectType(int idNumber,vec3 position, vec3 rotationAxis, float angle, MaterialProperty material)
        {
            this.idNumber = idNumber;
            this.transformationMatrix = Calculation.calculateTransformationMatrix(position, Calculation.calculateRotationMatrix(rotationAxis, angle), new vec3(1,1,1));
            this._object_material = material;
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

        internal Color getPhongAt(List<Light> lightList, vec4 direction, vec4 intersecPoint, vec4 normal, MaterialProperty material)
        {
            vec3 _color = material.AmbientColour * Light.LightAmbient;
            mat4 _transMat = glm.inverse(transformationMatrix);
            vec4 _interPoint = intersecPoint;
            vec4 _normal = normal;
            vec4 _vDirec = (-1) * glm.normalize((_transMat * direction));

            foreach (Light l in lightList)
            {
                vec4 _lPos = _transMat * l.Position;
                vec4 _lDirec = glm.normalize(_lPos - _interPoint);
                float _nl = Math.Max(0, glm.dot(_normal, _lDirec));

                vec4 _r = (2 * _nl * _normal) - _lDirec;

                float _rv = Math.Max(0, glm.dot(_r, _vDirec));
                float _m = material.SpecConst;
                float _s = (float)Math.Pow(_rv, material.SpecConst);

                _color += l.LightColour * (material.DiffuseColour * _nl
                                            + material.SpecularColour * _s);
            }

            return Calculation.createColour(_color);
        }
        
        /*
        internal virtual void moveObjectInDirection(vec3 direction)
        {
            vec3 currentPosition = new vec3(transformationMatrix[3].x, transformationMatrix[3].y, transformationMatrix[3].z);
            vec3 updatedPosition = currentPosition + direction;
            transformationMatrix[3, 0] = updatedPosition.x;
            transformationMatrix[3, 1] = updatedPosition.y;
            transformationMatrix[3, 2] = updatedPosition.z;
        }


        internal virtual void scaleObject(vec3 scaling)
        {
            vec3 currentPosition = new vec3(transformationMatrix[3].x, transformationMatrix[3].y, transformationMatrix[3].z);
            mat3 currentRotation = new mat3(new vec3(transformationMatrix[0]), new vec3(transformationMatrix[1]), new vec3(transformationMatrix[2]));
            mat4 _transformation = Calculation.calculateTransformationMatrix(currentPosition, currentRotation, scaling);
            this.transformationMatrix = _transformation;
        }

        internal virtual void rotateAroundAxisWithAngle(vec3 rotationAxis, float angle)
        {
            vec3 currentPosition = new vec3(transformationMatrix[3].x, transformationMatrix[3].y, transformationMatrix[3].z);
            mat3 rotMatrix = Calculation.calculateRotationMatrix(rotationAxis, angle);
            mat4 _transformation = Calculation.calculateTransformationMatrix(rotMatrix * currentPosition, rotMatrix, new vec3(1,1,1) );
            this.transformationMatrix = _transformation;
        }
        */

        internal virtual void updateObject(vec3 translate, vec3 rotationAxis, float angle, vec3 scaling)
        {
            mat4 _transformation = Calculation.calculateTransformationMatrix(translate, Calculation.calculateRotationMatrix(rotationAxis, angle), scaling);
            this.transformationMatrix = _transformation * transformationMatrix;
        }

        internal abstract bool hasAnyIntersectionPoint(Ray ray);
        abstract public bool hasIntersectionPoint(Ray ray, out vec4 intersecPoint, out vec4 normal, out float t, out MaterialProperty materialProperty);

        public mat4 TransformationMatrix { get => transformationMatrix; set => transformationMatrix = value; }
        public int IdNumber { get => idNumber; }
        public MaterialProperty ObjectMaterial { get => _object_material; }
    }
}
