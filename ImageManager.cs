using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace SimpleRayTracer
{
    
    class ImageManager
    {
        private const int fileNameLength = 5;
        private Color[,] imageArray; //standardfarbe: EmptyColor

        private ImageManager(int imageWidth, int imageHeigth)
        {
            imageArray = new Color[imageWidth, imageHeigth];
        }

        public static ImageManager createSceneManager(int imageWidth, int imageHeight)
        {
            return new ImageManager(imageWidth, imageHeight);
        }

        //Für Testzwecke
        private void generateAbritaryImage()
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

        public void generateImage(int number)
        {
            generateAbritaryImage();
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

        public void changeColorAt(int x, int y, Color colour)
        {
            if (x > imageArray.GetUpperBound(0) || x < imageArray.GetLowerBound(0) ||
                y > imageArray.GetUpperBound(1) || y < imageArray.GetLowerBound(1))
            {
                Console.WriteLine("Exception: changeColorAt: outOfBounds: {0}, {1}", x, y);
                return;
            }
            imageArray[x, y] = colour;
        }

        private string getDirectory()
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

        private string getFileName(int number)
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
