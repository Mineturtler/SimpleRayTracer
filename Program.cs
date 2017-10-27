using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;

namespace SimpleRayTracer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create Manager with 640x480");
            vec3 cameraPosition = new vec3(0, 0, 0);
            vec3 cameraDirection = new vec3(0, 0, 1);

            SceneManager sManager = SceneManager.createSceneManager(cameraPosition, cameraDirection);
            loadContent(ref sManager);

            ImageManager iManager = ImageManager.createImageManager(640, 480, sManager);


            Console.WriteLine("Create Image");
            for(int i = 0; i < 60; i++)
                iManager.generateImage(i);

            Console.ReadKey();
        }

        private static void loadContent(ref SceneManager sManager)
        {
            Objekte.Sphere sphere = new Objekte.Sphere();
            sManager.addContent(sphere, new vec3(0, 0, 4));
        }
  }
}
