using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using SharpVE.Chunks;
using SharpVE.Interfaces;
using SharpVE.Rendering.Graphics;

namespace SharpVE.Rendering
{
    public class Model : IDisposable
    {
        public List<Vector3> Vertices;
        //public List<Vector3> Normals;
        public List<Vector2> UV;
        public List<uint> Indices;
        uint IndexCount = 0;

        public bool Disposed { get; private set; } = false;

        #region Rendering Pipeline
        private VAO Vao;
        private VBO ModelVbo;
        private VBO TextureVbo;
        private IBO Ibo;
        #endregion

        #region Model Data
        private Matrix4 MatrixModel = Matrix4.Identity;
        public Vector3 Position { get; private set; } //Chunkspace Position
        private Vector3i ChunkPosition;
        #endregion

        public Model()
        {
            Vertices = new List<Vector3>();
            //Normals = new List<Vector3>();
            UV = new List<Vector2>();
            Indices = new List<uint>();

            Vao = new VAO();
            //Model Buffer
            ModelVbo = new VBO(Vertices);
            //Texture Buffer
            TextureVbo = new VBO(UV);

            Vao.LinkToVao(0, 3, ModelVbo);
            Vao.LinkToVao(1, 2, TextureVbo);

            Ibo = new IBO(Indices);

            Position = new Vector3();
        }

        public void Setup()
        {
            Vertices = new List<Vector3>();
            //Normals = new List<Vector3>();
            UV = new List<Vector2>();
            Indices = new List<uint>();

            Vao = new VAO();
            //Model Buffer
            ModelVbo = new VBO(Vertices);
            //Texture Buffer
            TextureVbo = new VBO(UV);

            Vao.LinkToVao(0, 3, ModelVbo);
            Vao.LinkToVao(1, 2, TextureVbo);

            Ibo = new IBO(Indices);

            Position = new Vector3();
        }

        public void Update()
        {
            ThrowIfDisposed();

            Vao.Bind();

            //Model Buffer
            ModelVbo = new VBO(Vertices);
            //Texture Buffer
            TextureVbo = new VBO(UV);

            Vao.LinkToVao(0, 3, ModelVbo);
            Vao.LinkToVao(1, 2, TextureVbo);

            Ibo = new IBO(Indices);
        }

        public void Reset()
        {
            MatrixModel = Matrix4.Identity;
        }

        public void Translate(Vector3 offset)
        {
            Position += offset;
            MatrixModel *= Matrix4.CreateTranslation(offset);
        }

        public void Draw()
        {
            ThrowIfDisposed();

            Vao.Bind();
            Ibo.Bind();
            GL.DrawElements(BeginMode.Triangles, Indices.Count, DrawElementsType.UnsignedInt, 0);
        }

        public void GenerateModel(IChunkData chunk)
        {
            Vertices.Clear();
            UV.Clear();
            Indices.Clear();

            ChunkPosition = chunk.Position;

            int faceCount = 0;
            for (int x = 0; x < ChunkColumn.Width; x++)
            {
                for (int y = 0; y < ChunkColumn.Width; y++)
                {
                    for (int z = 0; z < ChunkColumn.Width; z++)
                    {
                        var block = chunk.GetBlock(new Vector3i(x, y, z));
                        if (block.IsAir) continue;
                        for(int faceI = 0; faceI < block.Mesh.Faces.Count; faceI++)
                        {
                            var face = block.Mesh.Faces[faceI];
                            var pos = new Vector3(x,y,z);
                            pos.X += ChunkPosition.X * ChunkColumn.Width;
                            pos.Y += ChunkPosition.Y * ChunkColumn.Width;
                            pos.Z += ChunkPosition.Z * ChunkColumn.Width;

                            foreach (var vertice in face.Vertices)
                            {
                                Vertices.Add(vertice + pos);
                            }
                            UV.Add(face.UV);
                            faceCount++;
                        }
                    }
                }
            }

            AddFaceIndices(faceCount);
            Update();
        }

        private void AddFaceIndices(int facesAmount)
        {
            for (int i = 0; i < facesAmount; i++)
            {
                Indices.Add(0 + IndexCount);
                Indices.Add(1 + IndexCount);
                Indices.Add(2 + IndexCount);
                Indices.Add(2 + IndexCount);
                Indices.Add(3 + IndexCount);
                Indices.Add(0 + IndexCount);

                IndexCount += 4;
            }
        }

        public void Dispose()
        {
            if(!Disposed)
            {
                ModelVbo.Delete();
                TextureVbo.Delete();
                Vao.Delete();

                Disposed = true;
            }
        }

        private void ThrowIfDisposed()
        {
            if(Disposed) throw new ObjectDisposedException(nameof(Model));
        }
    }
}
