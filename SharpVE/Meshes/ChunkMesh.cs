using SharpVE.Interfaces;
using SharpVE.Worlds.Chunks;
using OpenTK.Mathematics;
using SharpVE.Blocks;
using SharpVE.Models;
using SharpVE.WorldSpace;
using SharpVE.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace SharpVE.Meshes
{
    public class ChunkMesh
    {
        public IChunkData Chunk;
        public BlockRegistry Registry;

        #region Mesh
        private List<Vector3> Vertices;
        private List<Vector2> UV;
        private List<uint> Indices;
        private uint IndexCount;
        #endregion

        #region Render Pipeline
        VAO vao;
        VBO? vbo;
        VBO? textureVbo;
        IBO? ibo;
        Texture atlas;
        #endregion

        public ChunkMesh(IChunkData chunk, BlockRegistry registry, Texture textureAtlas)
        {
            Chunk = chunk;
            Registry = registry;
            Vertices = new List<Vector3>();
            UV = new List<Vector2>();
            Indices = new List<uint>();

            vao = new VAO();
            atlas = textureAtlas;
        }

        public void GenerateMesh()
        {
            ClearMesh();
            var chunkPosition = Chunk.GetGlobalPosition();
            for (int x = 0; x < ChunkColumn.SIZE; x++)
            {
                for (int y = 0; y < ChunkColumn.SIZE; y++)
                {
                    for (int z = 0; z < ChunkColumn.SIZE; z++)
                    {
                        var blockState = Chunk.GetBlock(new Vector3i(x, y, z));
                        var block = Registry.GetBlock(blockState?.Name);

                        AddBlockModel(new Vector3i(x, y, z), chunkPosition, Chunk.Chunk.ParentWorld, block);
                    }
                }
            }
        }

        public void ClearMesh()
        {
            Vertices.Clear();
            Indices.Clear();
            IndexCount = 0;
        }

        private void AddBlockModel(Vector3i bPos, Vector3i cPos, World world, Block block)
        {
            var model = block.Model?.Faces ?? Cube.Faces;
            var globalBPos = new Vector3i(bPos.X + cPos.X, bPos.Y + cPos.Y, bPos.Z + cPos.Z);
            foreach (var face in model)
            {
                var uv = block.UV[face.UsesUV];
                switch (face.CullDirection)
                {
                    case CullCheck.PosX:
                        if (bPos.X < ChunkColumn.SIZE - 1)
                            if (!Registry.GetBlock(Chunk.GetBlock(new Vector3i(bPos.X + 1, bPos.Y, bPos.Z))?.Name).IsOpaque)
                                AddFace(face, globalBPos, block.GetUVsFromCoordinate(uv));
                        else
                            if (!Registry.GetBlock(world.GetBlock(new Vector3i(globalBPos.X + 1, globalBPos.Y, globalBPos.Z))?.Name).IsOpaque)
                                AddFace(face, globalBPos, block.GetUVsFromCoordinate(uv));
                        break;
                    case CullCheck.NegX:
                        if (bPos.X > 0)
                            if (!Registry.GetBlock(Chunk.GetBlock(new Vector3i(bPos.X - 1, bPos.Y, bPos.Z))?.Name).IsOpaque)
                                AddFace(face, globalBPos, block.GetUVsFromCoordinate(uv));
                            else
                            if (!Registry.GetBlock(world.GetBlock(new Vector3i(globalBPos.X - 1, globalBPos.Y, globalBPos.Z))?.Name).IsOpaque)
                                AddFace(face, globalBPos, block.GetUVsFromCoordinate(uv));
                        break;
                    case CullCheck.PosZ:
                        if (bPos.Z < ChunkColumn.SIZE - 1)
                            if (!Registry.GetBlock(Chunk.GetBlock(new Vector3i(bPos.X, bPos.Y, bPos.Z + 1))?.Name).IsOpaque)
                                AddFace(face, globalBPos, block.GetUVsFromCoordinate(uv));
                            else
                            if (!Registry.GetBlock(world.GetBlock(new Vector3i(globalBPos.X, globalBPos.Y, globalBPos.Z + 1))?.Name).IsOpaque)
                                AddFace(face, globalBPos, block.GetUVsFromCoordinate(uv));
                        break;
                    case CullCheck.NegZ:
                        if (bPos.Z > 0)
                            if (!Registry.GetBlock(Chunk.GetBlock(new Vector3i(bPos.X, bPos.Y, bPos.Z - 1))?.Name).IsOpaque)
                                AddFace(face, globalBPos, block.GetUVsFromCoordinate(uv));
                            else
                            if (!Registry.GetBlock(world.GetBlock(new Vector3i(globalBPos.X, globalBPos.Y, globalBPos.Z - 1))?.Name).IsOpaque)
                                AddFace(face, globalBPos, block.GetUVsFromCoordinate(uv));
                        break;
                    case CullCheck.PosY:
                        if (bPos.Y < ChunkColumn.SIZE - 1)
                            if (!Registry.GetBlock(Chunk.GetBlock(new Vector3i(bPos.X, bPos.Y + 1, bPos.Z))?.Name).IsOpaque)
                                AddFace(face, globalBPos, block.GetUVsFromCoordinate(uv));
                            else
                            if (!Registry.GetBlock(world.GetBlock(new Vector3i(globalBPos.X, globalBPos.Y + 1, globalBPos.Z))?.Name).IsOpaque)
                                AddFace(face, globalBPos, block.GetUVsFromCoordinate(uv));
                        break;
                    case CullCheck.NegY:
                        if (bPos.Y > 0)
                            if (!Registry.GetBlock(Chunk.GetBlock(new Vector3i(bPos.X, bPos.Y - 1, bPos.Z))?.Name).IsOpaque)
                                AddFace(face, globalBPos, block.GetUVsFromCoordinate(uv));
                            else
                            if (!Registry.GetBlock(world.GetBlock(new Vector3i(globalBPos.X, globalBPos.Y - 1, globalBPos.Z))?.Name).IsOpaque)
                                AddFace(face, globalBPos, block.GetUVsFromCoordinate(uv));
                        break;
                    case CullCheck.None:
                        AddFace(face, globalBPos, block.GetUVsFromCoordinate(uv));
                        break;
                }
            }
        }

        private void AddFace(Face face, Vector3i globalBPos, List<Vector2> uvs)
        {
            foreach (var vertice in face.Vertices)
            {
                Vertices.Add(vertice + globalBPos);
            }
            UV.AddRange(uvs);
            AddFaceIndice();
        }

        private void AddFaceIndice()
        {
            Indices.Add(0 + IndexCount);
            Indices.Add(1 + IndexCount);
            Indices.Add(2 + IndexCount);
            Indices.Add(2 + IndexCount);
            Indices.Add(3 + IndexCount);
            Indices.Add(0 + IndexCount);

            IndexCount += 4;
        }

        public void BuildMesh()
        {
            //Build VBO vertex and bind it.
            vbo = new VBO(Vertices);
            //Build VBO uv/texture and bind it.
            textureVbo = new VBO(UV);

            vao.LinkToVao(0, 3, vbo);
            vao.LinkToVao(1, 2, textureVbo);

            vao.Unbind();

            ibo = new IBO(Indices);
        }

        public void Draw()
        {
            vao?.Bind();
            ibo?.Bind();
            GL.DrawElements(PrimitiveType.Triangles, Indices.Count, DrawElementsType.UnsignedInt, 0);

            vao?.Unbind();
            ibo?.Unbind();
        }
    }
}