using OpenTK.Mathematics;

using System;

namespace BlockGame.Graphics
{
    /// <summary>
    /// Class representing view on OpenGL world.
    /// </summary>
    public class Camera
    {
        /// <summary>
        /// Storing position of camera in worlds.
        /// </summary>
        public Vector3 Position = Vector3.UnitZ * 3;

        private Vector3 _front = -Vector3.UnitZ;

        private Vector3 _right = Vector3.UnitX;

        private Vector3 _up = Vector3.UnitY;

        private float _rotationX = -MathHelper.PiOver2;

        private float _rotationY;

        private float _fov = MathHelper.PiOver2;

        public float _aspectRatio;

        /// <summary>
        /// Get only horizontal zoordinates ( X and Z ) of front.
        /// </summary>
        public Vector3 FrontHorizontal => new Vector3( _front.X, 0.0f, _front.Z );

        /// <summary>
        /// Camera's front vector.
        /// </summary>
        public Vector3 Front => _front;

        /// <summary>
        /// Get only horizontal zoordinates ( X and Z ) of right.
        /// </summary>
        public Vector3 RightHorizontal => new Vector3( _right.X, 0.0f, _right.Z );

        /// <summary>
        /// Camera's right vector.
        /// </summary>
        public Vector3 Right => _right;

        /// <summary>
        /// Camera's up vector.
        /// </summary>
        public Vector3 Up => _up;

        /// <summary>
        /// Create rotation around axis X using degrees.
        /// </summary>
        public float RotationX
        {
            get => MathHelper.RadiansToDegrees( _rotationX );

            set
            {
                _rotationX = MathHelper.DegreesToRadians( value );

                Update();
            }
        }

        /// <summary>
        /// Create rotation around axis Y using degrees.
        /// </summary>
        public float RotationY
        {
            get => MathHelper.RadiansToDegrees( _rotationY );

            set
            {
                // do not allow to rotate camera upside down
                float angle = MathHelper.Clamp( value, -89.0f, 89.0f );

                _rotationY = MathHelper.DegreesToRadians( angle );

                Update();
            }
        }

        /// <summary>
        /// Getter for view matrix.
        /// </summary>
        public Matrix4 ViewMatrix
        {
            // i guess magic ^^
            get => Matrix4.LookAt( Position, Position + _front, _up );
        }

        /// <summary>
        /// Getter for projection matrix.
        /// </summary>
        public Matrix4 ProjectionMatrix
        {
            get => Matrix4.CreatePerspectiveFieldOfView( _fov, _aspectRatio, 0.01f, 100.0f );
        }

        /// <summary>
        /// Constructor for <c> Camera </c>.
        /// </summary>
        /// <param cref="width"> Current width of window. </param>
        /// <param cref="height"> Current height of window. </param>
        public Camera( int width, int height )
        {
            AdjustAspectRatio( width, height );
        }

        /// <summary>
        /// Use this method to adjust current aspect ratio of camera, so
        /// projection matrix will be up to date for rendering.
        /// </summary>
        /// <param cref="width"> Current width of window. </param>
        /// <param cref="height"> Current height of window. </param>
        public void AdjustAspectRatio( int width, int height )
        {
            _aspectRatio = ( float ) width / height;
        }

        private void Update()
        {
            _front.X = ( float ) Math.Cos( _rotationY ) * ( float ) Math.Cos( _rotationX );
            _front.Y = ( float ) Math.Sin( _rotationY );
            _front.Z = ( float ) Math.Cos( _rotationY ) * ( float ) Math.Sin( _rotationX );

            _front.Normalize();

            _right = Vector3.Normalize( Vector3.Cross( _front, Vector3.UnitY ) );
            _up = Vector3.Normalize( Vector3.Cross( _right, _front ) );
        }
    }
}
