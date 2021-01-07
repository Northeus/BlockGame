using BlockGame.Renderer;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace BlockGame.Glue
{
    /// <summary>
    /// Class used as glue to connect both output and input
    /// functionality of <c> GameWindow </c> class.
    /// </summary>
    /// <example>
    /// Example how to run code from <c> Engine </c> class.
    /// <code>
    /// using ( Engine engine = new Engine() )
    /// {
    ///     engine.Run();
    /// }
    /// </code>
    /// </example>
    public class Engine : GameWindow
    {
        /// <summary>
        /// Create window with default settings.
        /// <see cref="WindowSettings.cs"/> for related settings.
        /// </summary>
        public Engine():
            base( WindowSettings.GameWindow, WindowSettings.NativeWindow )
        {

        }

        /// <summary>
        /// Is executed when <c> Run() </c> is called.
        /// Should be used for resource initialization.
        /// </summary>

        // TODO remove testing code
        private Model model;

        protected override void OnLoad()
        {
            // TODO remove testing code
            GL.ClearColor( 0.1f, 0.1f, 0.1f, 1.0f );
            model = new Model( new Vertex[0] {}, new uint[0] {} );
            model.Vertices = new Vertex[4] {
                new Vertex( -0.5f,  0.5f, 0.5f ),
                new Vertex( -0.5f, -0.5f, 0.5f ),
                new Vertex(  0.5f, -0.5f, 0.5f ),
                new Vertex(  0.5f,  0.5f, 0.5f ),
            };
            model.Indices = new uint[6] {
                1, 0, 3,
                1, 2, 3
            };

            base.OnLoad();
        }

        /// <summary>
        /// Run when window is about to close.
        /// Should be used for finalizing enviroment.
        /// </summary>
        protected override void OnUnload()
        {
            // TODO wrap this.
            Model.CleanUp();

            base.OnUnload();
        }

        /// <summary>
        /// Class should contain content of logic handling loop.
        /// </summary>
        /// <param creaf="args"> Event arguments for frame. </param>
        protected override void OnUpdateFrame( FrameEventArgs args )
        {
            // TODO remove after, used only to make easier program handling
            if ( KeyboardState.IsKeyDown( Keys.Escape ) )
            {
                Close();
            }

            base.OnUpdateFrame( args );
        }

        /// <summary>
        /// Class should contain content of rendering loop.
        /// </summary>
        /// <param cref="args"> Event arguments for frame. </param>
        protected override void OnRenderFrame( FrameEventArgs args )
        {
            // TODO remove testing code
            GL.Clear( ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit );
            model.Draw();

            SwapBuffers();

            base.OnRenderFrame( args );
        }

        /// <summary>
        /// Rise on every resize of window. Might be used to adjust
        /// renderer view on window.
        /// </summary>
        protected override void OnResize( ResizeEventArgs args )
        {
            // TODO make irenderer wrapper
            GL.Viewport( 0, 0, Size.X, Size.Y );

            base.OnResize( args );

        }
    }
}
