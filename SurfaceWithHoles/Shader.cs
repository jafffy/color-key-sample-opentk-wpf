using OpenTK.Graphics.OpenGL;
using System;
using System.IO;

namespace SurfaceWithHoles
{
    public class Shader
    {
        static public int loadFromSource(string vert_src, string geom_src, string frag_src)
        {
            int program;
            int vertex_shader, geometry_shader = -1, fragment_shader;
            string vertex_shader_errlog, geometry_shader_errlog, fragment_shader_errlog;

            string vertex_shader_source = vert_src,
                geometry_shader_source = geom_src,
                fragment_shader_source = frag_src;

            vertex_shader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertex_shader, vertex_shader_source);
            GL.CompileShader(vertex_shader);
            vertex_shader_errlog = GL.GetShaderInfoLog(vertex_shader);

            if (vertex_shader_errlog.Length > 0)
            {
                return -1;
            }

            if (geom_src != "")
            {
                geometry_shader = GL.CreateShader(ShaderType.GeometryShader);
                GL.ShaderSource(geometry_shader, geometry_shader_source);
                GL.CompileShader(geometry_shader);
                geometry_shader_errlog = GL.GetShaderInfoLog(geometry_shader);

                if (geometry_shader_errlog.Length > 0)
                {
                    return -1;
                }
            }

            fragment_shader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragment_shader, fragment_shader_source);
            GL.CompileShader(fragment_shader);
            fragment_shader_errlog = GL.GetShaderInfoLog(fragment_shader);

            if (fragment_shader_errlog.Length > 0)
            {
                return -1;
            }

            program = GL.CreateProgram();
            GL.AttachShader(program, vertex_shader);
            if (geom_src != "")
            {
                GL.AttachShader(program, geometry_shader);
            }
            GL.AttachShader(program, fragment_shader);
            GL.LinkProgram(program);

            GL.DeleteShader(vertex_shader);
            if (geom_src != "")
            {
                GL.DeleteShader(geometry_shader);
            }
            GL.DeleteShader(fragment_shader);

            return program;
        }

        static public int loadFromSource(string vert_src, string frag_src)
        {
            return loadFromSource(vert_src, "", frag_src);
        }
        static public int loadFromFile(string vert_path, string frag_path)
        {
            vert_path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, vert_path);
            frag_path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, frag_path);

            string vertex_shader_source = File.ReadAllText(vert_path),
                fragment_shader_source = File.ReadAllText(frag_path);

            return loadFromSource(vert_path, frag_path);
        }
    }
}
