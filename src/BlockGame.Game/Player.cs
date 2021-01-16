using BlockGame.Graphics;

using OpenTK.Mathematics;

namespace BlockGame.Game
{
    /// <summary>
    /// Class representing player, with his position and camera view.
    /// </summary>
    public class Player
    {
        private static float _speed = 5.0f;

        private Camera _camera;

        /// <summary>
        /// Give acces to camera for rendering functions.
        /// </summary>
        public Camera Camera => _camera;

        /// <summary>
        /// Constructor takes preinitialized camera object for his view.
        /// </summary>
        /// <param cref="camera"> Camera which will be used. </param>
        public Player( Camera camera )
        {
            _camera = camera;
        }

        /// <summary>
        /// Move character acording to directory using delta time.
        /// </summary>
        /// <param cref="direction"> Direction, in which is player moving. </param>
        /// <param cref="time"> Delta time ( time from last Move ) in ms. </param>
        public void Move( Direction direction, float time ) =>
#pragma warning disable CS8524
            _camera.Position += direction switch
            {
                Direction.Front =>   _camera.FrontHorizontal * _speed * time,
                Direction.Back  => - _camera.FrontHorizontal * _speed * time,
                Direction.Left  => - _camera.RightHorizontal * _speed * time,
                Direction.Right =>   _camera.RightHorizontal * _speed * time,
                Direction.Jump  =>   Vector3.UnitY           * _speed * time,
                Direction.Down  => - Vector3.UnitY           * _speed * time,
            };
#pragma warning restore CS8524

        public enum Direction
        {
            Front,
            Back,
            Left,
            Right,
            Jump,
            Down,
        }
    }
}
