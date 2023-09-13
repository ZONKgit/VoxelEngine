using System;
using OpenTK;
using VoxelEngine.Classes.Renderer;
using VoxelEngine.Classes.Loaders;
using VoxelEngine.Classes.Generators;

namespace VoxelEngine.Classes
{
    public class Chunk
    {
        public Vector3 chunkSize = new Vector3(16,256,16);
        public Vector3 position;
        Blocks blocks = new Blocks();
        TextureLoader textureLoader = new TextureLoader();
        public ChunkRenderer render;
        public Block [,,] data;
        public bool isDebugDraw = false;
        PerlinNoise noise = new PerlinNoise(3253);

        public Chunk()
        {
            render = new ChunkRenderer(this);
            data = new Block[(int)chunkSize.X, (int)chunkSize.Y, (int)chunkSize.Z];
            GenerateChunk();
            UpdateChunk();
        }

        void GenerateChunk()
        {
            for (int x = 0; x < (int)chunkSize.X; x++)
            {
                for (int y = 0; y < (int)chunkSize.Y; y++)
                {
                    for (int z = 0; z < (int)chunkSize.Z; z++)
                    {
                        Block block = blocks.air;
                        int worldX = (int)position.X * (int)chunkSize.X + x;
                        int worldZ = (int)position.Z * (int)chunkSize.Z + z;
                        int noiseValue = (int)noise.GetNoise(worldX,worldZ)*12;
                        

                        if (y >= 58 && y < 63)
                        {
                            block = blocks.grass;
                        }
                        if (y < 58)
                        {
                            block = blocks.stone;
                        }
                        if (y > 63)
                        {
                            block = blocks.air;
                        }

                        if ((int)(noise.GetNoise(worldX, worldZ) * 6)+63 == y)
                        {
                            block = blocks.grass;
                        }
                        data[x, y, z] = block;
                    }
                }
            }
        }

        public Block getBlockInPosition(Vector3 pos)
        {
            if (pos.X >= 0 && pos.X < data.GetLength(0) &&
                pos.Y >= 0 && pos.Y < data.GetLength(1) &&
                pos.Z >= 0 && pos.Z < data.GetLength(2))
            {
                return data[(int)pos.X, (int)pos.Y, (int)pos.Z];
            }
            return null;
        }

        void UpdateChunk()
        {
            render.texture = textureLoader.LoadTexture("Assets/Textures/World/Atlas.png");
            render.GenerateMesh();
            
        }

        public void Update()
        {
            render.RenderMesh();
        }
    }
}
