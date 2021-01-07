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
        /// Everythin needed to be cleaned up after using should
        /// be placed there.
        /// </summary>
        void CleanUp();

        /// <summary>
        /// Every time window is resized, this method is called with new
        /// window parameters, so renderer can adjust own settings.
        /// </summary>
        /// <param cref="width"> New width of the window. </param>
        /// <param cref="height"> new height of the window. </param>
        void OnResize( int width, int height );

        //TODO remove
        void Draw( Model model );
    }
}
