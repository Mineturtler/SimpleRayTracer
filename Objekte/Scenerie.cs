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
            //loadRoom(ref sManager);
        }

        private static void loadLights(ref SceneManager sManager)
        {
            Light l1 = new Light(new vec4(0, 30 , -0, 1));
            sManager.addLightSource(l1);
        }

        private static void load3x3x3Cubes(ref SceneManager sManager, float shift)
        {
            List<Triangle> _triangleList = Parser.getTriangleListFromFile(Constants.Filepath_objectdata, "Cube");

            
            //First Layer
            vec3 position = new vec3(-1 * shift, shift, shift);
            TriangleObject c1 = new TriangleObject(objID, position, _triangleList);
            objID++;
            position = new vec3(0, shift, shift);
            TriangleObject c2 = new TriangleObject(objID, position, _triangleList);
            objID++;
            position = new vec3(shift, shift, shift);
            TriangleObject c3 = new TriangleObject(objID, position, _triangleList);
            objID++;
            position = new vec3(-shift, 0, shift);
            TriangleObject c4 = new TriangleObject(objID, position, _triangleList);
            objID++;
            position = new vec3(0, 0, shift);
            TriangleObject c5 = new TriangleObject(objID, position, _triangleList);
            objID++;
            position = new vec3(shift, 0, shift);
            TriangleObject c6 = new TriangleObject(objID, position, _triangleList);
            objID++;
            position = new vec3(-shift, -shift, shift);
            TriangleObject c7 = new TriangleObject(objID, position, _triangleList);
            objID++;
            position = new vec3(0, -shift, shift);
            TriangleObject c8 = new TriangleObject(objID, position, _triangleList);
            objID++;
            position = new vec3(shift, -shift, shift);
            TriangleObject c9 = new TriangleObject(objID, position, _triangleList);
            objID++;
            //Second Layer
            position = new vec3(-shift, shift, 0);
            TriangleObject c10 = new TriangleObject(objID, position, _triangleList);
            objID++;
            position = new vec3(0, shift, 0);
            TriangleObject c11 = new TriangleObject(objID, position, _triangleList);
            objID++;
            position = new vec3(shift, shift, 0);
            TriangleObject c12 = new TriangleObject(objID, position, _triangleList);
            objID++;
            position = new vec3(-shift, 0, 0);
            TriangleObject c13 = new TriangleObject(objID, position, _triangleList);
            objID++;
            position = new vec3(0, 0, 0);
            TriangleObject c14 = new TriangleObject(objID, position, _triangleList);
            objID++;
            position = new vec3(shift, 0, 0);
            TriangleObject c15 = new TriangleObject(objID, position, _triangleList);
            objID++;
            position = new vec3(-shift, -shift, 0);
            TriangleObject c16 = new TriangleObject(objID, position, _triangleList);
            objID++;
            position = new vec3(0, -shift, 0);
            TriangleObject c17 = new TriangleObject(objID, position, _triangleList);
            objID++;
            position = new vec3(shift, -shift, 0);
            TriangleObject c18 = new TriangleObject(objID, position, _triangleList);
            objID++;
            //Third Layer
            position = new vec3(-shift, shift, -shift);
            TriangleObject c19 = new TriangleObject(objID, position, _triangleList);
            objID++;
            position = new vec3(0, shift, -shift);
            TriangleObject c20 = new TriangleObject(objID, position, _triangleList);
            objID++;
            position = new vec3(shift, shift, -shift);
            TriangleObject c21 = new TriangleObject(objID, position, _triangleList);
            objID++;
            position = new vec3(-shift, 0, -shift);
            TriangleObject c22 = new TriangleObject(objID, position, _triangleList);
            objID++;
            position = new vec3(0, 0, -shift);
            TriangleObject c23 = new TriangleObject(objID, position, _triangleList);
            objID++;
            position = new vec3(shift, 0, -shift);
            TriangleObject c24 = new TriangleObject(objID, position, _triangleList);
            objID++;
            position = new vec3(-shift, -shift, -shift);
            TriangleObject c25 = new TriangleObject(objID, position, _triangleList);
            objID++;
            position = new vec3(0, -shift, -shift);
            TriangleObject c26 = new TriangleObject(objID, position, _triangleList);
            objID++;
            position = new vec3(shift, -shift, -shift);
            TriangleObject c27 = new TriangleObject(objID, position, _triangleList);
            objID++;

            sManager.addContent(c1);
            sManager.addContent(c2);
            sManager.addContent(c3);
            sManager.addContent(c4);
            sManager.addContent(c5);
            sManager.addContent(c6);
            sManager.addContent(c7);
            sManager.addContent(c8);
            sManager.addContent(c9);
            sManager.addContent(c10);
            sManager.addContent(c11);
            sManager.addContent(c12);
            sManager.addContent(c13);
            sManager.addContent(c14);
            sManager.addContent(c15);
            sManager.addContent(c16);
            sManager.addContent(c17);
            sManager.addContent(c18);
            sManager.addContent(c19);
            sManager.addContent(c20);
            sManager.addContent(c21);
            sManager.addContent(c22);
            sManager.addContent(c23);
            sManager.addContent(c24);
            sManager.addContent(c25);
            sManager.addContent(c26);
            sManager.addContent(c27);
            
        }

        private static void loadRoom(ref SceneManager sManager)
        {
            var triangleList = Parser.getTriangleListFromFile(Constants.Filepath_objectdata, "Room");
            vec3 position = new vec3(0, 30, 0);
            vec3 scale = new vec3(3, 3, 3);
            mat3 rotationMatrix = mat3.identity();
            var transformationMatrix = Calculation.calculateTransformationMatrix(position, rotationMatrix, scale);
            TriangleObject room = new TriangleObject(objID, transformationMatrix, triangleList);
            objID++;

            sManager.addContent(room);
        }

        public static void updateScene(ref SceneManager sManager, int step)
        {
            //updateSpheres(ref sManager, step);
            //updateCube(ref sManager, step);
            //updateLight(ref sManager, step);
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
            /*
            ObjectType _object;
            if (sManager.ObjectList.TryGetValue(id, out _object))
                _object.rotateAroundAxisWithAngle(rotationAxisCube, rotationAngle);
             /**/
        }

        private static void emerge3x3x3Cube(ref SceneManager sManager, int step, int id)
        {
            sManager.ObjectList.Remove(id);
            load3x3x3Cubes(ref sManager, step * 0.04f);
        }

        private static void updateSpheres(ref SceneManager sManager, int step)
        {
            //Spheres have ID 2 and 3
            /*
            ObjectType _object;
            if (sManager.ObjectList.TryGetValue(2, out _object))
                _object.rotateAroundAxisWithAngle(rotationAxisSphere, -rotationAngle*0.5f);
            if (sManager.ObjectList.TryGetValue(3, out _object))
                _object.rotateAroundAxisWithAngle(rotationAxisSphere, rotationAngle);
            /**/
        }

        private static void updateLight(ref SceneManager sManager, int step)
        {
            
        }
    }
}
