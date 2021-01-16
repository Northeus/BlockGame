using BlockGame.Game;
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
        private Loader _loader;

        private IRenderer _renderer;

        /// <summary>
        /// Create window with default settings.
        /// <see cref="WindowSettings.cs"/> for related settings.
        /// </summary>
        public Engine():
            base( WindowSettings.GameWindow, WindowSettings.NativeWindow )
        {
            _renderer = Renderer.Instance;

            VSync = VSyncMode.On;

            CursorGrabbed = true;
        }

        /// <summary>
        /// Is executed when <c> Run() </c> is called.
        /// Should be used for resource initialization.
        /// </summary>
        protected override void OnLoad()
        {
            _loader = new Loader( Size.X, Size.Y );

            ControlPlayer.BindPlayer( _loader.Player );

            ControlPlayer.ScreenSize( Size.X, Size.Y );

            _renderer.LoadWorld( _loader.World, _loader.Player.Camera );


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

            ControlPlayer.Update( KeyboardState, MouseState, ( float ) args.Time );

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
            _renderer.Draw();

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

            ControlPlayer.ScreenSize( Size.X, Size.Y );

            _loader.Player.Camera.AdjustAspectRatio( Size.X, Size.Y );

            base.OnResize( args );
        }
    }
}
