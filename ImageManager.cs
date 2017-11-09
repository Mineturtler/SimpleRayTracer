using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using GlmNet;

namespace SimpleRayTracer
{
    
    class ImageManager
    {
        private const int fileNameLength = 5;
        private const float cameraDist = 6f;
        private const int resolutionWidth = 640;
        private const int resolutionHeight = 480;
        private const int planeWidth = 120;
        private const int planeHeight = 90;

        private static Color[,] imageArray; //standardfarbe: EmptyColor

        public static void generateImage(SceneManager sManager, Camera mCamera, int imageNumber)
        {
            imageArray = new Color[resolutionWidth, resolutionHeight];
            Pixel[,] projectPlane = createProjectionPlane(sManager, mCamera);
            writeImageArray(projectPlane);
            generateImage(imageNumber);
        }

        private static void renderImage(SceneManager sManager, mat4 viewMatrix,ref Pixel pixel)
        {
            var actualColor = Color.Empty;
            foreach(vec4 point in pixel.SamplePoints)
            {
                Ray worldSpaceRay = new Ray(new vec4(new vec3(0,0,0),1), new vec4(new vec3(point),0)).transformRay(glm.inverse(viewMatrix));
                float closestT = -1;
                vec4 intersectionPoint = new vec4(0, 0, 0, 0);
                actualColor = Color.Empty;
                //evtl ObjectType zwischenspeichern, oder ID?

                foreach(ObjectType obj in sManager.ObjectList)
                {
                    Ray objectSpaceRay = worldSpaceRay.transformRay(glm.inverse(obj.TransformationMatrix));
                    if (obj.hasIntersectionPoint(objectSpaceRay))
                    {
                        float t = obj.getIntersectionParameter(objectSpaceRay);
                        
                        if ( t > 0 && (t < closestT || closestT < 0 ))
                        {
                            closestT = t;
                            intersectionPoint = obj.getIntersectionPoint(objectSpaceRay, t);
                            actualColor = Color.Green; //Expansion: Colour of Object/Triangle + Shadow (Lightsource visible?)
                        }
                    }
                }
            }
            pixel.PixelColor = actualColor;
        }

      
        private static Pixel[,] createProjectionPlane(SceneManager sManager, Camera mCamera)
        {
            Pixel[,] projecPlane = new Pixel[resolutionWidth, resolutionHeight];
            ViewPlane viewPlane = ViewPlane.createViewPlane(mCamera, resolutionWidth, resolutionHeight);

            float pixelSize = (float) viewPlane.PlaneWidth/ resolutionWidth;
            float pixelSize2 = (float)viewPlane.PlaneHeigth / resolutionHeight;

            for(int i = 0; i < resolutionWidth; i++)
            {
                int k = i - resolutionWidth / 2;
                for (int j = 0; j < resolutionHeight; j++)
                {
                    int l = j - resolutionHeight / 2;
                    vec4 pixelPosition = viewPlane.Center + k * pixelSize * viewPlane.WidthDirection - l * pixelSize * viewPlane.HeigthDirection;
                    Pixel pixel = new Pixel(pixelPosition, pixelSize, viewPlane.WidthDirection, viewPlane.HeigthDirection);
                    renderImage(sManager, viewPlane.ViewMatrix, ref pixel);
                    projecPlane[i, j] = pixel;
                }
            }
            return projecPlane;
        }

        private static void writeImageArray(Pixel[,] projectPlane)
        {
            for (int i = 0; i < resolutionWidth; i++)
                for (int j = 0; j < resolutionHeight; j++)
                    imageArray[i, j] = projectPlane[i, j].PixelColor;
        }

        private static void generateImage(int number)
        {
            int length = imageArray.GetLength(0);
            int height = imageArray.GetLength(1);
            using (Bitmap b = new Bitmap(length, height))
            {              
                for(int i = 0; i <length; i++)
                {
                    for(int j = 0; j < height; j++)
                    {
                        b.SetPixel(i,j, imageArray[i, j]);
                    }
                }
                string filePath = getDirectory() + "\\" + getFileName(number);
                b.Save(@filePath, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private static void changeColorAt(int x, int y, Color colour)
        {
            if (x > imageArray.GetUpperBound(0) || x < imageArray.GetLowerBound(0) ||
                y > imageArray.GetUpperBound(1) || y < imageArray.GetLowerBound(1))
            {
                Console.WriteLine("Exception: changeColorAt: outOfBounds: {0}, {1}", x, y);
                return;
            }
            imageArray[x, y] = colour;
        }

        private static string getDirectory()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string[] folderPath = currentDirectory.Split('\\');
            string directory = "";
            int i = 0;
            while(!folderPath[i].Equals("SimpleRayTracer"))
            {
                directory += folderPath[i] + "\\";
                i++;
            }
            directory += "SimpleRayTracer\\Images";
            Directory.CreateDirectory(directory);
            return directory;
        }

        private static string getFileName(int number)
        {
            int size = number.ToString().Length;
            string fileName= "";
            while(fileName.Length < fileNameLength - size)
            {
                fileName += "0";
            }
            fileName += number.ToString();
            fileName += ".png";
            return fileName;
        }
    }
}
