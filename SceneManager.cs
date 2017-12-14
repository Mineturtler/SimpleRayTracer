using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using GlmNet;

namespace SimpleRayTracer
{
    class SceneManager
    {
        Dictionary<int, ObjectType> objectList = new Dictionary<int, ObjectType>();
        List<Light> lightList = new List<Light>();
        List<vec3> aABB_groups = new List<vec3>();

        private SceneManager() { }

        public static SceneManager createSceneManager()
        {
            return new SceneManager();
        }

        public void addContent(ObjectType type)
        {
            objectList.Add(type.IdNumber,type);
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
    }
}
