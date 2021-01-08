using BlockGame.Graphics;

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
        private IRenderer _renderer;

        /// <summary>
        /// Create window with default settings.
        /// <see cref="WindowSettings.cs"/> for related settings.
        /// </summary>
        public Engine():
            base( WindowSettings.GameWindow, WindowSettings.NativeWindow )
        {
            _renderer = Renderer.Instance;
        }

        /// <summary>
        /// Is executed when <c> Run() </c> is called.
        /// Should be used for resource initialization.
        /// </summary>

        // TODO remove testing code
        private Model model;

        protected override void OnLoad()
        {
            model = new Model( new Vertex[0] {}, new uint[0] {},
                               new Texture( "../Resources/Box.png" ) );
            model.Vertices = new Vertex[4] {
                new Vertex( -0.5f,  0.5f, 0.5f, 0.0f, 1.0f ),
                new Vertex( -0.5f, -0.5f, 0.5f, 0.0f, 0.0f ),
                new Vertex(  0.5f, -0.5f, 0.5f, 1.0f, 0.0f ),
                new Vertex(  0.5f,  0.5f, 0.5f, 1.0f, 1.0f ),
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
            _renderer.CleanUp();

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
            _renderer.ClearScreen();

            _renderer.Draw( model );

            SwapBuffers();

            base.OnRenderFrame( args );
        }

        /// <summary>
        /// Rise on every resize of window. Might be used to adjust
        /// renderer view on window.
        /// </summary>
        protected override void OnResize( ResizeEventArgs args )
        {
            _renderer.OnResize( Size.X, Size.Y );

            base.OnResize( args );
        }
    }
}
