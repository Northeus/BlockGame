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
        protected override void OnLoad()
        {
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
            base.OnRenderFrame( args );
        }
    }
}
