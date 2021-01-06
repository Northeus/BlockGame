using OpenTK.Windowing.Desktop;

namespace BlockGame.Glue
{
    /// <summary>
    /// Class used as glue to connect both output and input
    /// functionality of GameWindow class.
    /// </summary>
    public class Engine : GameWindow
    {
        /// <summary>
        /// Create Window with default settings.
        /// <see cref="WindowSettings.cs"/> for related settings.
        /// </summary>
        public Engine():
            base( WindowSettings.GameWindow, WindowSettings.NativeWindow )
        {

        }
    }
}
