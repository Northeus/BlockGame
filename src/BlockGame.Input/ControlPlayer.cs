using BlockGame.Game;
using BlockGame.Graphics;

using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace BlockGame.Input
{
    /// <summary>
    /// Class helping to handle <c> Plyer </c> movement.
    /// Do not foget to use <see cref="BindCamera"/> before
    /// using <see cref="Update"/>.
    /// </summary>
    public static class ControlPlayer
    {
        /// <summary>
        /// Mouse sensitivity.
        /// </summary>
        public static float Sensitivity = 0.1f;

        /// <summary>
        /// Player speed.
        /// </summary>
        public static float Speed = 5.0f;

        private static Player _player;

        private static Vector2 _mousePos = new Vector2( 0.0f, 0.0f );

        private static bool _isFirstMouseMove = true;

        /// <summary>
        /// Bind camera, which should be controled.
        /// </summary>
        /// <param cref="player"> Player object containing camera. </param>
        public static void BindPlayer( Player player )
        {
            _mousePos = new Vector2( 0.0f, 0.0f );

            _isFirstMouseMove = true;

            _player = player;
        }

        /// <summary>
        /// Adjust camera via given input and delta time.
        /// </summary>
        /// <param cref="keyboard"> Current status of keyboard. </param>
        /// <param cref="mouse"> Current status of mouse. </param>
        /// <param cref="time"> Delta time ( time from last update ). </param>
        public static void Update( KeyboardState keyboard, MouseState mouse, float time )
        {
            if ( keyboard.IsKeyDown( Keys.W ) )
            {
                _player.Move( Player.Direction.Front, time );
            }

            if ( keyboard.IsKeyDown( Keys.S ) )
            {
                _player.Move( Player.Direction.Back, time );
            }

            if ( keyboard.IsKeyDown( Keys.A ) )
            {
                _player.Move( Player.Direction.Left, time );
            }

            if ( keyboard.IsKeyDown( Keys.D ) )
            {
                _player.Move( Player.Direction.Right, time );
            }

            if ( keyboard.IsKeyDown( Keys.Space ) )
            {
                _player.Move( Player.Direction.Jump, time );
            }

            if ( keyboard.IsKeyDown( Keys.LeftShift ) )
            {
                _player.Move( Player.Direction.Down, time );
            }

            // TODO change equal on every screen size
            if ( ! _isFirstMouseMove )
            {
                _player.Camera.RotationX += ( mouse.X - _mousePos.X ) * Sensitivity;
                _player.Camera.RotationY -= ( mouse.Y - _mousePos.Y ) * Sensitivity;
            }
            else
            {
                _isFirstMouseMove = false;
            }

            _mousePos.X = mouse.X;
            _mousePos.Y = mouse.Y;
        }
    }
}
