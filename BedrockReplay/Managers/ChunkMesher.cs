using BedrockReplay.Components;
using BedrockReplay.Structures;
using Microsoft.Extensions.ObjectPool;
using System.Buffers;
using System.Collections.Concurrent;

namespace BedrockReplay.Managers
{
    public static class ChunkMesher<T> where T : class
    {
        private static Thread MesherThread = new Thread(MeshLogic);
        private static ConcurrentQueue<SubChunkMeshComponent<T>> QueuedMeshes = new ConcurrentQueue<SubChunkMeshComponent<T>>();

        public static async Task QueueMesh(SubChunkMeshComponent<T> chunkMesh)
        {
            QueuedMeshes.Enqueue(chunkMesh);
            if (!MesherThread.IsAlive)
                MesherThread.Start();
        }

        public static void MeshLogic()
        {
            var vertices = ArrayPool<Vertex>.Shared.Rent(1024);
            var indices = ArrayPool<uint>.Shared.Rent(1024);

            var verticesLoc = 0;
            var indicesLoc = 0;

            while(QueuedMeshes.Any())
            {
                QueuedMeshes.TryDequeue(out var subChunk);
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
