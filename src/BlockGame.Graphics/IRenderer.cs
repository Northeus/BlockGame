using BlockGame.Game;

namespace BlockGame.Graphics
{
    /// <summary>
    /// Interface for rendering world, gui and menu.
    /// </summary>
    public interface IRenderer
    {
        /// <summary>
        /// Every time window is resized, this method is called with new
        /// window parameters, so renderer can adjust own settings.
        /// </summary>
        /// <param cref="width"> New width of the window. </param>
        /// <param cref="height"> new height of the window. </param>
        void OnResize( int width, int height );

        /// <summary>
        /// Use this method to load data for rendering.
        /// </summary>
        /// <param cref="world"> World containing chunks to be drawn. </param>
        /// <param cref="camera"> Current players view. </param>
        void LoadWorld( World world, Camera camera );

        /// <summary>
        /// Draw world, information for drawing must be loaded beforehand using
        /// method <see cref="LoadWorld">.
        /// </summary>
        void Draw();
    }
}
