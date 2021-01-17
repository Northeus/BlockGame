namespace BlockGame.Game
{
    /// <summary>
    /// Class can be used to get data about chunk for rendering <see cref="Blocks"/>,
    /// or to get some metadata for optimalization ( i.e. if chunk was changed ).
    /// </summary>
    public class Chunk
    {
        /// <summary>
        /// Size of every <see cref="Blocks"/> array. That means it represents
        /// each chunk si cube with size of side equal to this constant.
        /// </summary>
        public const int Size = 16;

        /// <summary>
        /// Array of blocsk in chunk, indices are x, y, z in given order.
        /// Y's coordination represent height.
        /// </summary>
        public Block[,,] Blocks = new Block[ Size, Size, Size ];

        /// <summary>
        /// Public variable which will give you position of chunk in the world.
        /// </summary>
        public readonly Position Pos;

        /// <summary>
        /// Generate chunk at given position.
        /// </summary>
        /// <param cref="posX"> X's coordinate of chunk. </param>
        /// <param cref="posY"> Y's coordinate of chunk. </param>
        /// <param cref="posZ"> Z's coordinate of chunk. </param>
        public Chunk( int posX, int posY, int posZ )
        {
            ClearChunk();

            for ( int x = 0; x < Size; x++ )
            {
                for ( int y = 0; y < Size; y++ )
                {
                    for ( int z = 0; z < Size; z++ )
                    {
                        int actualHeight = posY * Chunk.Size + y;

                        if ( actualHeight < 16 )
                        {
                            Blocks[ x, y, z ] = Block.Rock;
                        }
                        else if ( actualHeight < 20 )
                        {
                            Blocks[ x, y, z ] = Block.Dirt;
                        }
                        else if ( actualHeight < 21 )
                        {
                            Blocks[ x, y, z ] = Block.Grass;
                        }
                    }
                }
            }

            Pos = new Position( posX, posY, posZ );
        }

        /// <summary>
        /// Representation of every possible block.
        /// </summary>
        public enum Block : int
        {
            Air = -1,

            Rock = 0,
            Stone,
            Dirt,
            Grass
        }

        private void ClearChunk()
        {
            for ( int x = 0; x < Size; x++ )
            {
                for ( int y = 0; y < Size; y++ )
                {
                    for ( int z = 0; z < Size; z++ )
                    {
                        Blocks[ x, y, z ] = Block.Air;
                    }
                }
            }
        }

        /// <summary>
        /// Representation of chunk's positions.
        /// </summary>
        public struct Position
        {
            /// <summary>
            /// Separated coordinates of the chunk.
            /// </summary>
            public readonly int X, Y, Z;

            /// <summary>
            /// Constructor which set coordinates via given values.
            /// Y's coordinate describe height.
            /// </summary>
            /// <param cref="x"> X's coordinate of chunk. </param>
            /// <param cref="y"> Y's coordinate of chunk. </param>
            /// <param cref="z"> Z's coordinate of chunk. </param>
            public Position( int x, int y, int z )
            {
                X = x;
                Y = y;
                Z = z;
            }
        }
    }
}
