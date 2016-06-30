using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SurfaceWithHoles
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GLControl _glControl;
        private int _vertexArrayObject;
        private int _vertexBufferObject;
        private float[] _vertices;
        private int _program;
        private int _vertexID;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void _host_Initialized(object sender, EventArgs e)
        {
            _glControl = new GLControl(new OpenTK.Graphics.GraphicsMode(32, 24, 0, 4));

            _glControl.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            _glControl.TopLevel = false;
            _glControl.Load += _glControl_Load;
            _glControl.Resize += _glControl_Resize;
            _glControl.Paint += _glControl_Paint;

            (sender as WindowsFormsHost).Child = _glControl;

            _glControl.MakeCurrent();
        }

        private void _glControl_Load(object sender, EventArgs e)
        {
            #region Shader codes
            string vertexShaderCode = @"
#version 330 core

in vec2 _vertex;

void main(void)
{
    gl_Position = vec4(_vertex, 0, 1);
}
";
            string fragmentShaderCode = @"
#version 330 core

out vec4 out_color;

void main(void)
{
    out_color = vec4(1, 1, 1, 1);
}
";
            #endregion
            _program = Shader.loadFromSource(vertexShaderCode, fragmentShaderCode);
            Debug.Assert(_program != -1);

            _vertexID = GL.GetAttribLocation(_program, "_vertex");
            Debug.Assert(_vertexID != -1);

            _vertices = new float[]
            {
                -1.0f, -1,
                 1.0f, -1,
                 0.0f,  1
            };

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float),
                _vertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(_vertexID, 2, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindVertexArray(0);
        }

        private void _glControl_Resize(object sender, EventArgs e)
        {
        }

        private void _glControl_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            GL.Viewport(_glControl.ClientRectangle);

            GL.ClearColor(0, 0, 0.3f, 0.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.UseProgram(_program);

            GL.BindVertexArray(_vertexArrayObject);

            GL.EnableVertexAttribArray(_vertexID);

            GL.DrawArrays(PrimitiveType.Triangles, 0, _vertices.Length / 2);

            GL.DisableVertexAttribArray(_vertexID);

            GL.BindVertexArray(0);

            GL.UseProgram(0);

            _glControl.SwapBuffers();
        }
    }
}
