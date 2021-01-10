using BlockGame.Graphics;

using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace BlockGame.Input
{
    /// <summary>
    /// Class helping to handle <c> Camera </c> movement.
    /// Do not foget to use <see cref="BindCamera"/> before
    /// using <see cref="Update"/>.
    /// </summary>
    public static class ControlCamera
    {
        /// <summary>
        /// Mouse sensitivity.
        /// </summary>
        public static float Sensitivity = 0.1f;

        /// <summary>
        /// Player speed.
        /// </summary>
        public static float Speed = 2.0f;

        private static Camera _camera;

        private static Vector2 _mousePos = new Vector2( 0.0f, 0.0f );

        private static bool _isFirstMouseMove = true;

        /// <summary>
        /// Bind camera, which should be controled.
        /// </summary>
        /// <param cref="camera"> Camera to be binded. </param>
        public static void BindCamera( Camera camera )
        {
            _mousePos = new Vector2( 0.0f, 0.0f );

            // TODO?
            //_firstMouseMove = true;

            _camera = camera;
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
                _camera.Position += _camera.Front * Speed * time;
            }

            if ( keyboard.IsKeyDown( Keys.S ) )
            {
                _camera.Position -= _camera.Front * Speed * time;
            }

            if ( keyboard.IsKeyDown( Keys.A ) )
            {
                _camera.Position -= _camera.Right * Speed * time;
            }

            if ( keyboard.IsKeyDown( Keys.D ) )
            {
                _camera.Position += _camera.Right * Speed * time;
            }

            if ( keyboard.IsKeyDown( Keys.Space ) )
            {
                _camera.Position += _camera.Up * Speed * time;
            }

            if ( keyboard.IsKeyDown( Keys.LeftShift ) )
            {
                _camera.Position -= _camera.Up * Speed * time;
            }

            if ( ! _isFirstMouseMove )
            {
                _camera.RotationX += ( mouse.X - _mousePos.X ) * Sensitivity;
                _camera.RotationY -= ( mouse.Y - _mousePos.Y ) * Sensitivity;
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
