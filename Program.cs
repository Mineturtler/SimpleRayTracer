using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;
using System.Drawing;

namespace SimpleRayTracer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create Manager with 640x480");
            vec3 cameraPosition = new vec3(0, 0, -20);
            vec3 cameraOrientation = new vec3(0, 0, 0);
            Camera mCamera = new Camera(cameraPosition, cameraOrientation);

            SceneManager sManager = SceneManager.createSceneManager();
            loadContent(ref sManager);

            Console.WriteLine("Create Image");
            for (int i = 0; i < 1; i++)
            {
                ImageManager.generateImage(sManager, mCamera, i);
                mCamera.Position = new vec4(cameraPosition.x, cameraPosition.y, cameraPosition.z + i, 1);
            }

            Console.ReadKey();
        }

        private static void loadContent(ref SceneManager sManager)
        {
            Objekte.Sphere sphere = new Objekte.Sphere(1, new vec3(0, 0, 0));
            //Objekte.Sphere sphere1 = new Objekte.Sphere(2, new vec3(0, 0, 0));
            sManager.addContent(sphere);
        }
  }
}
