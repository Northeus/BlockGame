namespace BlockGame.Renderer
{
    /// <summary>
    /// Singleton class for rendering, check example for tips how to use it.
    /// </summary>
    public partial class Renderer : IRenderer
    {
        private static Renderer _instance = null;

        /// <summary>
        /// Property which will give you instance of Renderer
        /// </summary>
        /// <example>
        /// How to access and cleat class after using.
        /// <code>
        /// renderer = Renderer.Instance;
        /// /* use of renderer class */
        /// renderer.CleanUp();
        /// /* do not use renderer anymore, you must get new one */
        /// </code>
        /// </example>
        public static Renderer Instance
        {
            get => _instance ??= new Renderer();
        }

        private Renderer()
        {

        }

        public void CleanUp()
        {

        }
    }

    /// <summary>
    /// Implementation of IRenderer interface.
    /// </summary>
    public partial class Renderer : IRenderer
    {
        // TODO
    }
}
