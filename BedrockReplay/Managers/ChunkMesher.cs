using BedrockReplay.Components;
using BedrockReplay.Structures;
using System.Buffers;
using System.Collections.Concurrent;

namespace BedrockReplay.Managers
{
    public static class ChunkMesher
    {
        private static Thread MesherThread = new Thread(MeshLogic);
        private static ConcurrentQueue<SubChunkMeshComponent> QueuedMeshes = new ConcurrentQueue<SubChunkMeshComponent>();

        public static void QueueMesh(SubChunkMeshComponent chunkMesh)
        {
            QueuedMeshes.Enqueue(chunkMesh);
            if (!MesherThread.IsAlive)
                MesherThread.Start();
        }

        public static void MeshLogic()
        {
            //At least allocate enough for 1 cube
            var vertices = ArrayPool<Vertex>.Shared.Rent(0);
            var indices = ArrayPool<uint>.Shared.Rent(0);

            var verticesLoc = 0;
            var indicesLoc = 0;

            while(QueuedMeshes.Any())
            {
                QueuedMeshes.TryDequeue(out var chunkMesh);
                chunkMesh.Mesh.Renderer.Execute(() => chunkMesh.Mesh.Update(vertices, indices));
                //Build Here!
            }
        }
    }

    public struct ChunkMeshResult
    {
        public Vertex[] Vertices;
        public uint[] Indices;
    }
}
