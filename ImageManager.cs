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

        private const int _resoWidth = 640;
        private const int _resoHeight = 480;

        private static Color[,] imageArray; //standardfarbe: EmptyColor
        
        public static void generateImage(SceneManager _sManager, Camera _c, int imageId)
        {
            imageArray = RenderContext.renderScene(_sManager, _c, _resoWidth, _resoHeight);
            generateImage(imageId);
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
