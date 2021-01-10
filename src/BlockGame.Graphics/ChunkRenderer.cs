using BlockGame.Game;

using System.Collections.Generic;

namespace BlockGame.Graphics
{
    /// <summary>
    /// Handler for chunks, which should be rendered.
    /// </summary>
    public static class ChunkRenderer
    {
        private static Dictionary< Chunk.Position, ChunkModel > Models =
            new Dictionary< Chunk.Position, ChunkModel >();

        // TODO
        public static void AddChunk( Chunk chunk )
        {
            Models[ chunk.Pos ] = new ChunkModel( chunk );
        }

        // TODO
        public static void UpdateChunk( Chunk.Position pos )
        {
            // TODO
        }

        // TODO
        public static void Draw()
        {
            foreach ( var item in Models )
            {
                item.Value.Draw();
            }
        }
    }
}
