using System;
using OpenTK;
using VoxelEngine.Classes.Renderer;
using VoxelEngine.Classes.Loaders;

namespace VoxelEngine.Classes
{
    public class Chunk
    {
        public int chunkSize = 16;
        public Vector3 position;
        Blocks blocks = new Blocks();
        TextureLoader texLoader = new TextureLoader();
        public ChunkRenderer render = new ChunkRenderer();
        public Block [,,] data;
        public bool debugDraw = true;

        public Chunk()
        {
            data = new Block[chunkSize, chunkSize, chunkSize];
            GenerateChunk();
            UpdateChunk();
        }

        void GenerateChunk()
        {
            for (int x = 0; x < chunkSize; x++)
            {
                for (int y = 0; y < chunkSize; y++)
                {
                    for (int z = 0; z < chunkSize; z++)
                    {
                        Block block;
                        float worldX = position.X * chunkSize + x;
                        float worldZ = position.Z * chunkSize + z;

                        if (y == 1)
                        {
                            block = blocks.grass;
                            Console.WriteLine("X:{0} Y:{1} Z:{2}", x, y, z);
                        }
                        else
                        {
                            block = blocks.air;
                        }
                        data[x, y, z] = block;
                    }
                }
            }
        }

        void UpdateChunk()
        {
            render.renderData = data;

            render.debugDraw = debugDraw;
            if (debugDraw)
            {
                render.texture = texLoader.LoadTexture("Assets/Textures/Debug/Grid.png");
            }
        }

        public void Update()
        {
            render.renderChunk();
        }
    }
}
