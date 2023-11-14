using BedrockReplay.Blocks;
using BedrockReplay.Graphics;
using BedrockReplay.Interfaces;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace BedrockReplay.Worlds.Chunks
{
    public class ChunkData : IChunkData
    {
        private short[] Data;
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

        //blockstate palette here...
        #region Palettes
        #endregion

        public ChunkData(Chunk chunkData, byte yIndex)
        {
            _Chunk = chunkData;
            YIndex = yIndex;
            Data = new short[(int)MathF.Pow(Chunk.Width, 3)];
            ImgTexture = _Chunk._World.ImgTexture;

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
            int idx = (localX * (int)MathF.Pow(Chunk.Width, 2)) + (localY * Chunk.Width) + localZ;
            int blockId = Data[idx];
            return _Chunk._World.Registry.Blocks[blockId];
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
                        if (block.Type == BlockType.Air) continue;

                        //Else
                        //Front Face
                        if (GetBlock(x, y, (byte)(z + 1))?.Type == BlockType.Air)
                        {
                            CreatePlane(block, Faces.FRONT, new Vector3(x, y + YIndex * Chunk.Width, z));
                            faceCount++;
                        }

                        //Back Face
                        if (GetBlock(x, y, (byte)(z - 1))?.Type == BlockType.Air)
                        {
                            CreatePlane(block, Faces.BACK, new Vector3(x, y + YIndex * Chunk.Width, z));
                            faceCount++;
                        }

                        //Right Face
                        if (GetBlock((byte)(x + 1), y, z)?.Type == BlockType.Air)
                        {
                            CreatePlane(block, Faces.RIGHT, new Vector3(x, y + YIndex * Chunk.Width, z));
                            faceCount++;
                        }

                        //Left Face
                        if (GetBlock((byte)(x - 1), y, z)?.Type == BlockType.Air)
                        {
                            CreatePlane(block, Faces.LEFT, new Vector3(x, y + YIndex * Chunk.Width, z));
                            faceCount++;
                        }

                        //Top Face
                        if (GetBlock(x, y + 1, z).Type == BlockType.Air)
                        {
                            CreatePlane(block, Faces.TOP, new Vector3(x, y + YIndex * Chunk.Width, z));
                            faceCount++;
                        }

                        //Bottom Face
                        if (GetBlock(x, (byte)(y - 1), z)?.Type == BlockType.Air)
                        {
                            CreatePlane(block, Faces.BOTTOM, new Vector3(x, y + YIndex * Chunk.Width, z));
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
            position.X += _Chunk.Position.X *  Chunk.Width;
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

        /// <summary>
        /// Draw the chunk
        /// </summary>
        /// <param name="shader"></param>
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
