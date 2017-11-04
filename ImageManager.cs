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
            generateImage(imageNumber);
        }

        private static void renderImage(SceneManager sManager, Camera mCamera, ref Pixel pixel)
        {
            foreach(vec4 point in pixel.SamplePoints)
            {
                Ray ray = new Ray(mCamera.Position, point - mCamera.Position);
                float closestT = -1;
                var actualColor = Color.Empty;

                foreach (Tuple<ObjectType,mat4> obj in sManager.ObjectList)
                {
                    float t = obj.Item1.getIntersectionPoint(ray).w;
                    if(t<closestT || closestT < 0)
                    {
                        closestT = t;
                        
                    }
                
                   
                }
            }
            
            //createProjectionPlane, Grid
            //foreach(Pixel p : projectionGrid)
            //generateRay durch p
            //foreach(ObjectType type : sManager.List)
            //intersect Ray mit type -> intersectionPoint
            //colour = getColorAt(type.getColorAt(intersectionPoint)
            //changeColourAt(p.Position.x, p.Position.y, colour)

        }

        private static Pixel[,] createProjectionPlane(SceneManager sManager, Camera mCamera)
        {
            Pixel[,] projecPlane = new Pixel[resolutionWidth, resolutionHeight];

            mat4 inverse = glm.inverse(mCamera.Transformation);
            vec4 norm = glm.normalize(inverse * mCamera.Orientation);
            vec4 nPos = inverse * mCamera.Position;
            vec4 center = nPos + cameraDist * norm;

            float pixelSize = (float) planeWidth/resolutionWidth;
            

            for (int i = 0; i < resolutionWidth; i++)
            {
                int k = i - (int) (resolutionWidth / 2);
                for (int j = 0; j < resolutionHeight; j++)
                {
                    int l = j - (int) (resolutionHeight / 2);
                    vec4 pixelPosition = new vec4(center.x + k * pixelSize, center.y + l * pixelSize, center.z, center.w);
                    Pixel pixel = new Pixel(pixelPosition, pixelSize);
                    renderImage(sManager, mCamera, ref pixel);
                    projecPlane[i, j] = pixel;


                }
            }
            return projecPlane;
        }
        
        //Für Testzwecke
        private static void generateAbritaryImage()
        {
            Random rnd = new Random();
            for(int i = 0; i < 500; i++)
            {
                int x = rnd.Next(0, 640);
                int y = rnd.Next(0, 480);

                int r = rnd.Next(0, 255);
                int g = rnd.Next(0, 255);
                int b = rnd.Next(0, 255);
                Color cl = Color.FromArgb(r,g,b);
                
                changeColorAt(x, y, cl);
            }
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
