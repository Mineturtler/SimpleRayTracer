using GlmNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRayTracer.Objekte
{
    class Scenerie
    {
        private static int objID = 1;
        private const float shift = 1.2f;
        private static vec3 rotationAxisCube = new vec3(-0.5f, 1.0f, 0.5f);
        private static vec3 rotationAxisSphere = new vec3(0, 1, 0);
        private const float rotationAngle = 1.0f;
        private const float radius1 = 6f;
        private const float radius2 = 3f;

        private Scenerie() { }

        public static void loadScene(ref SceneManager sManager)
        {
            loadLights(ref sManager);
            load3x3x3Cubes(ref sManager, shift);
            loadSpheres(ref sManager);
            loadRoom(ref sManager);
        }

        private static void loadLights(ref SceneManager sManager)
        {
            Light l1 = new Light(new vec4(0, 30 , -0, 1));
            sManager.addLightSource(l1);
        }

        private static void load3x3x3Cubes(ref SceneManager sManager, float shift)
        {
            //First Layer
            vec3 position = new vec3(-1 * shift, shift, shift);
            TriangleObject c1 = new TriangleObject(objID, position, Constants.Filepath_objectdata, "Cube");
            position = new vec3(0, shift, shift);
            TriangleObject c2 = new TriangleObject(objID, position, Constants.Filepath_objectdata, "Cube");
            position = new vec3(shift, shift, shift);
            TriangleObject c3 = new TriangleObject(objID, position, Constants.Filepath_objectdata, "Cube");
            position = new vec3(-shift, 0, shift);
            TriangleObject c4 = new TriangleObject(objID, position, Constants.Filepath_objectdata, "Cube");
            position = new vec3(0, 0, shift);
            TriangleObject c5 = new TriangleObject(objID, position, Constants.Filepath_objectdata, "Cube");
            position = new vec3(shift, 0, shift);
            TriangleObject c6 = new TriangleObject(objID, position, Constants.Filepath_objectdata, "Cube");
            position = new vec3(-shift, -shift, shift);
            TriangleObject c7 = new TriangleObject(objID, position, Constants.Filepath_objectdata, "Cube");
            position = new vec3(0, -shift, shift);
            TriangleObject c8 = new TriangleObject(objID, position, Constants.Filepath_objectdata, "Cube");
            position = new vec3(shift, -shift, shift);
            TriangleObject c9 = new TriangleObject(objID, position, Constants.Filepath_objectdata, "Cube");
            //Second Layer
            position = new vec3(-shift, shift, 0);
            TriangleObject c10 = new TriangleObject(objID, position, Constants.Filepath_objectdata, "Cube");
            position = new vec3(0, shift, 0);
            TriangleObject c11 = new TriangleObject(objID, position, Constants.Filepath_objectdata, "Cube");
            position = new vec3(shift, shift, 0);
            TriangleObject c12 = new TriangleObject(objID, position, Constants.Filepath_objectdata, "Cube");
            position = new vec3(-shift, 0, 0);
            TriangleObject c13 = new TriangleObject(objID, position, Constants.Filepath_objectdata, "Cube");
            position = new vec3(0, 0, 0);
            TriangleObject c14 = new TriangleObject(objID, position, Constants.Filepath_objectdata, "Cube");
            position = new vec3(shift, 0, 0);
            TriangleObject c15 = new TriangleObject(objID, position, Constants.Filepath_objectdata, "Cube");
            position = new vec3(-shift, -shift, 0);
            TriangleObject c16 = new TriangleObject(objID, position, Constants.Filepath_objectdata, "Cube");
            position = new vec3(0, -shift, 0);
            TriangleObject c17 = new TriangleObject(objID, position, Constants.Filepath_objectdata, "Cube");
            position = new vec3(shift, -shift, 0);
            TriangleObject c18 = new TriangleObject(objID, position, Constants.Filepath_objectdata, "Cube");
            //Third Layer
            position = new vec3(-shift, shift, -shift);
            TriangleObject c19 = new TriangleObject(objID, position, Constants.Filepath_objectdata, "Cube");
            position = new vec3(0, shift, -shift);
            TriangleObject c20 = new TriangleObject(objID, position, Constants.Filepath_objectdata, "Cube");
            position = new vec3(shift, shift, -shift);
            TriangleObject c21 = new TriangleObject(objID, position, Constants.Filepath_objectdata, "Cube");
            position = new vec3(-shift, 0, -shift);
            TriangleObject c22 = new TriangleObject(objID, position, Constants.Filepath_objectdata, "Cube");
            position = new vec3(0, 0, -shift);
            TriangleObject c23 = new TriangleObject(objID, position, Constants.Filepath_objectdata, "Cube");
            position = new vec3(shift, 0, -shift);
            TriangleObject c24 = new TriangleObject(objID, position, Constants.Filepath_objectdata, "Cube");
            position = new vec3(-shift, -shift, -shift);
            TriangleObject c25 = new TriangleObject(objID, position, Constants.Filepath_objectdata, "Cube");
            position = new vec3(0, -shift, -shift);
            TriangleObject c26 = new TriangleObject(objID, position, Constants.Filepath_objectdata, "Cube");
            position = new vec3(shift, -shift, -shift);
            TriangleObject c27 = new TriangleObject(objID, position, Constants.Filepath_objectdata, "Cube");
            TriangleObject c3x3x3 = TriangleObject.mergeTriangleObjects(1, c1, c2, c3, c4, c5, c6, c7, c8, c9, 
                                                                               c10, c11, c12, c13, c14, c15, c16, c17, c18, 
                                                                               c19, c20, c21, c22, c23, c24, c25, c26, c27);
            objID++;
            sManager.addContent(c3x3x3);
        }

        private static void loadRoom(ref SceneManager sManager)
        {
            vec3 position = new vec3(0, 30, 0);
            vec3 scale = new vec3(3, 3, 3);
            TriangleObject room = new TriangleObject(objID, position, scale, Constants.Filepath_objectdata, "Room");
            objID++;

            sManager.addContent(room);
        }

        private static void loadSpheres(ref SceneManager sManager)
        {
            vec3 aC = new vec3(1.0f, 0.88f, 0.25f);
            vec3 sC = new vec3(1.0f,0.913f,0.8f);
            float m = 20f;
            MaterialProperty l1 = new MaterialProperty(aC, aC, sC, m);
            Objekte.Sphere s1 = new Objekte.Sphere(objID, new vec3(radius1, 0, 0), l1);
            objID++;

            aC = new vec3(0.549f,0.471f,0.325f);
            sC = new vec3(0.456f, 0.398f, 0.325f);
            m = 25f;
            MaterialProperty l2 = new MaterialProperty(aC, aC, sC, m);
            Objekte.Sphere s2 = new Objekte.Sphere(objID, new vec3(radius2, 0, 0), l2);
            objID++;
            

            sManager.addContent(s1);
            sManager.addContent(s2);
        }

        public static void updateScene(ref SceneManager sManager, int step)
        {
            updateSpheres(ref sManager, step);
            updateCube(ref sManager, step);
            updateLight(ref sManager, step);
        }

        private static void updateCube(ref SceneManager sManager, int step)
        {
            //3x3x3 cube has id 1
            if (step < 30)
                emerge3x3x3Cube(ref sManager, step, 1);
            else if (step < 120)
                rotate3x3x3Cube(ref sManager, 1);
            else
                reduce3x3x3Cube(ref sManager, step, 1);
        }

        private static void reduce3x3x3Cube(ref SceneManager sManager, int step, int id)
        {
            sManager.ObjectList.Remove(id);
            load3x3x3Cubes(ref sManager, shift - (step-120) * 0.04f);
        }

        private static void rotate3x3x3Cube(ref SceneManager sManager, int id)
        {
            ObjectType _object;
            if (sManager.ObjectList.TryGetValue(id, out _object))
                _object.rotateAroundAxisWithAngle(rotationAxisCube, rotationAngle);
        }

        private static void emerge3x3x3Cube(ref SceneManager sManager, int step, int id)
        {
            sManager.ObjectList.Remove(id);
            load3x3x3Cubes(ref sManager, step * 0.04f);
        }

        private static void updateSpheres(ref SceneManager sManager, int step)
        {
            //Spheres have ID 2 and 3
            ObjectType _object;
            if (sManager.ObjectList.TryGetValue(2, out _object))
                _object.rotateAroundAxisWithAngle(rotationAxisSphere, -rotationAngle*0.5f);
            if (sManager.ObjectList.TryGetValue(3, out _object))
                _object.rotateAroundAxisWithAngle(rotationAxisSphere, rotationAngle);
        }

        private static void updateLight(ref SceneManager sManager, int step)
        {
            
        }
    }
}
