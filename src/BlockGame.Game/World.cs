namespace BlockGame.Game
{
    /// <summary>
    /// Instance of <c> World </c> class store data about world in chunks
    /// of blocks.
    /// </summary>
    public class World
    {
        // TODO
        public readonly Chunk[,,] WorldMap = new Chunk[ 2, 1, 2 ]
        {
            { { new Chunk( 0, 0, 0 ), new Chunk( 0, 0, 1 ) } },
            { { new Chunk( 1, 0, 0 ), new Chunk( 1, 0, 1 ) } }
        };

        // TODO
        public World()
        {

        }
    }
}
