﻿using SharpVE.Interfaces;
using SharpVE.Worlds.Chunks;
using OpenTK.Mathematics;
using SharpVE.Blocks;

namespace SharpVE.WorldSpace.Chunk
{
    public class SubChunk : IChunkData
    {
        private ILayerData[] Layers;
        private ChunkColumn Chunk;

        public readonly byte YLevel;
        public readonly Dictionary<short, BlockState> BlockStates;

        public SubChunk(ChunkColumn chunk, byte yLevel)
        {
            Layers = new SingleBlockChunkLayer[ChunkColumn.SIZE * ChunkColumn.SIZE * ChunkColumn.SIZE];
            BlockStates = new Dictionary<short, BlockState>();
            Chunk = chunk;
            YLevel = yLevel;
        }

        public BlockState? GetBlock(Vector3i localPosition)
        {
            if (localPosition.Y >= ChunkColumn.SIZE)
            {
                return null;
            }

            foreach(var layer in Layers)
            {
                if (layer.GetYLevel() == localPosition.Y) return layer.GetBlock(new Vector2i(localPosition.X, localPosition.Z));
            }
            return null;
        }
    }
}