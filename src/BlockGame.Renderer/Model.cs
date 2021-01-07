using OpenTK.Graphics.OpenGL4;

using System.Linq;

namespace BlockGame.Renderer
{
    /// <summary>
    /// Class representing OpeGL model which can be simply drawn after
    /// initialization of enviroment via using  <see cref="Draw"/>.
    /// </summary>
    public class Model
    {
        private static readonly Shader _shader = new Shader(
            "BlockGame.Renderer/Shaders/Shader.vert",
            "BlockGame.Renderer/Shaders/Shader.frag"
        );

        private int _vertexArrayObject;

        private int _vertexBufferObject;

        private int _elementBufferObject;

        private Vertex[] _vertices;

        private uint[] _indices;

        private float[] _verticesData
        {
            get => _vertices.SelectMany( vertex => vertex.Data ).ToArray();
        }

        private int _verticesSize
        {
            get => _vertices.Length * Vertex.Size;
        }

        private int _indicesSize
        {
            get => _indices.Length * sizeof( uint );
        }

        /// <summary>
        /// Property which provide loading new Vertices into the model.
        /// It will also reinitialize VBO and set array buffer to null.
        /// </summary>
        public Vertex[] Vertices
        {
            set
            {
                _vertices = value;

                GL.BindBuffer( BufferTarget.ArrayBuffer, _vertexBufferObject );

                GL.BufferData(
                    BufferTarget.ArrayBuffer,
                    _verticesSize,
                    _verticesData,
                    BufferUsageHint.StaticDraw
                );

                GL.BindBuffer( BufferTarget.ArrayBuffer, 0 );
            }
        }

        /// <summary>
        /// Property which provide loading new Indices into the model.
        /// It will also reinitialize EBO and set element buffer to null.
        /// </summary>
        public uint[] Indices
        {
            set
            {
                _indices = value;

                GL.BindBuffer( BufferTarget.ElementArrayBuffer, _elementBufferObject );

                GL.BufferData(
                    BufferTarget.ElementArrayBuffer,
                    _indicesSize,
                    _indices,
                    BufferUsageHint.StaticDraw
                );

                GL.BindBuffer( BufferTarget.ElementArrayBuffer, 0 );
            }
        }

        /// <summary>
        /// Constructor will initialize buffers and store data for further
        /// rendering of model. Loaded data might be changed later on via
        /// properties <see cref="Vertices"/> and <see cref="Indices"/>.
        /// Constructor will also set VAO, VBO and EBO to null.
        /// </summary>
        /// <param cref="vertices"> Array of model vertices. </param>
        /// <param cref="indices"> Array of indices indexing vertices. </param>
        /// <seealso cref="Vertex"/>
        public Model( Vertex[] vertices, uint[] indices )
        {
            _vertexArrayObject = GL.GenVertexArray();
            _vertexBufferObject = GL.GenBuffer();
            _elementBufferObject = GL.GenBuffer();

            /// Using properties must be done after we have buffers prepared
            Vertices = vertices;
            Indices = indices;

            // Set bindings for further use
            GL.BindVertexArray( _vertexArrayObject );
            GL.BindBuffer( BufferTarget.ArrayBuffer, _vertexBufferObject );
            GL.BindBuffer( BufferTarget.ElementArrayBuffer, _elementBufferObject );

            GL.VertexAttribPointer(
                0,      // location
                3,      // count
                VertexAttribPointerType.Float,
                false,  // normalize
                Vertex.Size,
                0
            );

            GL.EnableVertexAttribArray( 0 );

            // Unbind resources
            GL.BindVertexArray( 0 );
            GL.BindBuffer( BufferTarget.ArrayBuffer, 0 );
            GL.BindBuffer( BufferTarget.ElementArrayBuffer, 0 );
        }

        ~Model()
        {
            GL.DeleteVertexArray( _vertexArrayObject );
            GL.DeleteBuffer( _vertexBufferObject );
            GL.DeleteBuffer( _elementBufferObject );
        }

        /// <summary>
        /// Clean up enviroment after end of execution of the program.
        /// </summary>
        public static void CleanUp()
        {
            _shader.Free();
        }

        /// <summary>
        /// Draw object stored in <c> _vertices </c>
        /// via indexes stored in <c> _indices </c>.
        /// Method will also set VAO to null.
        /// </summary>
        public void Draw()
        {
            // TODO might be loaded once trough some function before draw batch.
            Model._shader.Use();

            GL.BindVertexArray( _vertexArrayObject );

            GL.DrawElements(
                PrimitiveType.Triangles,
                _indices.Length,
                DrawElementsType.UnsignedInt,
                0
            );

            GL.BindVertexArray( 0 );
        }
    }
}