using OpenTK.Graphics.OpenGL4;

using OpenGLPixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;

using System.Drawing;
using System.Drawing.Imaging;

using DrawingPixelFormat = System.Drawing.Imaging.PixelFormat;

namespace BlockGame.Graphics
{
    public class Texture
    {
        private readonly int _handle;

        /// <summary>
        /// Create OpenGL texture from given image.
        /// </summary>
        /// <exception>
        /// Constructor will throw error, if path is incorrect.
        /// </exception>
        /// <param creg="path">
        /// Specified location of .png file with texture image.
        /// </param>
        public Texture( string path )
        {
            _handle = GL.GenTexture();

            // bind handle
            Use();

            using ( Bitmap image = new Bitmap( path ) )
            {
                var data = image.LockBits(
                    new Rectangle( 0, 0, image.Width, image.Height ),
                    ImageLockMode.ReadOnly,
                    DrawingPixelFormat.Format32bppArgb
                );

                GL.TexImage2D(
                    TextureTarget.Texture2D,
                    0,
                    PixelInternalFormat.Rgba,
                    image.Width,
                    image.Height,
                    0,
                    OpenGLPixelFormat.Bgra,
                    PixelType.UnsignedByte,
                    data.Scan0
                );
            }

            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureMinFilter,
                ( int ) TextureMinFilter.Nearest
            );
            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureMagFilter,
                ( int ) TextureMagFilter.Nearest
            );

            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureWrapS,
                ( int ) TextureWrapMode.Repeat
            );
            GL.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureWrapT,
                ( int ) TextureWrapMode.Repeat
            );

            // for scale purposees
            GL.GenerateMipmap( GenerateMipmapTarget.Texture2D );
        }

        /// <summary>
        /// Automaticly free up texture after finalization.
        /// </summary>
        ~Texture()
        {
            GL.DeleteTexture( _handle );
        }

        /// <summary>
        /// Bind texture before drawing object.
        /// </summary>
        /// <param cref="unit">
        /// Specified OpenGL unit, where will be texture stored.
        /// Default is Texture0.
        /// </param>
        /// <example>
        /// For different textures at same time use different unit.
        /// <code>
        /// _texture1 = Texture( "..." );
        /// _texture2 = Texture( "..." );
        /// _texture1.Use();
        /// _texture2.Use( TextureUnit.Texture1 );
        /// </code>
        /// <code>
        /// uniform sampler2D texture0;
        /// uniform sampler2D texture1;
        /// </code>
        /// </example>
        public void Use( TextureUnit unit = TextureUnit.Texture0 )
        {
            GL.ActiveTexture( unit );

            GL.BindTexture( TextureTarget.Texture2D, _handle );
        }
    }
}
