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
        /// <summary>
        /// Width of one block in OpenGL coords.
        /// </summary>
        public static readonly float BlockWidth = 1.0f;

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
        /// <param cref="world"> World containing map of chunks. </param>
        /// <param cref="x"> First index into map of chunks. </param>
        /// <param cref="y"> Second index into map of chunks. </param>
        /// <param cref="z"> Third index into map of chunks. </param>
        public ChunkModel( World world, int x, int y, int z )
            : base( new Vertex[ 0 ] {}, new uint[ 0 ] {}, ChunkModel._textureAtlas.Handle )
        {
            LoadInnerBlocks( world.WorldMap[ x, y, z ] );
            LoadBoarderBlocks();

            BufferData();
        }

        /// <summary>
        /// Update chunk model.
        /// </summary>
        /// <param cref="world"> World containing map of chunks. </param>
        /// <param cref="x"> First index into map of chunks. </param>
        /// <param cref="y"> Second index into map of chunks. </param>
        /// <param cref="z"> Third index into map of chunks. </param>
        public void Update( World world, int x, int y, int z )
        {
            LoadInnerBlocks( world.WorldMap[ x, y, z] );
            LoadBoarderBlocks();

            BufferData();
        }

        /// <summary>
        /// Add cube into model defined by her front bottom left corner.
        /// </summary>
        /// <param cref="A"> Point of given corner. </param>
        /// <param cref="textureIndex"> Index into texture atlas of cubes. </param>
        public void AddCube( Point A, int textureIndex )
        {
            float[] textureCoords = _textureAtlas.TextureCoords( textureIndex );

            Point B = new Point( A.X + BlockWidth, A.Y + BlockWidth, A.Z - BlockWidth );

            _loadedVertices.AddRange( Shapes.CuboidVertices( A, B, textureCoords ) );
            _loadedIndices.AddRange( Shapes.CuboidIndices( _loadedVertices.Count ) );
        }

        private void BufferData()
        {
            Vertices = _loadedVertices.ToArray();
            Indices = _loadedIndices.ToArray();
        }

        private void LoadInnerBlocks( Chunk chunk )
        {
            float startX = chunk.Pos.X * ChunkModel.ChunkWidth;
            float startY = chunk.Pos.Y * ChunkModel.ChunkWidth;
            float startZ = chunk.Pos.Z * ChunkModel.ChunkWidth;

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
                                    startX + x * ChunkModel.BlockWidth,
                                    startY + y * ChunkModel.BlockWidth,
                                    startZ + z * ChunkModel.BlockWidth
                                ),
                                ( int ) chunk.Blocks[ x, y, z ]
                            );
                        }
                    }
                }
            }
        }

        // TODO
        private void LoadBoarderBlocks()
        {
            // TODO
        }
    }
}
