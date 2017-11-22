using System.Drawing;

namespace SimpleRayTracer
{
    class Constants
    {
        private const string ambient_color = "Ka";
        private const string diffuse_color = "Kd";
        private const string specular_color = "Ks";
        private const string specular_exponent = "Ns";
        private const string material_declaration = "newmtl";

        private const string vertex = "v";
        private const string vertex_normal = "vn";
        private const string vertex_texture = "vt";
        private const string object_name = "o";
        private const string group = "g";
        private const string material_usage = "usemtl";
        private const string triangle_groups = "f";
        private const string commentary = "#";

        private const float field_of_view = 90f;
        private const float camera_offset = 1f;
        private const float max_camera_distance = 60;
        private static Color background_color = Color.Aquamarine;

        private const int filename_length = 5;
        private const int resolution_width = 640;
        private const int resolution_height = 480;

        private const string filepath_objectdata = "C:\\Users\\Axel\\Documents\\GitHub\\SimpleRayTracer\\ObjDateien\\";

        private Constants() { }

        public static string Ambient_color => ambient_color;
        public static string Diffuse_color => diffuse_color;
        public static string Specular_color => specular_color;
        public static string Specular_exponent => specular_exponent;
        public static string Vertex => vertex;
        public static string Vertex_normal => vertex_normal;
        public static string Vertex_texture => vertex_texture;
        public static string Object_name => object_name;
        public static string Group => group;
        public static string Material_usage => material_usage;
        public static string Triangle_groups => triangle_groups;
        public static string Commentary => commentary;
        public static float Field_of_view => field_of_view;
        public static float Camera_offset => camera_offset;
        public static float Max_camera_distance => max_camera_distance;
        public static Color Background_color => background_color;
        public static int Filename_length => filename_length;
        public static int Resolution_width => resolution_width;
        public static int Resolution_height => resolution_height;
        public static string Filepath_objectdata => filepath_objectdata;
    }
}
