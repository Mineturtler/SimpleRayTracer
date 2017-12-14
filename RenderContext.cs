using System.Collections.Generic;
using System.Drawing;
using GlmNet;
using System;
using System.Threading;

namespace SimpleRayTracer
{
    class RenderContext
    {

        private RenderContext() { }

        public static Color[,] renderScene(SceneManager _sManager, Camera _c, int _resoWidth, int _resoHeight)
        {
            Color[,] _imageArray = new Color[_resoWidth, _resoHeight];
            ViewPlane _vPlane = new ViewPlane(_c, _resoWidth, _resoHeight, Constants.Field_of_view, Constants.Camera_offset);

            for (int i = 0; i < _resoWidth; i++)
                for (int j = 0; j < _resoHeight; j++)
                {
                    vec4 _pos = _vPlane.TopLeft + (i + 0.5f) * _vPlane.U_Increment + (j + 0.5f) * _vPlane.V_Increment;
                    Ray _ray = new Ray(_pos, _pos - _vPlane.Camera_Pos);
                    vec4 _intersecPoint;
                    vec4 _normal;
                    MaterialProperty _materialProperty;
                    KeyValuePair<int, ObjectType> _kvp;

                    if (hasObjectIntersection(_sManager.ObjectList, _ray, out _intersecPoint, out _normal, out _materialProperty, out _kvp))
                    {
                        _imageArray[i, j] = _kvp.Value.getIlluminationAt(_sManager, _intersecPoint, _ray.Direction, _normal, _materialProperty);
                    }
                    else
                        _imageArray[i, j] = Constants.Background_color;
                }
            return _imageArray;
        }
        
        private static bool hasObjectIntersection(Dictionary<int, ObjectType> objectList, Ray ray, out vec4 intersecPoint, out vec4 normal, out MaterialProperty materialProperty, out KeyValuePair<int, ObjectType> kvp)
        {
            float closestT = Constants.Max_camera_distance;
            intersecPoint = new vec4();
            normal = new vec4();
            materialProperty = new MaterialProperty();
            kvp = new KeyValuePair<int, ObjectType>();
            bool hasFound = false;

            foreach (var _kvp in objectList)
            {
                vec4 _current_intersec = new vec4();
                vec4 _current_normal = new vec4();
                MaterialProperty _props = new MaterialProperty();
                ObjectType obj = _kvp.Value;
                float t;

                if (obj.hasIntersectionPoint(ray, out _current_intersec, out _current_normal, out t, out _props))
                {
                    if (t > 0 && t < closestT)
                    {
                        intersecPoint = _current_intersec;
                        normal = _current_normal;
                        closestT = t;
                        materialProperty = _props;
                        kvp = _kvp;
                        hasFound = true;
                    }
                }
            }
            if (closestT <= Constants.Max_camera_distance && hasFound) return true;
            return false;
        }

    }
}
