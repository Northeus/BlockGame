using BlockGame.Game;

namespace BlockGame.Graphics
{
    /// <summary>
    /// Interface for rendering world, gui and menu.
    /// </summary>
    public interface IRenderer
    {
        /// <summary>
        /// Clear screene with black dark grey color.
        /// </summary>
        void ClearScreen();

        /// <summary>
        /// Adjust view using <c> Camera </c>.
        /// </summary>
        void UpdateView( Camera camera );

        /// <summary>
        /// Every time window is resized, this method is called with new
        /// window parameters, so renderer can adjust own settings.
        /// </summary>
        /// <param cref="width"> New width of the window. </param>
        /// <param cref="height"> new height of the window. </param>
        void OnResize( int width, int height );

        /// <summary>
        /// If needed, renderer may use this method which will be called
        /// before rendering to cache data about world.
        /// </summary>
        /// <param cref="world"> World containing chunks to be drawn. </param>
        /// <param cref="camera"> Current players view. </param>
        void LoadWorld( World world, Camera camera );

        /// <summary>
        /// Draw world using player view for optimalization.
        /// </summary>
        /// <param cref="world"> World containing chunks to be drawn. </param>
        /// <param cref="camera"> Current players view. </param>
        void Draw( World world, Camera camera );
    }
}
