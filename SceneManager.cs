using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlmNet;

namespace SimpleRayTracer
{
    class SceneManager
    {

        
        private SceneManager()
        {
            vec3 pos = new vec3(0, 0, 0);
            vec3 ori = new vec3(0, 0, 1);
            Camera c = new Camera(pos, ori);
        }

        public static SceneManager createSceneManager()
        {
            return new SceneManager();
        }

        public void addContent(ObjectType type, vec3 position, mat3 rotation, vec3 scale)
        {

        }
    }
}
