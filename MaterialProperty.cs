using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GlmNet;

namespace SimpleRayTracer
{
    class MaterialProperty
    {
        private vec3 ambientColour;
        private vec3 diffuseColour;
        private vec3 specularColour;
        private float specConst;
        private string name;

        public MaterialProperty() { }

        public MaterialProperty(vec3 ambientColour, vec3 diffuseColour, vec3 specularColour, float specConst, string name)
        {
            this.ambientColour = ambientColour;
            this.diffuseColour = diffuseColour;
            this.specularColour = specularColour;
            this.specConst = specConst;
            this.name = name;
        }

        public MaterialProperty(vec3 ambientColour, vec3 diffuseColour, vec3 specularColour, float specConst)
        {
            this.ambientColour = ambientColour;
            this.diffuseColour = diffuseColour;
            this.specularColour = specularColour;
            this.specConst = specConst;
            name = "CustomColour";
        }

        public MaterialProperty(vec3 objColour, vec3 specColour, float specConst)
        {
            this.ambientColour = objColour;
            this.diffuseColour = objColour;
            this.specularColour = specColour;
            this.specConst = specConst;
            name = "CustomMaterial";
        }

        public MaterialProperty(vec3 objColour, float specConst)
        {
            this.ambientColour = objColour;
            this.diffuseColour = objColour;
            this.specularColour = new vec3(255, 255, 255);
            this.specConst = specConst;
            name = "CustomMaterial";
        }

        public vec3 AmbientColour { get => ambientColour; }
        public vec3 DiffuseColour { get => diffuseColour; }
        public vec3 SpecularColour { get => specularColour; }
        public float SpecConst { get => specConst; }
        public override string ToString()
        {
            return name;
        }
    }
}
