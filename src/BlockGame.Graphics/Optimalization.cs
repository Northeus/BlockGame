using BlockGame.Game;
using BlockGame.Graphics.Data;

using System;

// You don't wanna see that :)
namespace BlockGame.Graphics
{
    /// <summary>
    /// Class <c> Optimalization </c> should be used for loading
    /// data in bit more optimalized way ( aka. premature optimalization ).
    /// </summary>
    public static class Optimalization
    {
        private static readonly int _RenderDistance = 1;

        /// <summary>
        /// Method which loads only chunks in render distance.
        /// <summary>
        /// <param cref="world"> World to be loaded. </param>
        /// <param cref="camera"> Position for choosing chunks. </param>
        public static void LoadChunks( World world, Camera camera )
        {
            Chunk.Position center = new Chunk.Position(
                ( int ) ( camera.Position.X / ChunkModel.ChunkWidth ),
                ( int ) ( camera.Position.Y / ChunkModel.ChunkWidth ),
                ( int ) ( camera.Position.Z / ChunkModel.ChunkWidth )
            );

            int startX = Math.Max( center.X + World.MiddleX - _RenderDistance, 0 );
            int startY = Math.Max( center.Y, 0 );
            int startZ = Math.Max( center.Z + World.MiddleZ - _RenderDistance, 0 );

            int boundX = Math.Min( startX + 2 * _RenderDistance + 1, World.WidthX );
            int boundY = Math.Min( startY + 2 * _RenderDistance + 1, World.Height );
            int boundZ = Math.Min( startZ + 2 * _RenderDistance + 1, World.WidthZ );

            for ( int x = startX; x < boundX; x++ )
            {
                for ( int y = startY; y < boundY; y++ )
                {
                    for ( int z = startZ; z < boundZ; z++ )
                    {
                        ChunkRenderer.AddChunk( world, x, y, z );
                    }
                }
            }
        }

        /// <summary>
        /// Load only visible blocks for rendering.
        /// </summary>
        /// <param cref="model"> Model which will render given chunk. </param>
        /// <param cref="world"> World which is containing given chunk. </param>
        /// <param cref="chunkX"> First index of chunk in world.WorldMap. </param>
        /// <param cref="chunkY"> Second index of chunk in world.WorldMap. </param>
        /// <param cref="chunkZ"> Third index of chunk in world.WorldMap. </param>
        public static void LoadBlocks( ChunkModel model, World world, int chunkX, int chunkY, int chunkZ )
        {
            Chunk chunk = world.WorldMap[ chunkX, chunkY, chunkZ ];

            GetChunkOffset( chunk, out float startX, out float startY, out float startZ );

            for ( int x = 0; x < Chunk.Size; x++ )
            {
                for ( int y = 0; y < Chunk.Size; y++ )
                {
                    for ( int z = 0; z < Chunk.Size; z++ )
                    {
                        System.Console.WriteLine( $"{x}/{y}/{z}");
                        if ( chunk.Blocks[ x, y, z ] != Chunk.Block.Air
                             && IsSurrounded( world, chunkX, chunkY, chunkZ, x, y, z
                        ) )
                        {
                            model.AddCube(
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

        private static void GetChunkOffset( Chunk chunk, out float x, out float y, out float z )
        {
            x = chunk.Pos.X * ChunkModel.ChunkWidth;
            y = chunk.Pos.Y * ChunkModel.ChunkWidth;
            z = chunk.Pos.Z * ChunkModel.ChunkWidth;
        }

        private static bool IsSurrounded( World world,
            int chunkX, int chunkY, int chunkZ, int x, int y, int z
        ) =>
                ( ! IsOnBoarder( x, y, z ) && ( CheckNeighbourInner(
                        world.WorldMap[ chunkX, chunkY, chunkZ ].Blocks, x, y, z
                ) ) )
            ||  ( IsOnBoarder( x, y, z ) && ( CheckNeighbourBoarder(
                        world, chunkX, chunkY, chunkZ, x, y, z
                ) ) );

        private static bool IsOnBoarder( int x, int y, int z ) =>
                x == 0 || y == 0 || z == 0 || x == Chunk.Size - 1
            ||  y == Chunk.Size - 1 || z == Chunk.Size - 1;

        private static bool CheckNeighbourInner( Chunk.Block[,,] chunk, int x, int y, int z ) =>
                chunk[ x - 1, y, z ] == Chunk.Block.Air || chunk[ x + 1, y, z ] == Chunk.Block.Air
            ||  chunk[ x, y - 1, z ] == Chunk.Block.Air || chunk[ x, y + 1, z ] == Chunk.Block.Air
            ||  chunk[ x, y, z - 1 ] == Chunk.Block.Air || chunk[ x, y, z + 1 ] == Chunk.Block.Air;

        private static bool CheckNeighbourBoarder( World world,
            int chunkX, int chunkY, int chunkZ, int x, int y, int z
        ) =>
                IsAir( world, chunkX, chunkY, chunkZ, x + 1, y, z )
            ||  IsAir( world, chunkX, chunkY, chunkZ, x - 1, y, z )
            ||  IsAir( world, chunkX, chunkY, chunkZ, x, y + 1, z )
            ||  IsAir( world, chunkX, chunkY, chunkZ, x, y - 1, z )
            ||  IsAir( world, chunkX, chunkY, chunkZ, x, y, z + 1 )
            ||  IsAir( world, chunkX, chunkY, chunkZ, x, y, z - 1 );

        private static bool IsAir( World world, int cX, int cY, int cZ, int x, int y, int z ) =>
            ( x, y, z ) switch
            {
                ( -1,           _,          _ )
                    => cX == 0 || IsAir( world, cX - 1, cY, cZ, Chunk.Size - 1, y, z ),

                ( Chunk.Size,   _,          _ )
                    => cX == World.WidthX - 1 || IsAir( world, cX + 1, cY, cZ, 0, y, z ),

                ( _,            -1,         _ )
                    => cY == 0 || IsAir( world, cX, cY - 1, cZ, x, Chunk.Size - 1, z ),

                ( _,            Chunk.Size, _ )
                    => cY == World.Height - 1 || IsAir( world, cX, cY + 1, cZ, x, 0, z ),

                ( _,            _,          -1 )
                    => cZ == 0 || IsAir( world, cX, cY, cZ - 1, x, y, Chunk.Size - 1 ),

                ( _,            _,         Chunk.Size )
                    => cZ == World.WidthZ - 1 || IsAir( world, cX, cY, cZ + 1, x, y, 0 ),

                _               => world.WorldMap[ cX, cY, cZ ].Blocks[ x, y, z ] == Chunk.Block.Air
            };
    }
}
