using BlockGame.Graphics;

namespace BlockGame.Graphics.Data
{
    /// <summary>
    /// Some methods for generating basic shapes.
    /// </summary>
    public static class Shapes
    {
        /// <summary>
        /// Construct cuboid from two points, which represent
        /// oposite corners ( ends of main diagonal ).
        /// </summary>
        /// <param cref="A"> First point of cuboid. </param>
        /// <param cref="B"> Second point of cuboid. </param>
        /// <param cref="tc"> Coords for points texture coords. </param>
        /// <returns> Array of vertices representing cuboid. </returns>
        public static Vertex[] CuboidVertices( Point A, Point B, float[] tc )
        {
            // It's ugly but it's simple ^^
            // comments after each line is point coordination:
            // { left / right } { up / bottom } { front / back }
            // first point is counted as left front bottom
            return new Vertex[ 24 ] {
                // front
                new Vertex( A.X, B.Y, A.Z, tc[ 2 ], tc[ 0 ] ), // LUF
                new Vertex( B.X, B.Y, A.Z, tc[ 3 ], tc[ 0 ] ), // RUF
                new Vertex( A.X, A.Y, A.Z, tc[ 2 ], tc[ 1 ] ), // LBF
                new Vertex( B.X, A.Y, A.Z, tc[ 3 ], tc[ 1 ] ), // RBF

                // top
                new Vertex( A.X, B.Y, B.Z, tc[ 2 ], tc[ 0 ] ), // LUB
                new Vertex( B.X, B.Y, B.Z, tc[ 3 ], tc[ 0 ] ), // RUB
                new Vertex( A.X, B.Y, A.Z, tc[ 2 ], tc[ 1 ] ), // LUF
                new Vertex( B.X, B.Y, A.Z, tc[ 3 ], tc[ 1 ] ), // RUF

                // left
                new Vertex( A.X, B.Y, A.Z, tc[ 2 ], tc[ 0 ] ), // LUF
                new Vertex( A.X, B.Y, B.Z, tc[ 3 ], tc[ 0 ] ), // LUB
                new Vertex( A.X, A.Y, A.Z, tc[ 2 ], tc[ 1 ] ), // LBF
                new Vertex( A.X, A.Y, B.Z, tc[ 3 ], tc[ 1 ] ), // LBB

                // right
                new Vertex( B.X, B.Y, A.Z, tc[ 2 ], tc[ 0 ] ), // RUF
                new Vertex( B.X, B.Y, B.Z, tc[ 3 ], tc[ 0 ] ), // RUB
                new Vertex( B.X, A.Y, A.Z, tc[ 2 ], tc[ 1 ] ), // RBF
                new Vertex( B.X, A.Y, B.Z, tc[ 3 ], tc[ 1 ] ), // RBB

                // back
                new Vertex( A.X, B.Y, B.Z, tc[ 2 ], tc[ 0 ] ), // LUB
                new Vertex( B.X, B.Y, B.Z, tc[ 3 ], tc[ 0 ] ), // RUB
                new Vertex( A.X, A.Y, B.Z, tc[ 2 ], tc[ 1 ] ), // LBB
                new Vertex( B.X, A.Y, B.Z, tc[ 3 ], tc[ 1 ] ), // RBB

                // bottom
                new Vertex( A.X, A.Y, B.Z, tc[ 2 ], tc[ 0 ] ), // LBB
                new Vertex( B.X, A.Y, B.Z, tc[ 3 ], tc[ 0 ] ), // RBB
                new Vertex( A.X, A.Y, A.Z, tc[ 2 ], tc[ 1 ] ), // LBF
                new Vertex( B.X, A.Y, A.Z, tc[ 3 ], tc[ 1 ] )  // RBF
            };
        }

        /// <summary>
        /// Return indices coresponding to cuboid. Indices must be ordered exactly
        /// as method <see cerf="CuboidVertices"/> implements
        /// </summary>
        /// <param cref="lastIndex"> Index of last cuboid's vertice. </param>
        public static uint[] CuboidIndices( int length )
        {
            uint[] indices = new uint[ 36 ];

            uint indice = ( uint ) length - 24;

            for ( uint side = 0; side < 6; side++ )
            {
                for ( uint triangle = 0; triangle < 2; triangle++ )
                {
                    for ( uint vertex = 0; vertex < 3; vertex++ )
                    {
                        indices[ side * 6 + triangle * 3 + vertex ] =
                            indice + triangle + vertex;
                    }
                }

                indice += 4;
            }

            return indices;
        }
    }


    /// <summary>
    /// Helpeing data structure for creating different shapes.
    /// </summary>
    public struct Point
    {
        /// <summary>
        /// X's coordinate of point.
        /// </summary>
        public float X;

        /// <summary>
        /// Y's coordinate of point.
        /// </summary>
        public float Y;

        /// <summary>
        /// Z's coordinate of point.
        /// </summary>
        public float Z;

        /// <summary>
        /// Construct point using three floats representing
        /// his position in world's space.
        /// </summary>
        /// <param cref="x"> X's coordinate of point </param>
        /// <param cref="y"> Y's coordinate of point </param>
        /// <param cref="z"> Z's coordinate of point </param>
        public Point( float x, float y, float z )
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
