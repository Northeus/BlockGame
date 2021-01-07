namespace BlockGame.Renderer
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
        /// </list>
        /// </summary>
        public readonly float[] Data;

        /// <summary>
        /// Storing size of data stored in <c> Data </c>.
        /// </summary>
        public static int Size
        {
            get => 3 * sizeof( float );
        }

        /// <summary>
        /// Construct one point which might be loaded into model datas
        /// and used for rendering triangle on screen.
        /// </summary>
        /// <param cref="x"> X's coordinate of the point. </param>
        /// <param cref="y"> Y's coordinate of the point. </param>
        /// <param cref="z"> Z's coordinate of the point. </param>
        public Vertex( float x, float y, float z )
        {
            Data = new float[ 3 ] { x, y, z };
        }
    }
}
