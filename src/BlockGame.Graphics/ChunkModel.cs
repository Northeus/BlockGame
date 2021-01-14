using BlockGame.Game;
using BlockGame.Graphics.Data;

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

        /// <summary>
        /// Represent width of chunk in OpenGL coords.
        /// </summary>
        public static readonly float ChunkWidth = BlockWidth * Chunk.ChunkSize;

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

        private void AddCube( Point A, int textureIndex )
        {
            float[] textureCoords = _textureAtlas.TextureCoords( textureIndex );

            Point B = new Point( A.X + BlockWidth, A.Y + BlockWidth, A.Z - BlockWidth );

            _loadedVertices.AddRange( Shapes.CuboidVertices( A, B, textureCoords ) );
            _loadedIndices.AddRange( Shapes.CuboidIndices( _loadedVertices.Count ) );
        }
    }
}
