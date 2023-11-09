using BedrockReplay.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace BedrockReplay.World
{
    public class Chunk
    {
        #region Chunk Data
        private Dimension ParentDimension;
        const int SIZE = 16;
        public Vector3 Position { get; private set; }
        public Block[] Data = new Block[SIZE * SIZE * SIZE];
        #endregion

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

        public Chunk(Dimension dimension, Vector3 position)
        {
            Position = position;
            ParentDimension = dimension;

            MeshFaceData = new FaceData()
            {
                Vertices = new List<Vector3>(),
                UV = new List<Vector2>()
            };

            ImgTexture = new Texture("atlas.png");

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
                            Data[index] = new Block(new Vector3(x, y, z), BlockType.Grass);
                        }
                        else
                        {
                            Data[index] = new Block(new Vector3(x, y, z), BlockType.Air);
                        }
                        index++;
                    }
                }
            }

            GenerateChunk();
        }

        public Block? GetBlock(int localX, int localY, int localZ)
        {
            int blockId = (localX * SIZE * SIZE) + (localY * SIZE) + localZ;
            var block = Data[blockId];
            return block;
        }

        public void GenerateChunk()
        {
            int faceCount = 0;
            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    for (int z = 0; z < SIZE; z++)
                    {
                        var index = (x * SIZE * SIZE) + (y * SIZE) + z;
                        var block = Data[index];
                        if (block != null && block.Type != BlockType.Air)
                        {
                            //Front Face
                            if (z < SIZE - 1)
                            {
                                if (GetBlock(x, y, z + 1)?.Type == BlockType.Air)
                                {
                                    faceCount += IntegrateFace(block, Faces.FRONT);
                                }
                            }
                            else
                            {
                                faceCount += IntegrateFace(block, Faces.FRONT);
                            }

                            //Back Face
                            if (z > 0)
                            {
                                if (GetBlock(x, y, z - 1)?.Type == BlockType.Air)
                                {
                                    faceCount += IntegrateFace(block, Faces.BACK);
                                }
                            }
                            else
                            {
                                faceCount += IntegrateFace(block, Faces.BACK);
                            }

                            //Right Face
                            if (x < SIZE - 1)
                            {
                                if (GetBlock(x + 1, y, z)?.Type == BlockType.Air)
                                {
                                    faceCount += IntegrateFace(block, Faces.RIGHT);
                                }
                            }
                            else
                            {
                                faceCount += IntegrateFace(block, Faces.RIGHT);
                            }

                            //Left Face
                            if (x > 0)
                            {
                                if (GetBlock(x - 1, y, z)?.Type == BlockType.Air)
                                {
                                    faceCount += IntegrateFace(block, Faces.LEFT);
                                }
                            }
                            else
                            {
                                faceCount += IntegrateFace(block, Faces.LEFT);
                            }

                            //Top Face
                            if (y < SIZE - 1)
                            {
                                if (GetBlock(x, y + 1, z)?.Type == BlockType.Air)
                                {
                                    faceCount += IntegrateFace(block, Faces.TOP);
                                }
                            }
                            else
                            {
                                faceCount += IntegrateFace(block, Faces.TOP);
                            }

                            //Bottom Face
                            if (y > 0)
                            {
                                if (GetBlock(x, y - 1, z)?.Type == BlockType.Air)
                                {
                                    faceCount += IntegrateFace(block, Faces.BOTTOM);
                                }
                            }
                            else
                            {
                                faceCount += IntegrateFace(block, Faces.BOTTOM);
                            }
                        }
                    }
                }
            }

            AddFaceIndices(faceCount);
            BuildChunk();
        }

        public int IntegrateFace(Block block, Faces face)
        {
            MeshFaceData.Vertices.AddRange(block.AddFace(face).Vertices);
            MeshFaceData.UV.AddRange(block.AddFace(face).UV);
            return 1;
        }

        public void AddFaceIndices(int facesAmount)
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

        /// <summary>
        /// Take data and process it for rendering
        /// </summary>
        public void BuildChunk()
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

        /// <summary>
        /// Deletes the chunk pipeline
        /// </summary>
        public void Delete()
        {
            Vao?.Delete();
            Vbo?.Delete();
            TexVbo?.Delete();
            Ibo?.Delete();
            ImgTexture?.Delete();
        }
    }
}
