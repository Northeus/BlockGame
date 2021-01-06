using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace BlockGame.Glue
{
    /// <summary>
    /// Predefined settings for window used by <c> Engine </c> class.
    /// </summary>
    public static class WindowSettings {
        /// <summary>
        /// Disabled multi-threading and unlimited frequency as default.
        /// </summary>
        public static readonly GameWindowSettings GameWindow =
            GameWindowSettings.Default;

        /// <summary>
        /// Settings for base window like size, title, fullscreen etc.
        /// </summary>
        public static readonly NativeWindowSettings NativeWindow =
            new NativeWindowSettings()
            {
                Title = "BlockGame",
                Size = new Vector2i( 800, 600 ),
            };
    }
}
