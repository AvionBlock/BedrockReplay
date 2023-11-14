using BedrockReplay.Blocks;
using BedrockReplay.Graphics;
using BedrockReplay.Interfaces;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace BedrockReplay.Worlds.Chunks
{
    public class SingleBlockChunkData : IChunkData
    {
        private short _Block;
        private Chunk _Chunk;
        private byte YIndex;

        #region Render Pipeline
        VAO? Vao;
        VBO? Vbo;
        VBO? TexVbo;
        VBO? BrightnessVbo;
        IBO? Ibo;
        Texture? ImgTexture;
        #endregion

        #region Chunk Mesh
        FaceData MeshFaceData;
        List<uint> Indices = new List<uint>();
        uint IndexCount = 0;
        #endregion

        public SingleBlockChunkData(Chunk chunkData, byte yIndex)
        {
            _Chunk = chunkData;
            YIndex = yIndex;
            _Block = (short)_Chunk._World.Registry.GetIndexForBlockId(BlockType.BookShelf);
            MeshFaceData = new FaceData()
            {
                Vertices = new List<Vector3>(),
                UV = new List<Vector2>()
            };

            GenerateMesh();
        }

        public Block GetBlock(int localX, int localY, int localZ)
        {
            if (localX >= Chunk.Width || localY >= Chunk.Width || localZ >= Chunk.Width)
            {
                throw new Exception($"The requested block at {localX}, {localY}, {localZ} is outside of the subchunk data!");
            }
            return _Chunk._World.Registry.Blocks[_Block];
        }

        public void GenerateMesh()
        {
            int faceCount = 0;
            for (int x = 0; x < Chunk.Width; x++)
            {
                for (int y = 0; y < Chunk.Width; y++)
                {
                    for (int z = 0; z < Chunk.Width; z++)
                    {
                        var block = GetBlock(x, y, z);
                        if (block.Type == BlockType.Air) continue; //Skip air blocks

                        if (x == Chunk.Width - 1)
                        {
                            CreatePlane(block, Faces.RIGHT, new Vector3(x, y + YIndex * Chunk.Width, z));
                            faceCount++;
                        }

                        if (x == 0)
                        {
                            CreatePlane(block, Faces.LEFT, new Vector3(x, y + YIndex * Chunk.Width, z));
                            faceCount++;
                        }

                        if (y == Chunk.Width - 1)
                        {
                            CreatePlane(block, Faces.TOP, new Vector3(x, y + YIndex * Chunk.Width, z));
                            faceCount++;
                        }

                        if (y == 0)
                        {
                            CreatePlane(block, Faces.BOTTOM, new Vector3(x, y + YIndex * Chunk.Width, z));
                            faceCount++;
                        }

                        if (z == Chunk.Width - 1)
                        {
                            CreatePlane(block, Faces.FRONT, new Vector3(x, y + YIndex * Chunk.Width, z));
                            faceCount++;
                        }

                        if (z == 0)
                        {
                            CreatePlane(block, Faces.BACK, new Vector3(x, y + YIndex * Chunk.Width, z));
                            faceCount++;
                        }
                    }
                }
            }

            AddFaceIndices(faceCount);
            Build();
        }

        private void CreatePlane(Block block, Faces face, Vector3 position)
        {
            var Face = block.AddFace(face);
            position.X += _Chunk.Position.X * Chunk.Width;
            position.Z += _Chunk.Position.Y * Chunk.Width;
            foreach (var vertice in Face.Vertices)
            {
                MeshFaceData.Vertices.Add(vertice + position);
            }
            MeshFaceData.UV.AddRange(Face.UV);
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

        private void Build()
        {
            //Build VAO
            Vao = new VAO();

            //Build VBO vertex and bind it.
            Vbo = new VBO(MeshFaceData.Vertices);
            //Build VBO uv/texture and bind it.
            TexVbo = new VBO(MeshFaceData.UV);

            Vao.LinkToVao(0, 3, Vbo);
            Vao.LinkToVao(1, 2, TexVbo);

            Vao.Unbind();

            Ibo = new IBO(Indices);
        }

        public void Draw(ShaderProgram shader)
        {
            shader.Bind();
            Vao?.Bind();
            Ibo?.Bind();
            ImgTexture?.Bind();
            GL.DrawElements(PrimitiveType.Triangles, Indices.Count, DrawElementsType.UnsignedInt, 0);

            Vao?.Unbind();
            Ibo?.Unbind();
        }
    }
}
