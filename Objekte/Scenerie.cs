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
        private static float shift = 0.01f;
        private const float shiftOffset = 0.05f;

        private static vec4 translateC1 = new vec4(-shift, shift, shift, 0);
        private static vec4 translateC2 = new vec4(0, shift, shift, 0);
        private static vec4 translateC3 = new vec4(shift, shift, shift, 0);
        private static vec4 translateC4 = new vec4(-shift, 0, shift, 0);
        private static vec4 translateC5 = new vec4(0, 0, shift, 0);
        private static vec4 translateC6 = new vec4(shift, 0, shift, 0);
        private static vec4 translateC7 = new vec4(-shift, -shift, shift, 0);
        private static vec4 translateC8 = new vec4(0, -shift, shift, 0);
        private static vec4 translateC9 = new vec4(shift, -shift, shift, 0);
        private static vec4 translateC10 = new vec4(-shift, shift, 0, 0);
        private static vec4 translateC11 = new vec4(0, shift, 0, 0);
        private static vec4 translateC12 = new vec4(shift, shift, 0, 0);
        private static vec4 translateC13 = new vec4(-shift, 0, 0, 0);
        private static vec4 translateC14 = new vec4(0, 0, 0, 0);
        private static vec4 translateC15 = new vec4(shift, 0, 0, 0);
        private static vec4 translateC16 = new vec4(-shift, -shift, 0, 0);
        private static vec4 translateC17 = new vec4(0, -shift, 0, 0);
        private static vec4 translateC18 = new vec4(shift, -shift, 0, 0);
        private static vec4 translateC19 = new vec4(-shift, shift, -shift, 0);
        private static vec4 translateC20 = new vec4(0, shift, -shift, 0);
        private static vec4 translateC21 = new vec4(shift, shift, -shift, 0);
        private static vec4 translateC22 = new vec4(-shift, 0, -shift, 0);
        private static vec4 translateC23 = new vec4(0, 0, -shift, 0);
        private static vec4 translateC24 = new vec4(shift, 0, -shift, 0);
        private static vec4 translateC25 = new vec4(-shift, -shift, -shift, 0);
        private static vec4 translateC26 = new vec4(0, -shift, -shift, 0);
        private static vec4 translateC27 = new vec4(shift, -shift, -shift, 0);

        private static vec4 globalRotationAxis = glm.normalize(new vec4(1, 1, 1, 0));
        private static float globalRotationAngle = 1/90f*292*2;
        private static mat3 globalRotationMatrix = Calculation.calculateRotationMatrix(globalRotationAxis, globalRotationAngle);
        private static float globalScalingFactor = 0.05f;

        private static vec4 cubeXaxis = new vec4(1, 0, 0, 0);
        private static vec4 cubeYaxis = new vec4(0, 1, 0, 0);
        private static vec4 cubeZaxis = new vec4(0, 0, 1, 0);
        private static float cubeRotationAngle = 5f; //original 5°

        private static ObjectType[] group3x3x3;
        private static ObjectType[] groupCorners;
        private static ObjectType[] groupNotCorners;
        private static ObjectType[] groupXalignmentLeft;
        private static ObjectType[] groupXalignmentMiddle;
        private static ObjectType[] groupXalignmentRight;
        private static ObjectType[] groupYalignmentBottom;
        private static ObjectType[] groupYalignmentMiddle;
        private static ObjectType[] groupYalignmentTop;
        private static ObjectType[] groupZalignmentFront;
        private static ObjectType[] groupZalignmentMiddle;
        private static ObjectType[] groupZalignmentBack;


        private Scenerie() { }

        public static void loadScene(ref SceneManager sManager)
        {
            loadLights(ref sManager);
            load3x3x3Cubes(ref sManager);
            loadRoom(ref sManager);
        }

        private static void loadLights(ref SceneManager sManager)
        {
            Light l1 = new Light(new vec4(0, 20, 0, 1));
            sManager.addLightSource(l1);
        }

        private static void load3x3x3Cubes(ref SceneManager sManager)
        {
            List<Triangle> _triangleList = Parser.getTriangleListFromFile(Constants.Filepath_objectdata, "Cube");
            //First Layer
            TriangleObject c1 = new TriangleObject(objID, translateC1, _triangleList);
            objID++;
            TriangleObject c2 = new TriangleObject(objID, translateC2, _triangleList);
            objID++;
            TriangleObject c3 = new TriangleObject(objID, translateC3, _triangleList);
            objID++;
            TriangleObject c4 = new TriangleObject(objID, translateC4, _triangleList);
            objID++;
            TriangleObject c5 = new TriangleObject(objID, translateC5, _triangleList);
            objID++;
            TriangleObject c6 = new TriangleObject(objID, translateC6, _triangleList);
            objID++;
            TriangleObject c7 = new TriangleObject(objID, translateC7, _triangleList);
            objID++;
            TriangleObject c8 = new TriangleObject(objID, translateC8, _triangleList);
            objID++;
            TriangleObject c9 = new TriangleObject(objID, translateC9, _triangleList);
            objID++;
            //Second Layer
            TriangleObject c10 = new TriangleObject(objID, translateC10, _triangleList);
            objID++;
            TriangleObject c11 = new TriangleObject(objID, translateC11, _triangleList);
            objID++;
            TriangleObject c12 = new TriangleObject(objID, translateC12, _triangleList);
            objID++;
            TriangleObject c13 = new TriangleObject(objID, translateC13, _triangleList);
            objID++;
            TriangleObject c14 = new TriangleObject(objID, translateC14, _triangleList);
            objID++;
            TriangleObject c15 = new TriangleObject(objID, translateC15, _triangleList);
            objID++;
            TriangleObject c16 = new TriangleObject(objID, translateC16, _triangleList);
            objID++;
            TriangleObject c17 = new TriangleObject(objID, translateC17, _triangleList);
            objID++;
            TriangleObject c18 = new TriangleObject(objID, translateC18, _triangleList);
            objID++;
            //Third Layer
            TriangleObject c19 = new TriangleObject(objID, translateC19, _triangleList);
            objID++;
            TriangleObject c20 = new TriangleObject(objID, translateC20, _triangleList);
            objID++;
            TriangleObject c21 = new TriangleObject(objID, translateC21, _triangleList);
            objID++;
            TriangleObject c22 = new TriangleObject(objID, translateC22, _triangleList);
            objID++;
            TriangleObject c23 = new TriangleObject(objID, translateC23, _triangleList);
            objID++;
            TriangleObject c24 = new TriangleObject(objID, translateC24, _triangleList);
            objID++;
            TriangleObject c25 = new TriangleObject(objID, translateC25, _triangleList);
            objID++;
            TriangleObject c26 = new TriangleObject(objID, translateC26, _triangleList);
            objID++;
            TriangleObject c27 = new TriangleObject(objID, translateC27, _triangleList);
            objID++;

            group3x3x3 = new TriangleObject[] { c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12, c13, c14, c15, c16, c17, c18, c19, c20, c21, c22, c23, c24, c25, c26, c27 };
            groupCorners = new TriangleObject[] { c1, c3, c7, c9, c19, c21, c25, c27 };
            groupNotCorners = new TriangleObject[] { c2, c4, c5, c6, c8, c10, c11, c12, c13, c14, c15, c16, c17, c18, c20, c22, c23, c24, c26 };

            groupXalignmentLeft = new TriangleObject[] { c1, c4, c7, c10, c13, c16, c19, c22, c25 };
            groupXalignmentMiddle = new TriangleObject[] { c2, c5, c8, c11, c14, c17, c20, c23, c26 };
            groupXalignmentRight = new TriangleObject[] { c3, c6, c9, c12, c15, c18, c21, c24, c27 };

            groupYalignmentBottom = new TriangleObject[] { c7, c8, c9, c16, c17, c18, c25, c26, c27 };
            groupYalignmentMiddle = new TriangleObject[] { c4, c5, c6, c13, c14, c15, c22, c23, c24 };
            groupYalignmentTop = new TriangleObject[] { c1, c2, c3, c10, c11, c12, c19, c20, c21 };

            groupZalignmentFront = new TriangleObject[] { c1, c2, c3, c4, c5, c6, c7, c8, c9 };
            groupZalignmentMiddle = new TriangleObject[] { c10, c11, c12, c13, c14, c15, c16, c17, c18 };
            groupZalignmentBack = new TriangleObject[] { c19, c20, c21, c22, c23, c24, c25, c26, c27 };

            sManager.addContent(group3x3x3);
            sManager.formGroup(group3x3x3);
            
            updateShiftTranslation(shiftOffset);
        }

        private static void loadRoom(ref SceneManager sManager)
        {
            var triangleList = Parser.getTriangleListFromFile(Constants.Filepath_objectdata, "Room");
            vec4 position = new vec4(0, 30, 0, 1);
            vec3 scale = new vec3(3, 3, 3);
            vec4 rotationAxis = new vec4(0, 1, 0, 1);
            float angle = 0;
            mat3 rotationMatrix = Calculation.calculateRotationMatrix(rotationAxis, angle);
            var transformationMatrix = Calculation.calculateTransformationMatrix(position, rotationMatrix, scale);
            TriangleObject room = new TriangleObject(objID, transformationMatrix, triangleList);
            objID++;

            sManager.addContent(room);
        }

        public static void updateScene(ref SceneManager sManager, int step)
        {
            updateCube(ref sManager, step);
            //updateLight(ref sManager, step);
        }

        private static void updateCube(ref SceneManager sManager, int step)
        {
               
            int firstPart = 24; //emerge cubes
            int secondPart = 40; //scale Corners
            int thirdPart = 56; //scale the Rest
            int fourthPart = 60; //do Nothing
            int fifthPart = 132; //rotate x-axis
            int sixthPart = 140; //break
            int seventhPart = 212; //rotate y-axis
            int eightPart = 220; //break
            int ninthPart = 292; //rotate z-axis
            int tenthPart = 300; //break
            int eleventhPart = 316; //descale not corners
            int twelthPart = 332; //descale corners
            int thirteenthPart = 356; //deemerge cubes

            if (step < firstPart)
            {
                translateCubes(true);
                globalRotation(ref sManager);
            }
            else if (step < secondPart)
                scaleCorners(true);
            else if (step < thirdPart)
                scaleNotCorners(true);
            else if (step < fourthPart)
            { }
            else if (step < fifthPart)
            {
                rotateXalignment();
            }
            else if (step < sixthPart)
                globalRotation(ref sManager);
            else if (step < seventhPart)
            {
                rotateYalignment();
            }
            else if (step < eightPart)
                globalRotation(ref sManager);
            else if (step < ninthPart)
            {
                rotateZalignment();
            }
            else if (step < tenthPart)
            { }
            else if (step < eleventhPart)
            {
                scaleNotCorners(false);
                globalRotation(ref sManager);
            }
            else if (step < twelthPart)
            {
                scaleCorners(false);
                globalRotation(ref sManager);
            }
            else if (step < thirteenthPart)
            {
                translateCubes(false);
                globalRotation(ref sManager);
            }

            foreach (var box in sManager.AABBList)
                box.generateHitBox();
        }
        

        private static void translateCubes(bool isEmerging)
        {
            foreach(var _o in group3x3x3)
            {
                var _transformation = getShiftMatrix(_o.IdNumber, isEmerging);
                _o.updateObject(_transformation);
            }
        }

        private static void scaleCorners(bool isScaling)
        {
            int factor = isScaling ? 1 : -1;
            vec3 _scaling = new vec3(1 + factor*globalScalingFactor);
            mat4 _transformation = Calculation.calculateTransformationMatrix(new vec4(0), mat3.identity(), _scaling);

            foreach(var _o in groupCorners)
            {
                _o.updateObject(_transformation);
            }
        }

        private static void scaleNotCorners(bool isScaling)
        {
            int factor = isScaling ? 1 : -1;
            vec3 _scaling = new vec3(1 + factor*globalScalingFactor);
            mat4 _transformation = Calculation.calculateTransformationMatrix(new vec4(0), mat3.identity(), _scaling);

            foreach (var _o in groupNotCorners)
                _o.updateObject(_transformation);
        }

        private static void globalRotation(ref SceneManager sManager)
        {
            var _aabbGroup = sManager.AABBList[0];
            var _transformation = Calculation.calculateTransformationMatrix(new vec4(0), globalRotationMatrix, new vec3(1));
            _aabbGroup.transformAABB(_transformation);
            updateTransformationAxis(_transformation);
        }

        private static void rotateXalignment()
        {
            mat3 _rotationMatrix = Calculation.calculateRotationMatrix(cubeXaxis, cubeRotationAngle);
            mat4 _transformation = Calculation.calculateTransformationMatrix(new vec4(0), _rotationMatrix, new vec3(1));
            foreach (var _o in groupXalignmentLeft)
                _o.updateObject(_transformation);
        }

        private static void rotateYalignment()
        {
            mat3 _rotationMatrixMiddle = Calculation.calculateRotationMatrix(cubeYaxis, cubeRotationAngle);
            mat3 _rotationMatrixBottom = Calculation.calculateRotationMatrix(cubeYaxis, -2*cubeRotationAngle);
            mat4 _transformationMiddle = Calculation.calculateTransformationMatrix(new vec4(0), _rotationMatrixMiddle, new vec3(1));
            mat4 _transformationBottom = Calculation.calculateTransformationMatrix(new vec4(0), _rotationMatrixBottom, new vec3(1));
            foreach (var _o in groupYalignmentMiddle)
                _o.updateObject(_transformationMiddle);
            foreach (var _o in groupYalignmentBottom)
                _o.updateObject(_transformationBottom);
        }

        private static void rotateZalignment()
        {
            mat3 _rotationFront = Calculation.calculateRotationMatrix(cubeZaxis, cubeRotationAngle);
            mat3 _rotationMiddle = Calculation.calculateRotationMatrix(cubeZaxis, 2 * cubeRotationAngle);
            mat3 _rotationBack = Calculation.calculateRotationMatrix(cubeZaxis, 3 * cubeRotationAngle);

            mat4 _transformationFront = Calculation.calculateTransformationMatrix(new vec4(0), _rotationFront, new vec3(1));
            mat4 _transformationMiddle = Calculation.calculateTransformationMatrix(new vec4(0), _rotationMiddle, new vec3(1));
            mat4 _transformationBack = Calculation.calculateTransformationMatrix(new vec4(0), _rotationBack, new vec3(1));
            
            foreach (var _o in groupZalignmentMiddle)
                _o.updateObject(_transformationMiddle);
            foreach (var _o in groupZalignmentBack)
                _o.updateObject(_transformationBack);
            foreach (var _o in groupZalignmentFront)
                _o.updateObject(_transformationFront);
        }

        private static void updateLight(ref SceneManager sManager, int step)
        {

        }

        private static void updateShiftTranslation(float shift)
        {
            translateC1 = new vec4(-shift, shift, shift, 0);
            translateC2 = new vec4(0, shift, shift, 0);
            translateC3 = new vec4(shift, shift, shift, 0);
            translateC4 = new vec4(-shift, 0, shift, 0);
            translateC5 = new vec4(0, 0, shift, 0);
            translateC6 = new vec4(shift, 0, shift, 0);
            translateC7 = new vec4(-shift, -shift, shift, 0);
            translateC8 = new vec4(0, -shift, shift, 0);
            translateC9 = new vec4(shift, -shift, shift, 0);
            translateC10 = new vec4(-shift, shift, 0, 0);
            translateC11 = new vec4(0, shift, 0, 0);
            translateC12 = new vec4(shift, shift, 0, 0);
            translateC13 = new vec4(-shift, 0, 0, 0);
            translateC14 = new vec4(0, 0, 0, 0);
            translateC15 = new vec4(shift, 0, 0, 0);
            translateC16 = new vec4(-shift, -shift, 0, 0);
            translateC17 = new vec4(0, -shift, 0, 0);
            translateC18 = new vec4(shift, -shift, 0, 0);
            translateC19 = new vec4(-shift, shift, -shift, 0);
            translateC20 = new vec4(0, shift, -shift, 0);
            translateC21 = new vec4(shift, shift, -shift, 0);
            translateC22 = new vec4(-shift, 0, -shift, 0);
            translateC23 = new vec4(0, 0, -shift, 0);
            translateC24 = new vec4(shift, 0, -shift, 0);
            translateC25 = new vec4(-shift, -shift, -shift, 0);
            translateC26 = new vec4(0, -shift, -shift, 0);
            translateC27 = new vec4(shift, -shift, -shift, 0);
        }

        private static mat4 getShiftMatrix(int id, bool isEmerging)
        {
            int factor = isEmerging ? 1 : -1;
            mat4 transformation = mat4.identity();
            vec3 scaling = new vec3(1);
            switch (id)
            {
                case 1: transformation = Calculation.calculateTransformationMatrix(factor * translateC1, mat3.identity(), scaling); break;
                case 2: transformation = Calculation.calculateTransformationMatrix(factor * translateC2, mat3.identity(), scaling); break;
                case 3: transformation = Calculation.calculateTransformationMatrix(factor * translateC3, mat3.identity(), scaling); break;
                case 4: transformation = Calculation.calculateTransformationMatrix(factor * translateC4, mat3.identity(), scaling); break;
                case 5: transformation = Calculation.calculateTransformationMatrix(factor * translateC5, mat3.identity(), scaling); break;
                case 6: transformation = Calculation.calculateTransformationMatrix(factor * translateC6, mat3.identity(), scaling); break;
                case 7: transformation = Calculation.calculateTransformationMatrix(factor * translateC7, mat3.identity(), scaling); break;
                case 8: transformation = Calculation.calculateTransformationMatrix(factor * translateC8, mat3.identity(), scaling); break;
                case 9: transformation = Calculation.calculateTransformationMatrix(factor * translateC9, mat3.identity(), scaling); break;
                case 10: transformation = Calculation.calculateTransformationMatrix(factor * translateC10, mat3.identity(), scaling); break;
                case 11: transformation = Calculation.calculateTransformationMatrix(factor * translateC11, mat3.identity(), scaling); break;
                case 12: transformation = Calculation.calculateTransformationMatrix(factor * translateC12, mat3.identity(), scaling); break;
                case 13: transformation = Calculation.calculateTransformationMatrix(factor * translateC13, mat3.identity(), scaling); break;
                case 14: transformation = Calculation.calculateTransformationMatrix(factor * translateC14, mat3.identity(), scaling); break;
                case 15: transformation = Calculation.calculateTransformationMatrix(factor * translateC15, mat3.identity(), scaling); break;
                case 16: transformation = Calculation.calculateTransformationMatrix(factor * translateC16, mat3.identity(), scaling); break;
                case 17: transformation = Calculation.calculateTransformationMatrix(factor * translateC17, mat3.identity(), scaling); break;
                case 18: transformation = Calculation.calculateTransformationMatrix(factor * translateC18, mat3.identity(), scaling); break;
                case 19: transformation = Calculation.calculateTransformationMatrix(factor * translateC19, mat3.identity(), scaling); break;
                case 20: transformation = Calculation.calculateTransformationMatrix(factor * translateC20, mat3.identity(), scaling); break;
                case 21: transformation = Calculation.calculateTransformationMatrix(factor * translateC21, mat3.identity(), scaling); break;
                case 22: transformation = Calculation.calculateTransformationMatrix(factor * translateC22, mat3.identity(), scaling); break;
                case 23: transformation = Calculation.calculateTransformationMatrix(factor * translateC23, mat3.identity(), scaling); break;
                case 24: transformation = Calculation.calculateTransformationMatrix(factor * translateC24, mat3.identity(), scaling); break;
                case 25: transformation = Calculation.calculateTransformationMatrix(factor * translateC25, mat3.identity(), scaling); break;
                case 26: transformation = Calculation.calculateTransformationMatrix(factor * translateC26, mat3.identity(), scaling); break;
                case 27: transformation = Calculation.calculateTransformationMatrix(factor * translateC27, mat3.identity(), scaling); break;
                default: break;
            }
            return transformation;
        }

        private static void updateTransformationAxis(mat4 transformation)
        {
            cubeXaxis = glm.normalize(transformation * cubeXaxis);
            cubeYaxis = glm.normalize(transformation * cubeYaxis);
            cubeZaxis = glm.normalize(transformation * cubeZaxis);

            translateC1 = (transformation * translateC1);
            translateC2 = (transformation * translateC2);
            translateC3 = (transformation * translateC3);
            translateC4 = (transformation * translateC4);
            translateC5 = (transformation * translateC5);
            translateC6 = (transformation * translateC6);
            translateC7 = (transformation * translateC7);
            translateC8 = (transformation * translateC8);
            translateC9 = (transformation * translateC9);
            translateC10 = (transformation * translateC10);
            translateC11 = (transformation * translateC11);
            translateC12 = (transformation * translateC12);
            translateC13 = (transformation * translateC13);
            translateC14 = (transformation * translateC14);
            translateC15 = (transformation * translateC15);
            translateC16 = (transformation * translateC16);
            translateC17 = (transformation * translateC17);
            translateC18 = (transformation * translateC18);
            translateC19 = (transformation * translateC19);
            translateC20 = (transformation * translateC20);
            translateC21 = (transformation * translateC21);
            translateC22 = (transformation * translateC22);
            translateC23 = (transformation * translateC23);
            translateC24 = (transformation * translateC24);
            translateC25 = (transformation * translateC25);
            translateC26 = (transformation * translateC26);
            translateC27 = (transformation * translateC27);
        }


    }
}
