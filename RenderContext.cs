using System.Collections.Generic;
using System.Drawing;
using GlmNet;
using System;

namespace SimpleRayTracer
{
    class RenderContext
    {
        private const float m_foV = 90f;
        private const float m_offset = 1f;
        private const float _maxDistance = 60;
        private static Color _backgroundColour = Color.Beige;

        private RenderContext() { }

        public static Color[,] renderScene(SceneManager _sManager, Camera _c, int _resoWidth, int _resoHeight)
        {
            Color[,] _imageArray = new Color[_resoWidth, _resoHeight];
            ViewPlane _vPlane = new ViewPlane(_c, _resoWidth, _resoHeight, m_foV, m_offset);

            for (int i = 0; i < _resoWidth; i++)
                for (int j = 0; j < _resoHeight; j++)
                {
                    vec4 _pos = _vPlane.TopLeft + (i + 0.5f) * _vPlane.U_Increment + (j + 0.5f) * _vPlane.V_Increment;
                    Ray _ray = new Ray(_pos, _pos - _vPlane.Camera_Pos);
                    vec4 _intersecPoint;
                    KeyValuePair<int, ObjectType> _kvp;

                    if (hasObjectIntersection(_sManager.ObjectList, _ray, out _intersecPoint, out _kvp))
                    {
                        _imageArray[i, j] = _kvp.Value.getPhongAt(_sManager.LightList, _ray.Direction, _intersecPoint);  
                    }
                    else
                        _imageArray[i, j] = _backgroundColour;
                }
            return _imageArray;
        }

        private static bool hasObjectIntersection(Dictionary<int, ObjectType> objectList, Ray ray, out vec4 intersecPoint, out KeyValuePair<int, ObjectType> kvp)
        {
            float closestT = _maxDistance;
            intersecPoint = new vec4();
            kvp = new KeyValuePair<int, ObjectType>();
            foreach (var _kvp in objectList)
            {
                vec4 _current_intersec = new vec4();
                ObjectType obj = _kvp.Value;
                float t;
                if (obj.hasIntersectionPoint(ray, out _current_intersec, out t))
                {
                    if (t > 0 && t < closestT)
                    {
                        intersecPoint = _current_intersec;
                        closestT = t;
                        kvp = _kvp;
                    }
                }
            }
            if (closestT < _maxDistance && !intersecPoint.Equals(new vec4())) return true;
            return false;
        }
    }
}
