using BlockGame.Game;

namespace BlockGame.Graphics
{
    /// <summary>
    /// Class <c> Optimalization </c> should be used for loading
    /// data in bit more optimalized way ( aka. premature optimalization ).
    /// </summary>
    public static class Optimalization
    {
        /// <summary>
        /// Method which load only needed chunks for rendering.
        /// <summary>
        /// <param cref="world"> World to be loaded. </param>
        /// <param cref="camera"> Position for choosing chunks. </param>
        public static void LoadChunks( World world, Camera camera )
        {
            // TODO exchange with some chunk picking
            for ( int i = 0; i < world.WorldMap.GetLength( 0 ); i++ )
            {
                for ( int j = 0; j < world.WorldMap.GetLength( 1 ); j++ )
                {
                    for ( int k = 0; k < world.WorldMap.GetLength( 2 ); k++ )
                    {
                        ChunkRenderer.AddChunk( world.WorldMap[ i, j, k ] );
                    }
                }
            }
        }
    }
}
