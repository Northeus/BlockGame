using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace BlockGame.Graphics
{
    /// <summary>
    /// Handler for OpenGL shaders cappable of loading and compiling them
    /// from given source files.
    /// </summary>
    public class Shader
    {
        private readonly int _handle;

        private static Dictionary< string, int > _uniformLocations;

        /// <summary>
        /// Constructor for creating shader from given source files.
        /// Syntax error in files will be printed out.
        /// </summary>
        /// <exception cref="System.Exception">
        /// Thrown if program wont compile, will also print out error.
        /// </exception>
        /// <exception cref="System.Exception">
        /// Thrown if program wont link.
        /// </exception>
        /// <param name="vertPath">
        /// Path to source file with vertex shader relative Main folder
        /// </param>
        /// <param name="FragPath">
        /// Path to source file with fragment shader relative to Main folder
        /// </param>
        public Shader( string vertPath, string fragPath )
        {
            int vertexShader = CreateShader( vertPath, ShaderType.VertexShader );

            int fragmentShader = CreateShader( fragPath, ShaderType.FragmentShader );

            _handle = JoinShaders( vertexShader, fragmentShader );

            // Both shaders are now copied into main shader in _handle.
            DetachShader( _handle, vertexShader );
            DetachShader( _handle, fragmentShader );

            _uniformLocations = GetUniformLocations( _handle );
        }

        /// <summary>
        /// Bind current shader to be used.
        /// </summary>
        public void Use()
        {
            GL.UseProgram( _handle );
        }

        /// <summary>
        /// Free resources attached to shader program.
        /// </summary>
        public void Free()
        {
            GL.DeleteProgram( _handle );
        }

        private static int CreateShader( string path, ShaderType shaderType )
        {
            string source = LoadSource( path );

            int shader = GL.CreateShader( shaderType );

            GL.ShaderSource( shader, source );

            CompileShader( shader );

            return shader;
        }

        private static string LoadSource( string path )
        {
            using ( StreamReader reader = new StreamReader( path, Encoding.UTF8 ) )
            {
                return reader.ReadToEnd();
            }
        }

        private static void CompileShader( int shader )
        {
            GL.CompileShader( shader );

            GL.GetShader( shader, ShaderParameter.CompileStatus, out int code );

            if ( code != ( int ) All.True )
            {
                string infoLog = GL.GetShaderInfoLog( shader );

                throw new Exception(
                    $"Error occured whilst compiling Shader({ shader })."
                    + $"\n\n{ infoLog }"
                );
            }
        }

        private static void LinkProgram( int program )
        {
            GL.LinkProgram( program );

            GL.GetProgram( program, GetProgramParameterName.LinkStatus, out int code );

            if ( code != ( int ) All.True )
            {
                throw new Exception( $"Error occured while linking Program({ program })" );
            }
        }

        private static int JoinShaders( int vertexShader, int fragmentShader )
        {
            int handle = GL.CreateProgram();

            GL.AttachShader( handle, vertexShader );
            GL.AttachShader( handle, fragmentShader );

            LinkProgram( handle );

            return handle;
        }

        private static void DetachShader( int handle, int shader )
        {
            GL.DetachShader( handle, shader );

            GL.DeleteShader( shader );
        }

        // Cache uniform locations from shader sources.
        private static Dictionary< string, int > GetUniformLocations( int handle )
        {
            var locations = new Dictionary< string, int >();

            GL.GetProgram(
                handle,
                GetProgramParameterName.ActiveUniforms,
                out int numberOfUniforms
            );

            for ( int i = 0; i < numberOfUniforms; i++ )
            {
                string name = GL.GetActiveUniform( handle, i, out _, out _ );

                int location = GL.GetUniformLocation( handle, name );

                locations.Add( name, location );
            }

            return locations;
        }
    }
}
