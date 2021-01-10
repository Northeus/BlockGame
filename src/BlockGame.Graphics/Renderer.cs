using BlockGame.Game;

using OpenTK.Graphics.OpenGL4;

namespace BlockGame.Graphics
{
    /// <summary>
    /// Singleton class for rendering, check example for tips how to use it.
    /// </summary>
    public partial class Renderer : IRenderer
    {
        private static Renderer _instance = null;

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
    }

    /// <summary>
    /// Implementation of <see cref="IRenderer"/> interface.
    /// </summary>
     public partial class Renderer : IRenderer
    {
        public void ClearScreen()
        {
            GL.Clear( ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit );
        }

        public void UpdateView( Camera camera )
        {
            // TODO bubble functionality up
            Model._shader.LoadMatrix4( "view", camera.ViewMatrix );
            Model._shader.LoadMatrix4( "projection", camera.ProjectionMatrix );
        }

        public void OnResize( int width, int height )
        {
            GL.Viewport( 0, 0, width, height );
        }

        public void LoadWorld( World world, Camera camera )
        {
            ChunkRenderer.AddChunk( world.WorldMap[ 0, 0, 0 ] );
        }

        public void Draw( World world, Camera camera )
        {
            // TODO Check for changes

            ChunkRenderer.Draw();
        }
    }
}
