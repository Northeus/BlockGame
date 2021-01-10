namespace BlockGame.Game
{
    // TODO
    public class Chunk
    {
        // TODO
        public static readonly int ChunkSize = 16;

        // TODO
        public Block[,,] Blocks = new Block[ ChunkSize, ChunkSize, ChunkSize ];

        // TODO
        public readonly Position Pos;

        // TODO
        public Chunk( int posX, int posY, int posZ )
        {
            for ( int x = 0; x < ChunkSize; x++ )
            {
                for ( int y = 0; y < ChunkSize; y++ )
                {
                    for ( int z = 0; z < ChunkSize; z++ )
                    {
                        Blocks[ x, y, z ] = ( x == 0 || y == 0 || z == 15 ) ? Block.Rock : Block.Air;
                    }
                }
            }

            Pos = new Position( posX, posY, posZ );
        }

        /// <summary>
        /// Representation of every possible block.
        /// </summary>
        public enum Block : byte
        {
            Rock,
            Stone,
            Dirt,
            Grass,

            Air = 255
        }

        // TODO
        public struct Position
        {
            // TODO
            public int X, Y, Z;

            // TODO
            public Position( int x, int y, int z )
            {
                X = x;
                Y = y;
                Z = z;
            }
        }
    }
}
