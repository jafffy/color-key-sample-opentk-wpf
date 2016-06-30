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
    public class Layer
    {
        #region Fields
        private int _program;

        private float[] _vertices;
        private int _vertexArrayObject;
        private int _vertexBufferObject;
        private int _vertexID;
        private int _UVID;

        private int _framebufferTexture;
        private int _textureBufferObject;

        private int _frameBufferObject;
        private int _width;
        private int _height;
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public Layer()
        {

        }
        #endregion

        #region Functions
        public void Draw()
        {

        }
        #endregion
    }
}
