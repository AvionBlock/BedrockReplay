using BedrockReplay.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace BedrockReplay.World
{
    public class Chunk
    {
        private List<Vector3> ChunkVerts;
        private List<Vector2> ChunkUVs;
        private List<uint> ChunkIndices;

        const int SIZE = 16;
        const int HEIGHT = 320;
        public Vector3 Position;

        private uint IndexCount;
        private Block[,,] chunkBlocks = new Block[SIZE, HEIGHT, SIZE];

        //Render Pipeline
        VAO Vao;
        VBO Vbo;
        VBO TexVbo;
        IBO Ibo;

        Texture ImgTexture;

        public Chunk(Vector3 position)
        {
            Position = position;
            ChunkVerts = new List<Vector3>();
            ChunkUVs = new List<Vector2>();
            ChunkIndices = new List<uint>();

            GenBlocks();
            GenFaces();
            BuildChunk();
        }

        /// <summary>
        /// Generate the data
        /// </summary>
        public void GenChunk()
        {
        }

        /// <summary>
        /// Generate the appropriate block faces given the data
        /// </summary>
        public void GenBlocks()
        {
            for (int x = 0; x < SIZE; x++)
            {
                for (int z = 0; z < SIZE; z++)
                {
                    for (int y = 0; y < HEIGHT; y++)
                    {
                        //var type = BlockType.Air;
                        chunkBlocks[x, y, z] = new Block(new Vector3(x, y, z), BlockType.Grass);
                    }
                }
            }
        }

        public void GenFaces()
        {
            for (int x = 0; x < SIZE; x++)
            {
                for (int z = 0; z < SIZE; z++)
                {
                    for (int y = 0; y < HEIGHT; y++)
                    {
                        int numFaces = 0;

                        if (chunkBlocks[x, y, z].Type == BlockType.Air)
                        {
                            continue;
                        }

                        //Left Faces
                        if (x > 0)
                        {
                            if (chunkBlocks[x - 1, y, z].Type == BlockType.Air)
                            {
                                numFaces += IntegrateFace(chunkBlocks[x, y, z], Faces.LEFT);
                            }
                        }
                        else
                        {
                            numFaces += IntegrateFace(chunkBlocks[x, y, z], Faces.LEFT);
                        }

                        //Right Faces
                        if (x < SIZE - 1)
                        {
                            if (chunkBlocks[x + 1, y, z].Type == BlockType.Air)
                            {
                                numFaces += IntegrateFace(chunkBlocks[x, y, z], Faces.RIGHT);
                            }
                        }
                        else
                        {
                            numFaces += IntegrateFace(chunkBlocks[x, y, z], Faces.RIGHT);
                        }

                        //Top Faces
                        if (y < HEIGHT - 1)
                        {
                            if (chunkBlocks[x, y + 1, z].Type == BlockType.Air)
                            {
                                numFaces += IntegrateFace(chunkBlocks[x, y, z], Faces.TOP);
                            }
                        }
                        else
                        {
                            numFaces += IntegrateFace(chunkBlocks[x, y, z], Faces.TOP);
                        }

                        //Bottom Faces
                        if (y > 0)
                        {
                            if (chunkBlocks[x, y - 1, z].Type == BlockType.Air)
                            {
                                numFaces += IntegrateFace(chunkBlocks[x, y, z], Faces.BOTTOM);
                            }
                        }
                        else
                        {
                            numFaces += IntegrateFace(chunkBlocks[x, y, z], Faces.BOTTOM);
                        }

                        //Front Faces
                        if (z < SIZE - 1)
                        {
                            if (chunkBlocks[x, y, z + 1].Type == BlockType.Air)
                            {
                                numFaces += IntegrateFace(chunkBlocks[x, y, z], Faces.FRONT);
                            }
                        }
                        else
                        {
                            numFaces += IntegrateFace(chunkBlocks[x, y, z], Faces.FRONT);
                        }

                        //Front Faces
                        if (z > 0)
                        {
                            if (chunkBlocks[x, y, z - 1].Type == BlockType.Air)
                            {
                                numFaces += IntegrateFace(chunkBlocks[x, y, z], Faces.BACK);
                            }
                        }
                        else
                        {
                            numFaces += IntegrateFace(chunkBlocks[x, y, z], Faces.BACK);
                        }

                        AddIndices(numFaces);
                    }
                }
            }
        }

        public int IntegrateFace(Block block, Faces face)
        {
            var faceData = block.GetFace(face);
            ChunkVerts.AddRange(faceData.Vertices);
            ChunkUVs.AddRange(faceData.UV);
            return 1;
        }

        public void AddIndices(int facesAmount)
        {
            for (int i = 0; i < facesAmount; i++)
            {
                ChunkIndices.Add(0 + IndexCount);
                ChunkIndices.Add(1 + IndexCount);
                ChunkIndices.Add(2 + IndexCount);
                ChunkIndices.Add(2 + IndexCount);
                ChunkIndices.Add(3 + IndexCount);
                ChunkIndices.Add(0 + IndexCount);

                IndexCount += 4;
            }
        }

        /// <summary>
        /// Take data and process it for rendering
        /// </summary>
        public void BuildChunk()
        {
            //Build VAO and bind it.
            Vao = new VAO();
            Vao.Bind();

            //Build VBO vertex and bind it.
            Vbo = new VBO(ChunkVerts);
            Vbo.Bind();
            Vao.LinkToVao(0, 3, Vbo);

            //Build VBO uv/texture and bind it.
            TexVbo = new VBO(ChunkUVs);
            TexVbo.Bind();
            Vao.LinkToVao(1, 2, TexVbo);

            Ibo = new IBO(ChunkIndices);

            ImgTexture = new Texture("atlas.png");
        }

        /// <summary>
        /// Draw the chunk
        /// </summary>
        /// <param name="shader"></param>
        public void Render(ShaderProgram shader)
        {
            shader.Bind();
            Vao.Bind();
            Ibo.Bind();
            ImgTexture.Bind();
            GL.DrawElements(PrimitiveType.Triangles, ChunkIndices.Count, DrawElementsType.UnsignedInt, 0);
        }

        /// <summary>
        /// Deletes the chunk pipeline
        /// </summary>
        public void Delete()
        {
            Vao.Delete();
            Vbo.Delete();
            TexVbo.Delete();
            Ibo.Delete();
            ImgTexture.Delete();
        }
    }
}
