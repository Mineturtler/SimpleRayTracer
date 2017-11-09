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
        List<ObjectType> objectList = new List<ObjectType>();
        private SceneManager()
        {
        }

        public static SceneManager createSceneManager()
        {
            return new SceneManager();
        }

        public void addContent(ObjectType type)
        {
             objectList.Add(type);
        }

        public List<ObjectType> ObjectList
        {
            get { return objectList; }
        }
    }
}
