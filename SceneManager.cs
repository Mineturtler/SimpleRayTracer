using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using GlmNet;
using SimpleRayTracer.Objekte;

namespace SimpleRayTracer
{
    class SceneManager
    {
        Dictionary<int, ObjectType> objectList = new Dictionary<int, ObjectType>();
        List<Light> lightList = new List<Light>();
        List<AABB> aABB_groups = new List<AABB>();

        private SceneManager() { }

        public static SceneManager createSceneManager()
        {
            return new SceneManager();
        }

        public void addContent(ObjectType type)
        {
            objectList.Add(type.IdNumber,type);
        }

        public void addContent(params ObjectType[] types)
        {
            foreach (var _o in types)
                objectList.Add(_o.IdNumber, _o);
        }

        public void formGroup(AABB aabbGroup)
        {
            aABB_groups.Add(aabbGroup);
            foreach (var _o in aabbGroup.ElementList)
                _o.PartOfGroup = true;
        }

        internal void formGroup(params ObjectType[] _objectList)
        {
            var aabbGroup = new AABB(_objectList);
            aABB_groups.Add(aabbGroup);
            foreach (var _o in aabbGroup.ElementList)
                _o.PartOfGroup = true;
        }

        public void addLightSource(Light light)
        {
            lightList.Add(light);
        }

        public void addLightSource(vec4 position)
        {
            lightList.Add(new Light(position));
        }

        public Dictionary<int, ObjectType> ObjectList { get => objectList; }

        public List<Light> LightList { get => lightList; }

        public List<AABB> AABBList { get => aABB_groups; }
    }
}
