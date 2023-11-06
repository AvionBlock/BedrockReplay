using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

namespace BedrockReplay
{
    public class Renderer : GameWindow
    {
        float[] vertices =
        {
            -0.5f, 0.5f, 0f, //Top Left
            0.5f, 0.5f, 0f, //Top Right
            0.5f, -0.5f, 0f, //Bottom Right
            -0.5f, -0.5f, 0f, //Bottom Left
        };

        float[] textCoords =
        {
            0f, 1f,
            1f, 1f,
            1f, 0f,
            0f, 0f
        };

        uint[] indices =
        {
            //TopTriangle
            0, 1, 2,
            //Bottom Triangle
            2, 3, 0
        };

        //Render Pipeline Variables
        int vao;
        int vbo;
        int texVbo;
        int shaderProgram;
        int ebo;
        int textureID;

        int Width, Height;
        public Renderer(int width, int height) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            this.Width = width;
            this.Height = height;

            CenterWindow(new Vector2i(width, height));
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            //Gen VAO
            vao = GL.GenVertexArray();
            //Bind VAO
            GL.BindVertexArray(vao);

            //Gen VBO
            vbo = GL.GenBuffer();
            //Bind VBO
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            //Put the vertex VBO in slot 0 of our VAO
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexArrayAttrib(vao, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            //Texture VBO
            texVbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, texVbo);
            GL.BufferData(BufferTarget.ArrayBuffer, textCoords.Length * sizeof(float), textCoords, BufferUsageHint.StaticDraw);

            //Put the texture VBO in slot 1 of our VAO
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexArrayAttrib(vao, 1);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);


            GL.BindVertexArray(0); //Unbind VAO

            ebo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            shaderProgram = GL.CreateProgram();

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, LoadShaderSource("Default.vert"));
            GL.CompileShader(vertexShader);
            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, LoadShaderSource("Default.frag"));
            GL.CompileShader(fragmentShader);

            GL.AttachShader(shaderProgram, vertexShader);
            GL.AttachShader(shaderProgram, fragmentShader);

            GL.LinkProgram(shaderProgram);

            //Delete shaders
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            //TEXTURES
            textureID = GL.GenTexture();
            //activate the texture in the unit
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, textureID);

            //Texture Parameters
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            //Load image
            StbImage.stbi_set_flip_vertically_on_load(1);
            ImageResult bookshelfTexture = ImageResult.FromStream(File.OpenRead("./Textures/bookshelf.png"), ColorComponents.RedGreenBlueAlpha);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bookshelfTexture.Width, bookshelfTexture.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, bookshelfTexture.Data);
            //Unbind Texture
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
            this.Width = e.Width;
            this.Height = e.Height;
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.ClearColor(0.4f, 0.6f, 0.8f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            //Draw triangle
            GL.UseProgram(shaderProgram);
            GL.BindTexture(TextureTarget.Texture2D, textureID);
            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 4);

            Context.SwapBuffers();
            base.OnRenderFrame(args);
        }

        protected override void OnUnload()
        {
            base.OnUnload();

            GL.DeleteVertexArray(vao);
            GL.DeleteBuffer(vbo);
            GL.DeleteBuffer(ebo);
            GL.DeleteTexture(textureID);
            GL.DeleteProgram(shaderProgram);
        }

        public static string LoadShaderSource(string filePath)
        {
            string shaderSource = "";

            try
            {
                using(StreamReader reader = new StreamReader("./Shaders/" + filePath))
                {
                    shaderSource = reader.ReadToEnd();
                }
            }
            catch
            {
                //Nothing RN
            }

            return shaderSource;
        }
    }
}
