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
            ImageManager iManager = ImageManager.createSceneManager(640, 480);
            SceneManager sManager = SceneManager.createSceneManager();

            Console.WriteLine("Create Image");
            for(int i = 0; i < 60; i++)
                iManager.generateImage(i);

            Console.ReadKey();
        }
    }
}
