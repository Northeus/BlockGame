using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

using System.Linq;

namespace BlockGame.Graphics
{
    /// <summary>
    /// Class representing OpeGL model which can be simply drawn after
    /// initialization of enviroment via using  <see cref="Draw"/>.
    /// </summary>
    public class Model
    {
        public static readonly Shader _shader = new Shader(
            "BlockGame.Graphics/Shaders/ModelShader.vert",
            "BlockGame.Graphics/Shaders/ModelShader.frag"
        );

        private readonly Texture _texture;

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
        public Model( Vertex[] vertices, uint[] indices, Texture texture )
        {
            _texture = texture;

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

            GL.VertexAttribPointer(
                1,      // location
                2,      // count
                VertexAttribPointerType.Float,
                false,  // normalize
                Vertex.Size,
                Vertex.TextureCoordsOffset
            );

            GL.EnableVertexAttribArray( 0 );
            GL.EnableVertexAttribArray( 1 );

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
        ///
        /// </summary>
        /// <param cref="view"> Matrix of view transformation. </param>
        /// <param cref="projection"> Matrix of projection transform. </param>
        public static void AdjustMatrices( Matrix4 view, Matrix4 projection )
        {
            _shader.LoadMatrix4( "view", view );
            _shader.LoadMatrix4( "projection", projection );
        }

        /// <summary>
        /// Draw object stored in <c> _vertices </c>
        /// via indexes stored in <c> _indices </c>
        /// and use texture from <c> _texture </c>.
        /// Method will also set VAO to null.
        /// </summary>
        public void Draw()
        {
            // TODO might be loaded once trough some function before draw batch.
            Model._shader.Use();

            // TODO also might have own triger to make more optimalized batching
            _texture.Use();

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
