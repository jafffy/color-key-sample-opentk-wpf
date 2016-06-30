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
        List<Geometry> _geometries = new List<Geometry>();

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
            _geometries.Add(new Geometry(new Vector4(1, 1, 1, 1), new Vector2(1, 1)));

            _geometries.Add(new Geometry(new Vector4(0, 0, 1, 1), new Vector2(1, 1)));
            _geometries.Add(new Geometry(new Vector4(0, 0, 0, 0), new Vector2(0.5f, 0.5f)));
        }

        private void _glControl_Resize(object sender, EventArgs e)
        {
        }

        private void _glControl_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            GL.Viewport(_glControl.ClientRectangle);

            GL.ClearColor(0, 0, 0, 0.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            foreach (var geometry in _geometries)
            {
                geometry.Draw();
            }

            _glControl.SwapBuffers();
        }
    }
}
