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

        public Pixel(vec4 position, float size)
        {
            this.position = position;
            pixelSize = size;
            pixelColor = Color.White;
            generateSamplePoints();
        }

        private void generateSamplePoints()
        {
            samplePoints.Add(new vec4(position.x + pixelSize / 2, position.y + pixelSize / 2, 
                                      position.z, position.w));
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
