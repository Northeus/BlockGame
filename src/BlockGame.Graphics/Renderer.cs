using BlockGame.Game;

using OpenTK.Graphics.OpenGL4;

namespace BlockGame.Graphics
{
    /// <summary>
    /// Singleton class for rendering, check example for tips how to use it.
    /// </summary>
    public partial class Renderer : IRenderer
    {
        private static Renderer _instance;

        private Camera _camera;

        private World _world;

        /// <summary>
        /// Property which will give you instance of Renderer
        /// </summary>
        /// <example>
        /// How to access and cleat class after using.
        /// <code>
        /// renderer = Renderer.Instance;
        /// /* use of renderer class */
        /// renderer.CleanUp();
        /// /* do not use renderer anymore, you must get new one */
        /// </code>
        /// </example>
        public static Renderer Instance
        {
            get => _instance ??= new Renderer();
        }

        private Renderer()
        {
            GL.ClearColor( 0.1f, 0.1f, 0.1f, 1.0f );

            GL.Enable( EnableCap.DepthTest );
        }

        private void ClearScreen()
        {
            GL.Clear( ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit );
        }

        private void UpdateView()
        {
            Model.AdjustMatrices( _camera.ViewMatrix, _camera.ProjectionMatrix );
        }
    }

    /// <summary>
    /// Implementation of <see cref="IRenderer"/> interface.
    /// </summary>
     public partial class Renderer : IRenderer
    {
        public void OnResize( int width, int height )
        {
            GL.Viewport( 0, 0, width, height );
        }

        public void LoadWorld( World world, Camera camera )
        {
            _camera = camera;

            _world = world;

            ChunkRenderer.LoadChunks( world, camera );
        }

        public void Draw()
        {
            ClearScreen();

            UpdateView();

            ChunkRenderer.Draw();
        }
    }
}
