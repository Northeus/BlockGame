using System.Collections.Generic;

namespace BlockGame.Game
{
    /// <summary>
    /// Instance of <c> World </c> class store data about world in chunks
    /// of blocks.
    /// </summary>
    public class World
    {
        /// <summary>
        /// Width of world ( <c> WorldMap </c> ) in X's coordinate.
        /// </summary>
        public static readonly int WidthX = 8;

        /// <summary>
        /// Height of world ( <c> WorldMap </c> ) in Y's coordinate.
        /// </summary>
        public static readonly int WidthZ = 8;

        /// <summary>
        /// Width of world ( <c> WorldMap </c> ) in Z's coordinate.
        /// </summary>
        public static readonly int Height = 4;

        /// <summary>
        /// Store chunks of world.
        /// </summary>
        public readonly Chunk[,,] WorldMap = new Chunk[ WidthX, Height, WidthZ ];

        private List< Chunk.Position > _updatedChunks = new List< Chunk.Position >();

        /// <summary>
        /// Store positions of changed chunks. To reset this buffer
        /// use <see cref="Updated"/>.
        /// </summary>
        public List< Chunk.Position > UpdatedChunks => _updatedChunks;

        /// <summary>
        /// Get coords of X's coord center in world.
        /// </summary>
        public static int MiddleX => WidthX / 2;

        /// <summary>
        /// Get coords of Z's coord center in world.
        /// </summary>
        public static int MiddleZ => WidthZ / 2;

        /// <summary>
        /// Generate new world map on construction.
        /// </summary>
        public World()
        {
            GenerateMap();
        }

        public void Updated()
        {
            _updatedChunks.Clear();
        }

        private void GenerateMap()
        {
            for ( int x = 0; x < WidthX; x++ )
            {
                for ( int y = 0; y < Height; y++ )
                {
                    for ( int z = 0; z < WidthZ; z++ )
                    {
                        WorldMap[ x, y, z ] = new Chunk(
                            x - MiddleX,
                            y,
                            z - MiddleZ
                        );
                    }
                }
            }
        }
    }
}
