using BlockGame.Game;

using System.Collections.Generic;

namespace BlockGame.Graphics
{
    /// <summary>
    /// Wrap some functionality around object for bit more optimalized
    /// rendering chunks of map.
    /// </summary>
    public class ChunkModel : Model
    {
        private static readonly float BlockWidth = 1.0f;

        private static readonly float ChunkWidth = BlockWidth * Chunk.ChunkSize;

        private static TextureAtlas _textureAtlas = new TextureAtlas(
            "../Resources/Atlas.png",
            8
        );

        private List< Vertex > _loadedVertices = new List< Vertex >();

        private List< uint > _loadedIndices = new List< uint >();

        /// <summary>
        /// Construct empty model for chunk which can be loaded with
        /// data using methods and later on drawn.
        /// </summary>
        public ChunkModel( Chunk chunk )
            : base( new Vertex[ 0 ] {}, new uint[ 0 ] {}, ChunkModel._textureAtlas.Handle )
        {
            // TODO Optimalization class
            float startX = chunk.Pos.X * ChunkWidth;
            float startY = chunk.Pos.Y * ChunkWidth;
            float startZ = chunk.Pos.Z * ChunkWidth;
            for ( int x = 0; x < Chunk.ChunkSize; x++ )
            {
                for ( int y = 0; y < Chunk.ChunkSize; y++ )
                {
                    for ( int z = 0; z < Chunk.ChunkSize; z++ )
                    {
                        if ( chunk.Blocks[ x, y, z ] != Chunk.Block.Air )
                        {
                            AddCube(
                                new Point(
                                    startX + x * BlockWidth,
                                    startY + y * BlockWidth,
                                    startZ + z * BlockWidth
                                ),
                                ( int ) chunk.Blocks[ x, y, z ]
                            );
                        }
                    }
                }
            }

            Update();
        }

        // TODO implement
        public void Update()
        {
            /// Change buffers
            Vertices = _loadedVertices.ToArray();
            Indices = _loadedIndices.ToArray();
        }

        // A is left bottom front point of the cube
        private void AddCube( Point A, int textureIndex )
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

            // right up back point of cube
            Point B = new Point( A.X + BlockWidth, A.Y + BlockWidth, A.Z - BlockWidth );

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
