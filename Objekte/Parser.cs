using GlmNet;
using SimpleRayTracer.Objekte;
using System;
using System.Collections.Generic;
using System.IO;

namespace SimpleRayTracer
{
    class Parser
    {
        private const string VERTEX = "v";
        private const string VERTEXNORMAL = "vn";
        private const string VERTEXTEXTURE = "vt";
        private const string OBJECTNAME = "o";
        private const string GROUP = "g";
        private const string MATERIAL_USAGE = "usemtl";
        private const string TRIANGLEGROUPS = "f";
        private const string COMMENTARY = "#";
        private const int NULLPLAN = 2;

        private const string AMBIENTCOLOR = "Ka";
        private const string DIFFUSECOLOR = "Kd";
        private const string SPECULARCOLOR = "Ks";
        private const string SPECULAREXPONENT = "Ns";
        private const string MATERIAL_DECLARATION = "newmtl";

        private Parser() { }

        public static List<Triangle> getTriangleListFromFile(string filePath, string objName, string mtlName)
        {
            string pathObject = filePath + objName + ".obj";
            string pathMaterial = filePath + mtlName + ".mtl";

            return getTriangleList(pathObject, getMaterialList(pathMaterial));
        }
        public static List<Triangle> getTriangleListFromFile(string filePath, string name)
        {
            string pathObject = filePath + name + ".obj";
            string pathMaterial = filePath + name + ".mtl";

            return getTriangleList(pathObject, getMaterialList(pathMaterial));
        }

        private static List<MaterialProperty> getMaterialList(string filePath)
        {
            List<MaterialProperty> _materialList = new List<MaterialProperty>();
            using (StreamReader _sMaterial = new StreamReader(filePath))
            {
                string line;
                string _materialName = "";
                float _mNs = 0;
                vec3 _mKa = new vec3();
                vec3 _mKd = new vec3();
                vec3 _mKs = new vec3();

                while ((line = _sMaterial.ReadLine()) != null)
                {
                    if (line.StartsWith(COMMENTARY))
                        continue;
                    else if (line.StartsWith(MATERIAL_DECLARATION))
                    {
                        if (!_materialName.Equals(""))
                            _materialList.Add(new MaterialProperty(_mKa, _mKd, _mKs, _mNs, _materialName));
                        _materialName = readMaterialName(line);
                    }
                    else if (line.StartsWith(SPECULAREXPONENT))
                    {
                        _mNs = readSpecularExponent(line);
                    }
                    else if (line.StartsWith(AMBIENTCOLOR))
                    {
                        _mKa = readVertexFromLine(line, AMBIENTCOLOR.Length);
                    }
                    else if (line.StartsWith(DIFFUSECOLOR))
                    {
                        _mKd = readVertexFromLine(line, DIFFUSECOLOR.Length);
                    }
                    else if (line.StartsWith(SPECULARCOLOR))
                    {
                        _mKs = readVertexFromLine(line, SPECULARCOLOR.Length);
                    }
                    else
                        continue;
                }
                _materialList.Add(new MaterialProperty(_mKa, _mKd, _mKs, _mNs, _materialName));
            }
            return _materialList;
        }

        private static float readSpecularExponent(string line)
        {
            line = line.Substring(SPECULAREXPONENT.Length).Trim().Replace('.', ',');

            float exponent;
            if (float.TryParse(line, out exponent))
                return exponent;
            else
                throw new Exception("Could not parse float");
        }

        private static string readMaterialName(string line)
        {
            return line.Substring(MATERIAL_DECLARATION.Length).Trim();
        }

        private static List<Triangle> getTriangleList(string filePath, List<MaterialProperty> materialList)
        {
            List<Triangle> _triangleList = new List<Triangle>();
            List<vec3> _vertexList = new List<vec3>();
            List<vec3> _vertexNormalList = new List<vec3>();
            List<vec2> _vertexTextureList = new List<vec2>();

            using (StreamReader s = new StreamReader(filePath))
            {
                string line;
                MaterialProperty _currentProperty = new MaterialProperty();

                while ((line = s.ReadLine()) != null)
                {
                    if (line.StartsWith(COMMENTARY))
                        continue;
                    else if (line.StartsWith(OBJECTNAME))
                    {
                        _vertexList = new List<vec3>();
                        _vertexNormalList = new List<vec3>();
                    }
                    else if (line.StartsWith(VERTEXNORMAL))
                    {
                        _vertexNormalList.Add(readVertexNormalFromLine(line, VERTEXNORMAL.Length));
                    }
                    else if (line.StartsWith(VERTEXTEXTURE))
                    {
                        //_vertexTextureList.Add(readVertexTextureFromLine(line, VERTEXTEXTURE.Length));
                    }
                    else if (line.StartsWith(VERTEX))
                    {
                        _vertexList.Add(readVertexFromLine(line, VERTEX.Length));
                    }
                    else if (line.StartsWith(MATERIAL_USAGE))
                    {
                        _currentProperty = getMaterialFromLine(line, materialList);
                    }
                    else if (line.StartsWith(TRIANGLEGROUPS))
                    {
                        _triangleList.Add(addTrianglesFromLine(line, _vertexList, _vertexNormalList, _vertexTextureList,_currentProperty));
                    }
                }
            }
            return _triangleList;
        }

        private static Triangle addTrianglesFromLine(string line, List<vec3> vertexList, List<vec3> vertexNormalList, List<vec2> vertexTextureList,MaterialProperty materialProperty)
        {
            line = line.Substring(TRIANGLEGROUPS.Length).Trim();
            string[] split = line.Split(' ');
            if (split.Length > 3)
                Console.WriteLine("Tetrahedar oder hoehere Ordnung.");
            if (split[0].Contains("//"))
                return newTriangleWithNormal(split, vertexList, vertexNormalList, materialProperty);
            if (split[0].Contains("/"))
                return newTriangleWithTextureCoordinates(split, vertexList, vertexNormalList, vertexTextureList, materialProperty);
            else
                return newTriangleWithoutNormal(split, vertexList, materialProperty);
        }

        private static Triangle newTriangleWithTextureCoordinates(string[] split, List<vec3> vertexList, List<vec3> vertexNormalList, List<vec2> vertexTextureList, MaterialProperty material)
        {
            if(split[0].Split('/').Length == NULLPLAN)
            {
                //TODO: TEXTUREN
            }
            else
            {

            }
            throw new Exception();
        }

        private static Triangle newTriangleWithoutNormal(string[] split, List<vec3> vertexList, MaterialProperty lightProperty)
        {
            int _i1, _i2, _i3;
            string _s1 = split[0];
            string _s2 = split[1];
            string _s3 = split[2];
            if (Int32.TryParse(_s1[0].ToString(), out _i1) && Int32.TryParse(_s2[0].ToString(), out _i2) && Int32.TryParse(_s3[0].ToString(), out _i3))
            {
                vec4 _p0 = new vec4(vertexList[_i1 - 1], 1);
                vec4 _p1 = new vec4(vertexList[_i2 - 1], 1);
                vec4 _p2 = new vec4(vertexList[_i3 - 1], 1);
                return new Triangle(_p0, _p1, _p2, lightProperty);
            }
            else
                throw new Exception("Could not parse Triangle");
        }

        private static Triangle newTriangleWithNormal(string[] split, List<vec3> vertexList, List<vec3> vertexNormalList, MaterialProperty lightProperty)
        {
            int _i1, _i2, _i3, _iN;
            string _s1 = split[0];
            string _s2 = split[1];
            string _s3 = split[2];
            string _sN = split[0].Split('/')[2];
            if (Int32.TryParse(_s1[0].ToString(), out _i1) && Int32.TryParse(_s2[0].ToString(), out _i2) && Int32.TryParse(_s3[0].ToString(), out _i3) && Int32.TryParse(_sN, out _iN))
            {
                vec4 _p0 = new vec4(vertexList[_i1 - 1], 1);
                vec4 _p1 = new vec4(vertexList[_i2 - 1], 1);
                vec4 _p2 = new vec4(vertexList[_i3 - 1], 1);
                vec4 _n = new vec4(vertexNormalList[_iN - 1], 0);
                return new Triangle(_p0, _p1, _p2, _n, lightProperty);
            }
            else
                throw new Exception("Could not parse Triangle");
        }

        private static MaterialProperty getMaterialFromLine(string line, List<MaterialProperty> materialList)
        {
            string name = line.Substring(MATERIAL_USAGE.Length).Trim();
            foreach (MaterialProperty l in materialList)
                if (l.ToString().Equals(name))
                    return l;

            throw new Exception("Could not find Material");
        }

        private static vec3 readVertexFromLine(string line, int declarationLength)
        {
            line = line.Substring(declarationLength).Trim();
            string[] split = line.Split(' ');
            float x, y, z;
            split[0] = split[0].Replace('.', ',');
            split[1] = split[1].Replace('.', ',');
            split[2] = split[2].Replace('.', ',');

            if (float.TryParse(split[0], out x) && float.TryParse(split[1], out y) && float.TryParse(split[2], out z))
            {
                return new vec3(x, y, z);
            }
            else
                throw new Exception("Could not parse line");
        }

        private static vec3 readVertexNormalFromLine(string line, int declarationLength)
        {
            line = line.Substring(declarationLength).Trim();
            string[] split = line.Split(' ');
            float x, y, z;
            split[0] = split[0].Replace('.', ',');
            split[1] = split[1].Replace('.', ',');
            split[2] = split[2].Replace('.', ',');
            if (float.TryParse(split[0], out x) && float.TryParse(split[1], out y) && float.TryParse(split[2], out z))
            {
                return new vec3(x, y, z);
            }
            else
                throw new Exception("Could not parse line");
        }

        private static vec2 readVertexTextureFromLine(string line, int declarationLength)
        {
            line = line.Substring(declarationLength).Trim();
            string[] split = line.Split(' ');
            float u, v;
            split[0] = split[0].Replace('.', ',');
            split[1] = split[1].Replace('.', ',');
            if (float.TryParse(split[0], out u) && float.TryParse(split[1], out v))
                return new vec2(u, v);
            else
                throw new Exception("Could not parse line");
        }
    }
}
