namespace BlockGame.Graphics
{
    /// <summary>
    /// Texture atlases are able to hold many textures in one bigger texture.
    /// However given texture should have resolution exactly any power of two
    /// ( like 512 x 152 or 1024 x 1024 ).
    /// Do not forget to use shader with uniform <c> offset </c>.
    /// </summary>
    public class TextureAtlas
    {
        /// <summary>
        /// Represent number of textures per line / column in atlas.
        /// </summary>
        /// <example>
        /// Rows = 5, so we have 5 x 5 atlas with 25 textures.
        /// </example>
        public readonly int Rows;

        /// <summary>
        /// Handle for texture in case you want to use it separatly.
        /// </summary>
        public readonly Texture Handle;

        /// <summary>
        /// Normalized size of one texture side.
        /// </summary>
        public float TextureSize
        {
            get => 1.0f / Rows;
        }

        /// <summary>
        /// Create texture atlas from image represented by path.
        /// Each texture have same portion of image and are separated
        /// by lines, which count is given via <see cref="rows"/>.
        /// </summary>
        /// <param cref="path"> Path to .png image. </param>
        /// <param cref="rows"> Numbers of rows and columns. </param>
        /// <seealso cref="Texture"/>
        /// <example>
        /// If we pass 512 x 512 picture and chose rows = 4,
        /// then we get texture atlas with 16 ( 4 x 4 ) textures,
        /// where each texture's resolution is 128 x 128 ( 512 / 4 ).
        /// </example>
        public TextureAtlas( string path, int rows )
        {
            Handle = new Texture( path );

            Rows = rows;
        }

        /// <summary>
        /// Returns 4 values representing texture coordinates from atlas
        /// in given order: top, bottom, left, right.
        /// </summary>
        /// <param cref="textureIndex"> Index into atlas. </param>
        /// <returns>
        /// Array of 4 floats representing texture position within atlas.
        /// <returns>
        public float[] TextureCoords( int textureIndex )
        {
            int row = textureIndex / Rows;
            int col = textureIndex % Rows;

            return new float[ 4 ] {
                row * TextureSize,          // top
                ( row + 1 ) * TextureSize,  // bottom
                col * TextureSize,          // left
                ( col + 1 ) * TextureSize   // right
            };
        }
    }
}
