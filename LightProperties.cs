using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GlmNet;

namespace SimpleRayTracer
{
    class LightProperties
    {
        private vec3 ambientColour;
        private vec3 diffuseColour;
        private vec3 specularColour;
        private float specConst;

        public LightProperties() { }

        public LightProperties(vec3 ambientColour, vec3 diffuseColour, vec3 specularColour, float specConst)
        {
            this.ambientColour = ambientColour;
            this.diffuseColour = diffuseColour;
            this.specularColour = specularColour;
            this.specConst = specConst;
        }

        public LightProperties(vec3 objColour, vec3 specColour, float specConst)
        {
            this.ambientColour = objColour;
            this.diffuseColour = objColour;
            this.specularColour = specColour;
            this.specConst = specConst;
        }

        public LightProperties(vec3 objColour, float specConst)
        {
            this.ambientColour = objColour;
            this.diffuseColour = objColour;
            this.specularColour = new vec3(255,255,255);
            this.specConst = specConst;
        }

        public vec3 AmbientColour { get => ambientColour; }
        public vec3 DiffuseColour { get => diffuseColour; }
        public vec3 SpecularColour { get => specularColour; }
        public float SpecConst { get => specConst; }
    }
}
