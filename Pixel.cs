using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;
using System.Drawing;

namespace SimpleRayTracer
{
    class Pixel
    {
        private vec4 position;
        private float pixelSize;
        private Color pixelColor;
        private List<vec4> samplePoints = new List<vec4>();
        private vec4 right;
        private vec4 down;

        public Pixel(vec4 position, float size, vec4 right, vec4 down)
        {
            this.position = position;
            pixelSize = size;
            pixelColor = Color.White;
            this.right = right;
            this.down = down;
            generateSamplePoints(right, down);
        }

        private void generateSamplePoints(vec4 right, vec4 down)
        {
            //Expansion: Implement Sampling algorithm
            vec4 samplePoint = position + pixelSize / 2 * (right + down);

            samplePoints.Add(samplePoint);
        }

        public List<vec4> SamplePoints
        {
            get { return samplePoints; }
        }

        public Color PixelColor
        {
            get { return pixelColor; }
            set { this.pixelColor = value; }
        }

        public vec4 Position
        {
            get { return position; }
        }

        public float PixelSize
        {
            get { return pixelSize; } 
        }

    }
}
