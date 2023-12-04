using SharpVE.Interfaces;
using SharpVE.Worlds.Chunks;
using OpenTK.Mathematics;
using SharpVE.Blocks;

namespace SharpVE.WorldSpace.Chunk
{
    public class SubChunk : IChunkData
    {
        private ILayerData[] Layers;
        public ChunkColumn Chunk { get; }
        public sbyte YLevel { get; }

        public readonly Dictionary<ushort, BlockState> BlockStates;

        public SubChunk(ChunkColumn chunk, sbyte yLevel)
        {
            Layers = new ILayerData[ChunkColumn.SIZE];
            BlockStates = new Dictionary<ushort, BlockState>() {
                {
                    0,
                    chunk.ParentWorld.BlockRegistry.DefaultBlock.GetBlockState()
                }
            };
            Chunk = chunk;
            YLevel = yLevel;

            for (int i = 0; i < Layers.Length; i++)
            {
                Layers[i] = new SingleBlockChunkLayer(this, (byte)i, 0);
            }
        }

        public BlockState? GetBlock(Vector3i localPosition)
        {
            if (localPosition.Y >= ChunkColumn.SIZE || localPosition.Y < 0)
            {
                return null;
            }

            var layer = Layers[localPosition.Y];
            return layer.GetBlock(new Vector2i(localPosition.X, localPosition.Z));
        }

        public void SetBlock(Vector3i localPosition, BlockState block)
        {
            if (localPosition.Y >= ChunkColumn.SIZE || localPosition.Y < 0)
            {
                return;
            }

            var layer = Layers[localPosition.Y];
            if(layer is SingleBlockChunkLayer)
            {
                var newLayer = new ChunkLayer(this, (byte)localPosition.Y);
                newLayer.SetBlock(new Vector2i(localPosition.X, localPosition.Z), block);
                Layers[localPosition.Y] = newLayer;
            }
            else
            {
                var chunkLayer = layer as ChunkLayer;
                chunkLayer?.SetBlock(new Vector2i(localPosition.X, localPosition.Z), block);
                if (chunkLayer != null && chunkLayer.Data.All(x => x == chunkLayer?.Data[0]))
                {
                    Layers[localPosition.Y] = new SingleBlockChunkLayer(this, (byte)localPosition.Y, chunkLayer.Data[0]);
                }
            }

            //I have no idea how to remove unused blockstates...
        }

        public Vector3i GetGlobalPosition()
        {
            return new Vector3i(Chunk.Position.X * ChunkColumn.SIZE, YLevel * ChunkColumn.SIZE, Chunk.Position.Y * ChunkColumn.SIZE);
        }
    }
}