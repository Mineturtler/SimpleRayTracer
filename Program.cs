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
        private static int objID = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Create Manager with 640x480");
            vec3 cameraPosition = new vec3(2,2,-2);
            vec3 cameraOrientation = new vec3(0, 0, 0);
            Camera mCamera = new Camera(cameraPosition, cameraOrientation);

            SceneManager sManager = SceneManager.createSceneManager();
            //loadSpheres(ref sManager);
            loadTriangleObjects(ref sManager);
            //triangleTest(ref sManager);
            loadLights(ref sManager);


            Console.WriteLine("Create Image");
            for (int i = 0; i < 1; i++)
            {
                ImageManager.generateImage(sManager, mCamera, i);
                vec4 _pos = new vec4(cameraPosition.x, cameraPosition.y, cameraPosition.z + i, 1);
                mCamera.Position = _pos;
            }

            Console.ReadKey();
        }

        private static void loadLights(ref SceneManager sManager)
        {
            Light light = new Light(new vec4(2, 2, -2.5f, 1));
            sManager.addLightSource(light);
        }

        private static void loadTriangleObjects(ref SceneManager sManager)
        {
            TriangleObject cube = new TriangleObject(objID, mat4.identity(), Constants.Filepath_objectdata, "Cube");


            vec4 p0 = new vec4(-4, -2, 0, 1);
            vec4 p1 = new vec4(4, -2, 2, 1);
            vec4 p2 = new vec4(0, 4, -1, 1);
            objID++;

            vec3 aC = new vec3(0, 1, 0);
            vec3 sC = new vec3(1, 1, 1);
            float m = 20f;
            MaterialProperty l1 = new MaterialProperty(aC, aC, sC, m);
            Triangle t = new Triangle(p0, p1, p2, l1);
            TriangleObject triangle = new TriangleObject(objID, t);
            objID++;

            sManager.addContent(cube);
            sManager.addContent(triangle);
        }

        private static void loadSpheres(ref SceneManager sManager)
        {
            vec3 aC = new vec3(0, 1, 0);
            vec3 sC = new vec3(1, 1, 1);
            float m = 5f;
            MaterialProperty l1 = new MaterialProperty(aC,aC,sC,m);
            Objekte.Sphere sphere = new Objekte.Sphere(objID, new vec3(0, 0, 0),l1); //GRÜN
            objID++;

            aC = new vec3(1, 0, 0);
            MaterialProperty l2 = new MaterialProperty(aC, aC, sC, m);
            Objekte.Sphere s1 = new Objekte.Sphere(objID, new vec3(-2.3f, 0, 0), l2); //ROT
            objID++;

            aC = new vec3(0, 0, 1);
            MaterialProperty l3 = new MaterialProperty(aC, aC, sC, m);
            Objekte.Sphere s2 = new Objekte.Sphere(objID, new vec3(2.3f, 0, 0), l3); //BLAU 
            objID++;

            aC = new vec3(1, 0, 1);
            MaterialProperty l4 = new MaterialProperty(aC, aC, sC, m);
            Objekte.Sphere s3 = new Objekte.Sphere(objID, new vec3(0, 2.3f, 0), l4); //LILA
            objID++;

            aC = new vec3(1, 1, 0);
            MaterialProperty l5 = new MaterialProperty(aC, aC, sC, m);
            Objekte.Sphere s4 = new Objekte.Sphere(objID, new vec3(0, -2.3f, 0), l5); //GELB
            objID++;

            sManager.addContent(sphere);
            sManager.addContent(s1);
            sManager.addContent(s2);
            sManager.addContent(s3);
            sManager.addContent(s4);
        }

        private static void triangleTest(ref SceneManager sManager)
        {
            var mat = new MaterialProperty(new vec3(0.5f, 0.8f, 0.5f));

            var v1 = new vec4(1, -1, -1, 1);
            var v2 = new vec4(1, -1, 1, 1);
            var v3 = new vec4(-1, -1, 1, 1);
            var v4 = new vec4(-1, -1, -1, 1);
            var v5 = new vec4(1, 1, -1, 1);
            var v6 = new vec4(1, 1, 1, 1);
            var v7 = new vec4(-1, 1, 1, 1);
            var v8 = new vec4(-1, 1, -1, 1);

            var vn1 = new vec4(0, -1, 0, 0);
            var vn2 = new vec4(0, 1, 0, 0);
            var vn3 = new vec4(1, 0, 0, 0);
            var vn4 = new vec4(0, 0, 1, 0);
            var vn5 = new vec4(-1, 0, 0, 0);
            var vn6 = new vec4(0, 0, -1, 0);

            var t1 = new Triangle(v2, v3, v4, vn1, mat);
            var t2 = new Triangle(v8, v7, v6, vn2, mat);
            var t3 = new Triangle(v1, v5, v6, vn3, mat);
            var t4 = new Triangle(v2, v6, v7, vn4, mat);
            var t5 = new Triangle(v7, v8, v4, vn5, mat);
            var t6 = new Triangle(v1, v4, v8, vn6, mat);
            var t7 = new Triangle(v1, v2, v4, vn1, mat);
            var t8 = new Triangle(v5, v8, v6, vn2, mat);
            var t9 = new Triangle(v2, v1, v6, vn3, mat);
            var t10 = new Triangle(v3, v2, v7, vn4, mat);
            var t11 = new Triangle(v3, v7, v4, vn5, mat);
            var t12 = new Triangle(v5, v1, v8, vn6, mat);

            
            var tO1 = new TriangleObject(objID, t1); objID++;
            var tO2 = new TriangleObject(objID, t2); objID++;
            var tO3 = new TriangleObject(objID, t3); objID++;
            var tO4 = new TriangleObject(objID, t4); objID++;
            var tO5 = new TriangleObject(objID, t5); objID++;
            var tO6 = new TriangleObject(objID, t6); objID++;
            var tO7 = new TriangleObject(objID, t7); objID++;
            var tO8 = new TriangleObject(objID, t8); objID++;
            var tO9 = new TriangleObject(objID, t9); objID++;
            var tO10 = new TriangleObject(objID, t10); objID++;
            var tO11 = new TriangleObject(objID, t11); objID++;
            var tO12 = new TriangleObject(objID, t12); objID++;

            sManager.addContent(tO1);
            sManager.addContent(tO2);
            sManager.addContent(tO3);
            sManager.addContent(tO4);
            sManager.addContent(tO5);
            sManager.addContent(tO6);
            sManager.addContent(tO7);
            sManager.addContent(tO8);
            sManager.addContent(tO9);
            sManager.addContent(tO10);
            sManager.addContent(tO11);
            sManager.addContent(tO12);




        }
    }
}
