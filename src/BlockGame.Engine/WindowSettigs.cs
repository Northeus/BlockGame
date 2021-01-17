using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace BlockGame.Glue
{
    /// <summary>
    /// Predefined settings for window used by <c> Engine </c> class.
    /// </summary>
    public static class WindowSettings {
        /// <summary>
        /// Setting for gamewindow (native window wrapper ) which gives us
        /// multi threading, vsync etc.
        /// </summary>
        public static GameWindowSettings GameWindow
        {
            get
            {
                var settings = GameWindowSettings.Default;

                settings.IsMultiThreaded = true;

                return settings;
            }
        }

        /// <summary>
        /// Settings for base window like size, title, fullscreen etc.
        /// </summary>
        public static readonly NativeWindowSettings NativeWindow =
            new NativeWindowSettings()
            {
                Title = "BlockGame",
                Size = new Vector2i( 1280, 720 ),
            };
    }
}
