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
        private LightProperties objectLight;

        public ObjectType(int idNumber, mat4 transformationMatrix, LightProperties objectLight)
        {
            this.idNumber = idNumber;
            this.transformationMatrix = transformationMatrix;
            this.objectLight = objectLight;
        }

        public ObjectType(int idNumber, vec3 position, mat3 rotation, vec3 scale, LightProperties objectLight)
        {
            this.idNumber = idNumber;
            this.transformationMatrix = Calculation.calculateTransformationMatrix(position, rotation, scale);
            this.objectLight = objectLight;
        }

        public ObjectType(int idNumber, vec3 position, vec3 rotationAxis, float angle, vec3 scale, LightProperties objectLight)
        {
            this.idNumber = idNumber;
            this.transformationMatrix = Calculation.calculateTransformationMatrix(position, Calculation.calculateRotationMatrix(rotationAxis, angle), scale);
            this.objectLight = objectLight;
        }

        public ObjectType(int idNumber, vec3 position, vec3 scale, LightProperties objectLight)
        {
            this.idNumber = idNumber;
            this.transformationMatrix = Calculation.calculateTransformationMatrix(position, Calculation.calculateRotationMatrix(new vec3(0, 0, 0), 0), scale);
            this.objectLight = objectLight;
        }

        public ObjectType(int idNumber, vec3 position, LightProperties objectLight)
        {
            this.idNumber = idNumber;
            this.transformationMatrix = Calculation.calculateTransformationMatrix(position, mat3.identity(), new vec3(1, 1, 1));
            this.objectLight = objectLight;
        }

        public Color getPhongAt(List<Light> lightList, vec4 direction, vec4 intersecPoint)
        {
            vec3 _color = objectLight.AmbientColour * Light.LightAmbient;
            mat4 _transMat = glm.inverse(transformationMatrix);
            vec4 _interPoint = intersecPoint;
            vec4 _normal = getNormalisedNormalAt(_interPoint);
            vec4 _vDirec = (-1)* glm.normalize((_transMat * direction));

            foreach (Light l in lightList)
            {
                vec4 _lPos = _transMat * l.Position;
                vec4 _lDirec = glm.normalize(_lPos - _interPoint);
                float _nl = Math.Max(0, Calculation.scalarProduct(_normal, _lDirec));

                vec4 _r = (2 * _nl * _normal) - _lDirec;

                float _rv = Math.Max(0,Calculation.scalarProduct(_r, _vDirec));
                float _m = objectLight.SpecConst;
                float _s = (float)Math.Pow(_rv, objectLight.SpecConst);

                _color += l.LightColour * (objectLight.DiffuseColour * _nl
                                            + objectLight.SpecularColour * _s);
            }
            
            return Calculation.createColour(_color);
        }

        abstract public vec4 getNormalAt(vec4 pos);

        public vec4 getNormalisedNormalAt(vec4 pos)
        {
            return glm.normalize(getNormalAt(pos));
        }

        abstract public bool hasIntersectionPoint(Ray ray, out vec4 intersecPoint, out float t);
        
        public mat4 TransformationMatrix { get => transformationMatrix; set => transformationMatrix = value; }
        
        public int IdNumber { get => idNumber; set => idNumber = value; }

        public LightProperties ObjectLight { get => objectLight; }
    }
}
