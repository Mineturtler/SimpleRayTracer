using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRayTracer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create Manager with 640x480");
            ImageManager manager = ImageManager.createSceneManager(640, 480);


            Console.WriteLine("Create Image");
            for(int i = 0; i < 60; i++)
                manager.generateImage(i);

            Console.ReadKey();
        }
    }
}
