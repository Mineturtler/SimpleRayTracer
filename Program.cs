using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;
using System.Drawing;
using SimpleRayTracer.Objekte;

namespace SimpleRayTracer
{
    class Program
    {
        private static int noOfAnimations = 360;

        static void Main(string[] args)
        {
            Console.WriteLine("Create Manager with 640x480");
            var time = DateTime.Now;

            vec3 cameraPosition = new vec3(12f, 10, -6f);
            vec3 cameraOrientation = new vec3(0, 0, 0);
            Camera mCamera = new Camera(cameraPosition, cameraOrientation);

            SceneManager sManager = SceneManager.createSceneManager();
            Console.Write("Loading Scene...");
            Scenerie.loadScene(ref sManager);
            Console.WriteLine(": {0}", DateTime.Now - time);
            
            Console.WriteLine("Create Image");
            for (int i = 0; i < noOfAnimations; i++)
            {
                ImageManager.generateImage(sManager, mCamera, i);
                Scenerie.updateScene(ref sManager, i);
                Console.WriteLine("Image {0} rendered in {1}", i, DateTime.Now - time);
            }
            Console.WriteLine("Needed time: {0}", DateTime.Now - time);
            Console.WriteLine("Done");
            Console.ReadKey();
        }
        
    }
}
