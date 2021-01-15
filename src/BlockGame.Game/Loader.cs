using BlockGame.Graphics;

namespace BlockGame.Game
{
    /// <summary>
    /// Class wrap up and load everything needed from game
    /// perspective, so everything will be set easily, just
    /// via calling constructor.
    /// </summary>
    public class Loader
    {
        private World _world;

        private Player _player;

        /// <summary>
        /// Get currently used world.
        /// </summary>
        public World World => _world;

        /// <summary>
        /// Get currently handled player.
        /// </summary>
        public Player Player => _player;

        /// <summary>
        /// Loads up game. Will require information about window,
        /// which will be used for player's pov.
        /// </summary>
        /// <param cref="width"> Width of the screen. </param>
        /// <param cref="height"> Height of the screen. </param>
        public Loader( int width, int height )
        {
            _world = new World();

            _player = new Player( new Camera( width, height ) );
        }
    }
}
