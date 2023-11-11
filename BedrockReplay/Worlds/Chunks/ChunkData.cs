using BedrockReplay.Blocks;
using BedrockReplay.Graphics;
using BedrockReplay.Interfaces;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace BedrockReplay.Worlds.Chunks
{
    public class ChunkData : IChunkData
    {
        public const int SIZE = 16;
        public bool IsAllAir => Data.All(x => x == _Chunk._World.Registry.GetIndexForBlockId(BlockType.Air));

        private short[] Data;
        private Chunk _Chunk;
        private int YLevel;

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

        public ChunkData(Chunk chunkData, int yIndex)
        {
            _Chunk = chunkData;
            YLevel = yIndex * SIZE;
            Data = new short[Chunk.Width * SIZE * Chunk.Width];
            ImgTexture = _Chunk._World.ImgTexture;

            MeshFaceData = new FaceData()
            {
                Vertices = new List<Vector3>(),
                UV = new List<Vector2>()
            };

            Random rand = new Random();
            var index = 0;
            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    for (int z = 0; z < SIZE; z++)
                    {
                        if (rand.Next(5) == 0)
                        {
                            Data[index] = 1;
                        }
                        else
                        {
                            Data[index] = 0;
                        }
                        index++;
                    }
                }
            }

            GenerateMesh();
        }

        //Will change later to GetBlockState(int localX, int localY, int localZ);
        public Block GetBlock(int localX, int localY, int localZ, bool throwException = false)
        {
            if (localX >= SIZE || localY >= SIZE || localZ >= SIZE)
            {
                if (throwException)
                    throw new Exception($"The requested block at {localX}, {localY}, {localZ} is outside of the subchunk data!");
                else
                    return _Chunk._World.Registry.DefaultBlock;
            }
            int idx = (localX * Chunk.Width * SIZE) + (localY * SIZE) + localZ;
            int blockId = Data[idx];
            return _Chunk._World.Registry.Blocks[blockId];
        }

        public void GenerateMesh()
        {
            int faceCount = 0;
            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    for (int z = 0; z < SIZE; z++)
                    {
                        var block = GetBlock(x, y, z);
                        if (block.Type == BlockType.Air) continue;

                        //Else
                        //Front Face
                        if (GetBlock(x, y, (byte)(z + 1))?.Type == BlockType.Air)
                        {
                            CreatePlane(block, Faces.FRONT, new Vector3(x, y + YLevel, z));
                            faceCount++;
                        }

                        //Back Face
                        if (GetBlock(x, y, (byte)(z - 1))?.Type == BlockType.Air)
                        {
                            CreatePlane(block, Faces.BACK, new Vector3(x, y + YLevel, z));
                            faceCount++;
                        }

                        //Right Face
                        if (GetBlock((byte)(x + 1), y, z)?.Type == BlockType.Air)
                        {
                            CreatePlane(block, Faces.RIGHT, new Vector3(x, y + YLevel, z));
                            faceCount++;
                        }

                        //Left Face
                        if (GetBlock((byte)(x - 1), y, z)?.Type == BlockType.Air)
                        {
                            CreatePlane(block, Faces.LEFT, new Vector3(x, y + YLevel, z));
                            faceCount++;
                        }

                        //Top Face
                        if (GetBlock(x, y + 1, z).Type == BlockType.Air)
                        {
                            CreatePlane(block, Faces.TOP, new Vector3(x, y + YLevel, z));
                            faceCount++;
                        }

                        //Bottom Face
                        if (GetBlock(x, (byte)(y - 1), z)?.Type == BlockType.Air)
                        {
                            CreatePlane(block, Faces.BOTTOM, new Vector3(x, y + YLevel, z));
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
            foreach (var vertice in Face.Vertices)
            {
                MeshFaceData.Vertices.Add(vertice + position + _Chunk.Position * Chunk.Width);
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
