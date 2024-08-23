using BedrockReplay.Structures;
using Microsoft.Extensions.ObjectPool;
using SharpVE.Chunks;
using System.Collections.Concurrent;

namespace BedrockReplay.Managers
{
    public static class ChunkMesher<T> where T : class
    {
        private static Thread MesherThread = new Thread(MeshLogic);
        private static ConcurrentQueue<SubChunk<T>> QueuedMeshes = new ConcurrentQueue<SubChunk<T>>();
        private static ObjectPool<List<Vertex>> VerticesPool = ObjectPool.Create<List<Vertex>>();
        private static ObjectPool<List<uint>> IndicesPool = ObjectPool.Create<List<uint>>();

        private delegate void MeshBuilt(SubChunk<T> subChunk, Vertex[] vertices, uint[] indices);

        private static event MeshBuilt? OnMeshBuilt;


        public static async Task<ChunkMeshResult> QueueMesh(SubChunk<T> chunk)
        {
            OnMeshBuilt += MeshBuilt;
            QueuedMeshes.Enqueue(chunk);
            if (!MesherThread.IsAlive)
                MesherThread.Start();

            ChunkMeshResult? result = null;
            while (result == null)
            {
                await Task.Delay(1); //1ms delay so the CPU doesn't burn.
            }

            return (ChunkMeshResult)result;

            void MeshBuilt(SubChunk<T> builtChunk, Vertex[] vertices, uint[] indices)
            {
                if (builtChunk != chunk) return;
                result = new ChunkMeshResult() { Vertices = vertices, Indices = indices };
            }
        }

        public static void MeshLogic()
        {
            while(QueuedMeshes.Any())
            {
                QueuedMeshes.TryDequeue(out var subChunk);
                var vertices = VerticesPool.Get();
                var indices = IndicesPool.Get();
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
