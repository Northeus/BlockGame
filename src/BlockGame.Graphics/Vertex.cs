namespace BlockGame.Graphics
{
    /// <summary>
    /// Representation of one vertex, storing data needed to be drawn.
    /// </summary>
    /// <example>
    /// You can get whole array of data from array of vertices.
    /// <code>
    /// using System.Linq;
    ///
    /// Vertex[] arr = ...;
    ///
    /// float[] data = arr.SelectMany( vertex => vertex.Data ).ToArray;
    /// </code>
    /// </example>
    public class Vertex
    {
        /// <summary>
        /// Storing data about vertex in given order:
        /// <list type="number">
        /// <item>
        /// Three floats representing 3D coordinates.
        /// </item>
        /// <item>
        /// Two floats representing 2D coordinates within texture.
        /// </item>
        /// </list>
        /// </summary>
        public readonly float[] Data;

        /// <summary>
        /// Storing size of data stored in <c> Data </c>.
        /// </summary>
        public static int Size
        {
            get => 5 * sizeof( float );
        }

        /// <summary>
        /// Give data offset for coordinates of texture.
        /// </summary>
        public static int TextureCoordsOffset
        {
            get => 3 * sizeof( float );
        }

        /// <summary>
        /// Construct one point which might be loaded into model datas
        /// and used for rendering triangle on screen.
        /// Each coordinate should be normalized. Bottom left of
        /// texture is ( 0, 0 ).
        /// </summary>
        /// <param cref="x"> X's coordinate of the point. </param>
        /// <param cref="y"> Y's coordinate of the point. </param>
        /// <param cref="z"> Z's coordinate of the point. </param>
        /// <param cref="textureX"> X's coordinate into texture. </param>
        /// <param cref="textureY"> Y's coordinate into texture. </param>
        public Vertex( float x, float y, float z,
                       float textureX, float textureY )
        {
            Data = new float[ 5 ] { x, y, z, textureX, textureY };
        }
    }
}
