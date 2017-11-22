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
            vec3 cameraPosition = new vec3(0, 0 , -5);
            vec3 cameraOrientation = new vec3(0, 0, 0);
            Camera mCamera = new Camera(cameraPosition, cameraOrientation);

            SceneManager sManager = SceneManager.createSceneManager();
            loadSpheres(ref sManager);
            loadTriangleObjects(ref sManager);


            Console.WriteLine("Create Image");
            for (int i = 0; i < 1; i++)
            {
                ImageManager.generateImage(sManager, mCamera, i);
                vec4 _pos = new vec4(cameraPosition.x, (float) Math.Sqrt(Math.Abs((cameraPosition.z+i)* (cameraPosition.z + i) - cameraPosition.z)), cameraPosition.z + i, 1);
                mCamera.Position = _pos;
            }

            Console.ReadKey();
        }

        private static void loadTriangleObjects(ref SceneManager sManager)
        {
            
        }

        private static void loadSpheres(ref SceneManager sManager)
        {
            vec3 aC = new vec3(0, 1, 0);
            vec3 sC = new vec3(1, 1, 1);
            float m = 5f;
            MaterialProperty l1 = new MaterialProperty(aC,aC,sC,m);
            Objekte.Sphere sphere = new Objekte.Sphere(1, new vec3(0, 0, 0),l1); //GRÜN

            aC = new vec3(1, 0, 0);
            MaterialProperty l2 = new MaterialProperty(aC, aC, sC, m);
            Objekte.Sphere s1 = new Objekte.Sphere(2, new vec3(-2.3f, 0, 0), l2); //ROT

            aC = new vec3(0, 0, 1);
            MaterialProperty l3 = new MaterialProperty(aC, aC, sC, m);
            Objekte.Sphere s2 = new Objekte.Sphere(3, new vec3(2.3f, 0, 0), l3); //BLAU 

            aC = new vec3(1, 0, 1);
            MaterialProperty l4 = new MaterialProperty(aC, aC, sC, m);
            Objekte.Sphere s3 = new Objekte.Sphere(4, new vec3(0, 2.3f, 0), l4); //LILA

            aC = new vec3(1, 1, 0);
            MaterialProperty l5 = new MaterialProperty(aC, aC, sC, m);
            Objekte.Sphere s4 = new Objekte.Sphere(5, new vec3(0, -2.3f, 0), l5); //GELBS

            Light light = new Light(new vec4(0, 0, -2.5f, 1));
            sManager.addLightSource(light);

            sManager.addContent(sphere);
            sManager.addContent(s1);
            sManager.addContent(s2);
            sManager.addContent(s3);
            sManager.addContent(s4);
        }
  }
}
