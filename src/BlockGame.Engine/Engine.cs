using BlockGame.Graphics;
using BlockGame.Input;

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

            VSync = VSyncMode.Off;

            CursorGrabbed = true;
        }

        /// <summary>
        /// Is executed when <c> Run() </c> is called.
        /// Should be used for resource initialization.
        /// </summary>

        // TODO remove testing code
        private ChunkModel model;
        private Camera _camera;

        protected override void OnLoad()
        {
            // TODO remove test code
            model = new ChunkModel();
            model.Update();
            _camera = new Camera( ( float ) Size.X / Size.Y );
            ControlCamera.BindCamera( _camera );

            base.OnLoad();
        }

        /// <summary>
        /// Run when window is about to close.
        /// Should be used for finalizing enviroment.
        /// </summary>
        protected override void OnUnload()
        {
            base.OnUnload();
        }

        /// <summary>
        /// Class should contain content of logic handling loop.
        /// </summary>
        /// <param creaf="args"> Event arguments for frame. </param>
        protected override void OnUpdateFrame( FrameEventArgs args )
        {
            if ( ! IsFocused )
            {
                return;
            }

            // TODO
            ControlCamera.Update( KeyboardState, MouseState, ( float ) args.Time );

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

            _renderer.UpdateView( _camera );

            System.Console.WriteLine( _camera.RotationX );
            System.Console.WriteLine( _camera.RotationY );
            // TODO remove test code

            model.Draw();
            System.Console.WriteLine( "\n" + (1.0f / args.Time).ToString() );

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

            // TODO
            _camera.AspectRatio = ( float ) Size.X / Size.Y ;

            base.OnResize( args );
        }
    }
}
