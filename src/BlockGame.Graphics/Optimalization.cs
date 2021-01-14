using BlockGame.Game;

using System;

namespace BlockGame.Graphics
{
    /// <summary>
    /// Class <c> Optimalization </c> should be used for loading
    /// data in bit more optimalized way ( aka. premature optimalization ).
    /// </summary>
    public static class Optimalization
    {
        private static readonly int _RenderDistance = 3;

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

            int startX = Math.Max( center.X - World.MiddleX, 0 );
            int startY = Math.Max( center.Y, 0 );
            int startZ = Math.Max( center.Z - World.MiddleZ, 0 );

            int boundX = Math.Min( startX + _RenderDistance, World.WidthX );
            int boundY = Math.Min( startY + _RenderDistance, World.Height );
            int boundZ = Math.Min( startZ + _RenderDistance, World.WidthZ );

            for ( int x = startX; x < boundX; x++ )
            {
                for ( int y = startY; y < boundY; y++ )
                {
                    for ( int z = startZ; z < boundZ; z++ )
                    {
                        ChunkRenderer.AddChunk( world.WorldMap[ x, y, z ] );
                        // TODO side rendering method
                    }
                }
            }
        }
    }
}
