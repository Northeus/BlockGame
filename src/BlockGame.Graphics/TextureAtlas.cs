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

        private Texture _texture;

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
            _texture = new Texture( path );

            Rows = rows;
        }
    }
}