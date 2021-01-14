using BlockGame.Game;

using System.Collections.Generic;

namespace BlockGame.Graphics
{
    /// <summary>
    /// Handler for chunks, which should be rendered.
    /// </summary>
    public static class ChunkRenderer
    {
        private static Dictionary< Chunk.Position, ChunkModel > _models =
            new Dictionary< Chunk.Position, ChunkModel >();

        /// <summary>
        /// Will load chunk from world for rendering.
        /// </summary>
        /// <param cref="chunk"> World to be added for rendering. </param>
        public static void LoadChunks( World world, Camera camera )
        {
            Optimalization.LoadChunks( world, camera );
        }

        /// <summary>
        /// Add chunk model into renderer.
        /// </summary>
        /// <param cref="chunk"> Chunk to be added. </param>
        public static void AddChunk( Chunk chunk )
        {
            _models[ chunk.Pos ] = new ChunkModel( chunk );
        }

        /// <summary>
        /// Stop rendering chunk given by his position.
        /// </summary>
        /// <param cref="pos"> Position of given chunk.  </param>
        /// <returns>
        /// True, if chunk was found and removed, false otherwise.
        /// </returns>
        public static bool RemoveChunk( Chunk.Position pos )
        {
            return _models.Remove( pos, out _ );
        }

        /// <summary>
        /// Method will recreate chunk model, to ensure, that it
        /// represents curent look of model ( And neighbour sides also ).
        /// You should call this method, each time chunk is changed.
        /// </summary>
        /// <param cref="chunk"> Chunk to be updated. </param>
        public static void UpdateChunk( Chunk chunk )
        {
            _models[ chunk.Pos ] = new ChunkModel( chunk );
        }

        /// <summary>
        /// Draw every model, which is currently loaded using
        /// <see cref="AddChunk"/>.
        /// </summary>
        public static void Draw()
        {
            foreach ( ChunkModel model in _models.Values )
            {
                model.Draw();
            }
        }
    }
}
