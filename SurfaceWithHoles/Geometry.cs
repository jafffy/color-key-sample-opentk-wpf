using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceWithHoles
{
    public class Geometry
    {
        #region Fields
        private int _vertexArrayObject;
        private int _vertexBufferObject;
        private float[] _vertices;
        private int _program;
        private int _vertexID;
        private int _colorID;
        private int _SID;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public Geometry(Vector4 color, Vector2 scale)
        {
            #region Shader codes
            string vertexShaderCode = @"
#version 330 core

in vec2 _vertex;

uniform mat4 _S;

void main(void)
{
    gl_Position = _S * vec4(_vertex, 0, 1);
}
";
            string fragmentShaderCode = @"
#version 330 core

out vec4 out_color;

uniform vec4 _color;

void main(void)
{
    out_color = _color;
}
";
            #endregion
            _program = Shader.loadFromSource(vertexShaderCode, fragmentShaderCode);
            Debug.Assert(_program != -1);

            _vertexID = GL.GetAttribLocation(_program, "_vertex");
            Debug.Assert(_vertexID != -1);

            _colorID = GL.GetUniformLocation(_program, "_color");
            Debug.Assert(_colorID != -1);

            _SID = GL.GetUniformLocation(_program, "_S");
            Debug.Assert(_SID != -1);

            _vertices = new float[]
            {
                -1.0f, -1.0f,
                 1.0f, -1.0f,
                 0.0f,  1.0f
            };

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float),
                _vertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(_vertexID, 2, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindVertexArray(0);

            GL.UseProgram(_program);
            GL.Uniform4(_colorID, color);

            Matrix4 S = Matrix4.CreateScale(scale.X, scale.Y, 1.0f);
            GL.UniformMatrix4(_SID, false, ref S);

            GL.UseProgram(0);
        }
        #endregion

        #region Functions
        public void Draw()
        {
            GL.UseProgram(_program);

            GL.BindVertexArray(_vertexArrayObject);

            GL.EnableVertexAttribArray(_vertexID);

            GL.DrawArrays(PrimitiveType.Triangles, 0, _vertices.Length / 2);

            GL.DisableVertexAttribArray(_vertexID);

            GL.BindVertexArray(0);

            GL.UseProgram(0);
        }
        #endregion
    }
}
