using SharpVE.Interfaces;
using SharpVE.Worlds.Chunks;
using OpenTK.Mathematics;
using SharpVE.Blocks;
using SharpVE.Models;
using SharpVE.WorldSpace;

namespace SharpVE.Meshes
{
    public class ChunkMesh
    {
        public IChunkData Chunk;
        public BlockRegistry Registry;

        private List<Vector3> Vertices;
        private List<uint> Indices;
        private uint IndexCount;

        public ChunkMesh(IChunkData chunk, BlockRegistry registry)
        {
            Chunk = chunk;
            Registry = registry;
            Vertices = new List<Vector3>();
            Indices = new List<uint>();
        }

        public void BuildMesh()
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
                        var model = block.Model?.Faces ?? Cube.Faces;

                        AddBlockModel(new Vector3i(x, y, z), chunkPosition, Chunk.Chunk.ParentWorld, model);
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

        private void AddBlockModel(Vector3i bPos, Vector3i cPos, World world, List<Face> model)
        {
            var globalBPos = new Vector3i(bPos.X + cPos.X, bPos.Y + cPos.Y, bPos.Z + cPos.Z);
            foreach (var face in model)
            {
                switch (face.CullDirection)
                {
                    case CullCheck.PosX:
                        if (bPos.X < ChunkColumn.SIZE - 1)
                            if (!Registry.GetBlock(Chunk.GetBlock(new Vector3i(bPos.X + 1, bPos.Y, bPos.Z))?.Name).IsOpaque)
                                AddFace(face, globalBPos);
                        else
                            if (!Registry.GetBlock(world.GetBlock(new Vector3i(globalBPos.X + 1, globalBPos.Y, globalBPos.Z))?.Name).IsOpaque)
                                AddFace(face, globalBPos);
                        break;
                    case CullCheck.NegX:
                        if (bPos.X > 0)
                            if (!Registry.GetBlock(Chunk.GetBlock(new Vector3i(bPos.X - 1, bPos.Y, bPos.Z))?.Name).IsOpaque)
                                AddFace(face, globalBPos);
                            else
                            if (!Registry.GetBlock(world.GetBlock(new Vector3i(globalBPos.X - 1, globalBPos.Y, globalBPos.Z))?.Name).IsOpaque)
                                AddFace(face, globalBPos);
                        break;
                    case CullCheck.PosZ:
                        if (bPos.Z < ChunkColumn.SIZE - 1)
                            if (!Registry.GetBlock(Chunk.GetBlock(new Vector3i(bPos.X, bPos.Y, bPos.Z + 1))?.Name).IsOpaque)
                                AddFace(face, globalBPos);
                            else
                            if (!Registry.GetBlock(world.GetBlock(new Vector3i(globalBPos.X, globalBPos.Y, globalBPos.Z + 1))?.Name).IsOpaque)
                                AddFace(face, globalBPos);
                        break;
                    case CullCheck.NegZ:
                        if (bPos.Z > 0)
                            if (!Registry.GetBlock(Chunk.GetBlock(new Vector3i(bPos.X, bPos.Y, bPos.Z - 1))?.Name).IsOpaque)
                                AddFace(face, globalBPos);
                            else
                            if (!Registry.GetBlock(world.GetBlock(new Vector3i(globalBPos.X, globalBPos.Y, globalBPos.Z - 1))?.Name).IsOpaque)
                                AddFace(face, globalBPos);
                        break;
                    case CullCheck.PosY:
                        if (bPos.Y < ChunkColumn.SIZE - 1)
                            if (!Registry.GetBlock(Chunk.GetBlock(new Vector3i(bPos.X, bPos.Y + 1, bPos.Z))?.Name).IsOpaque)
                                AddFace(face, globalBPos);
                            else
                            if (!Registry.GetBlock(world.GetBlock(new Vector3i(globalBPos.X, globalBPos.Y + 1, globalBPos.Z))?.Name).IsOpaque)
                                AddFace(face, globalBPos);
                        break;
                    case CullCheck.NegY:
                        if (bPos.Y > 0)
                            if (!Registry.GetBlock(Chunk.GetBlock(new Vector3i(bPos.X, bPos.Y - 1, bPos.Z))?.Name).IsOpaque)
                                AddFace(face, globalBPos);
                            else
                            if (!Registry.GetBlock(world.GetBlock(new Vector3i(globalBPos.X, globalBPos.Y - 1, globalBPos.Z))?.Name).IsOpaque)
                                AddFace(face, globalBPos);
                        break;
                }
            }
        }

        private void AddFace(Face face, Vector3i globalBPos)
        {
            foreach (var vertice in face.Vertices)
            {
                Vertices.Add(vertice + globalBPos);
            }
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
    }
}