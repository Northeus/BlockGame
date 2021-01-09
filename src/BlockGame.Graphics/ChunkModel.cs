using System.Collections.Generic;

namespace BlockGame.Graphics
{
    /// <summary>
    /// Wrap some functionality around object for bit more optimalized
    /// rendering chunks of map.
    /// </summary>
    public class ChunkModel : Model
    {
        private static TextureAtlas _textureAtlas = new TextureAtlas(
            "../Resources/Atlas.png",
            2
        );

        private List< Vertex > _loadedVertices = null;

        private List< uint > _loadedIndices = null;

        /// <summary>
        /// Construct empty model for chunk which can be loaded with
        /// data using methods and later on drawn.
        /// </summary>
        public ChunkModel()
            : base( new Vertex[ 0 ] {}, new uint[ 0 ] {}, ChunkModel._textureAtlas.Handle )
        {

        }

        // TODO implement
        public void Update()
        {
            _loadedVertices = new List< Vertex >();
            _loadedIndices = new List< uint >();

            AddCube( new Point( -0.5f, -0.5f, 0.5f ), new Point( 0.5f, 0.5f, -0.5f ), 1 );

            /// Change buffers
            Vertices = _loadedVertices.ToArray();
            Indices = _loadedIndices.ToArray();
        }

        // A is left bottom front point of the cube
        // B is right up back point of the cube
        private void AddCube( Point A, Point B, int textureIndex )
        {
            int row = textureIndex / _textureAtlas.Rows;
            int col = textureIndex % _textureAtlas.Rows;

            // texture coords
            float[] tc = new float[ 4 ] {
                row * _textureAtlas.TextureSize, // top
                ( row + 1 ) * _textureAtlas.TextureSize, // bottom
                col * _textureAtlas.TextureSize, // left
                ( col + 1 ) * _textureAtlas.TextureSize  // right
            };

            // front
            _loadedVertices.Add( new Vertex( A.X, B.Y, A.Z, tc[ 2 ], tc[ 0 ] ) ); // LUF
            _loadedVertices.Add( new Vertex( B.X, B.Y, A.Z, tc[ 3 ], tc[ 0 ] ) ); // RUF
            _loadedVertices.Add( new Vertex( A.X, A.Y, A.Z, tc[ 2 ], tc[ 1 ] ) ); // LBF
            _loadedVertices.Add( new Vertex( B.X, A.Y, A.Z, tc[ 3 ], tc[ 1 ] ) ); // RBF

            _addIndices();

            // top
            _loadedVertices.Add( new Vertex( A.X, B.Y, B.Z, tc[ 2 ], tc[ 0 ] ) ); // LUB
            _loadedVertices.Add( new Vertex( B.X, B.Y, B.Z, tc[ 3 ], tc[ 0 ] ) ); // RUB
            _loadedVertices.Add( new Vertex( A.X, B.Y, A.Z, tc[ 2 ], tc[ 1 ] ) ); // LUF
            _loadedVertices.Add( new Vertex( B.X, B.Y, A.Z, tc[ 3 ], tc[ 1 ] ) ); // RUF

            _addIndices();

            // left
            _loadedVertices.Add( new Vertex( A.X, B.Y, A.Z, tc[ 2 ], tc[ 0 ] ) ); // LUF
            _loadedVertices.Add( new Vertex( A.X, B.Y, B.Z, tc[ 3 ], tc[ 0 ] ) ); // LUB
            _loadedVertices.Add( new Vertex( A.X, A.Y, A.Z, tc[ 2 ], tc[ 1 ] ) ); // LBF
            _loadedVertices.Add( new Vertex( A.X, A.Y, B.Z, tc[ 3 ], tc[ 1 ] ) ); // LBB

            _addIndices();

            // right
            _loadedVertices.Add( new Vertex( B.X, B.Y, A.Z, tc[ 2 ], tc[ 0 ] ) ); // RUF
            _loadedVertices.Add( new Vertex( B.X, B.Y, B.Z, tc[ 3 ], tc[ 0 ] ) ); // RUB
            _loadedVertices.Add( new Vertex( B.X, A.Y, A.Z, tc[ 2 ], tc[ 1 ] ) ); // RBF
            _loadedVertices.Add( new Vertex( B.X, A.Y, B.Z, tc[ 3 ], tc[ 1 ] ) ); // RBB

            _addIndices();

            // back
            _loadedVertices.Add( new Vertex( A.X, B.Y, B.Z, tc[ 2 ], tc[ 0 ] ) ); // LUB
            _loadedVertices.Add( new Vertex( B.X, B.Y, B.Z, tc[ 3 ], tc[ 0 ] ) ); // RUB
            _loadedVertices.Add( new Vertex( A.X, A.Y, B.Z, tc[ 2 ], tc[ 1 ] ) ); // LBB
            _loadedVertices.Add( new Vertex( B.X, A.Y, B.Z, tc[ 3 ], tc[ 1 ] ) ); // RBB

            _addIndices();

            // bottom
            _loadedVertices.Add( new Vertex( A.X, A.Y, B.Z, tc[ 2 ], tc[ 0 ] ) ); // LBB
            _loadedVertices.Add( new Vertex( B.X, A.Y, B.Z, tc[ 3 ], tc[ 0 ] ) ); // RBB
            _loadedVertices.Add( new Vertex( A.X, A.Y, A.Z, tc[ 2 ], tc[ 1 ] ) ); // LBF
            _loadedVertices.Add( new Vertex( B.X, A.Y, A.Z, tc[ 3 ], tc[ 1 ] ) ); // RBF

            _addIndices();

            // local method for adding indices of last squere
            void _addIndices()
            {
                uint firstIndex = ( uint ) _loadedVertices.Count - 4;

                _loadedIndices.AddRange( new uint[ 6 ] {
                    firstIndex, firstIndex + 1, firstIndex + 2,
                    firstIndex + 1, firstIndex + 2, firstIndex + 3
                } );
            }
        }

        private struct Point
        {
            public float X, Y, Z;

            public Point( float x, float y, float z )
            {
                X = x;
                Y = y;
                Z = z;
            }
        }
    }
}
