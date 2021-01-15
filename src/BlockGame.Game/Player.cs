using BlockGame.Graphics;

namespace BlockGame.Game
{
    /// <summary>
    /// Class representing player, with his position and camera view.
    /// </summary>
    public class Player
    {
        private Camera _camera;

        public Camera Camera => _camera;

        /// <summary>
        /// Constructor takes preinitialized camera object for his view.
        /// </summary>
        /// <param cref="camera"> Camera which will be used. </param>
        public Player( Camera camera )
        {
            _camera = camera;
        }
    }
}
