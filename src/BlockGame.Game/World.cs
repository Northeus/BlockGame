namespace BlockGame.Game
{
    /// <summary>
    /// Instance of <c> World </c> class store data about world in chunks
    /// of blocks.
    /// </summary>
    public class World
    {
        public static readonly int WidthX = 3;

        public static readonly int WidthZ = 3;

        public static readonly int Height = 1;

        /// <summary>
        /// Store chunks of world.
        /// </summary>
        public readonly Chunk[,,] WorldMap = new Chunk[ WidthX, Height, WidthZ ];

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
