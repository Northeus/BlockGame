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
        /// <param cref="world"> World containing map of chunks. </param>
        /// <param cref="x"> First index into map of chunks. </param>
        /// <param cref="y"> Second index into map of chunks. </param>
        /// <param cref="z"> Third index into map of chunks. </param>
        public static void AddChunk( World world, int x, int y, int z )
        {
            _models[ world.WorldMap[ x, y, z ].Pos ] = new ChunkModel( world, x, y, z );
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
        /// Check for changes in world and load new data into buffers.
        /// </summary>
        /// <param cref="world"> Specified world for update. </param>
        public static void Update( World world )
        {
            foreach ( Chunk.Position pos in world.UpdatedChunks )
            {
                int x = pos.X + World.MiddleX;
                int y = pos.Y;
                int z = pos.Z + World.MiddleZ;

                _models[ pos ].Update( world, x, y, z );
            }

            world.Updated();
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
