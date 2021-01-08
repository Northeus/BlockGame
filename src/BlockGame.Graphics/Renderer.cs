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

        public void CleanUp()
        {
            Model.CleanUp();
        }

        public void OnResize( int width, int height )
        {
            GL.Viewport( 0, 0, width, height );
        }

        // TODO remove
        public void Draw( Model model )
        {
            model.Draw();
        }
    }
}
