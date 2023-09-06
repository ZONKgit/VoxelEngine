using System;
using OpenTK;
using VoxelEngine.Classes.Renderer;
using VoxelEngine.Classes.Loaders;

namespace VoxelEngine.Classes
{
    public class Chunk
    {
        public Vector3 chunkSize = new Vector3(16,256,16);
        public Vector3 position;
        Blocks blocks = new Blocks();
        TextureLoader texLoader = new TextureLoader();
        public ChunkRenderer render;
        public Block [,,] data;
        public bool isDebugDraw = true;

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
                        float worldX = position.X * (int)chunkSize.X + x;
                        float worldZ = position.Z * (int)chunkSize.Z + z;

                        
                        if (y >= 58 && y < 63)
                        {
                            block = blocks.grass;
                        }
                        if (y < 58)
                        {
                            block = blocks.stone;
                        }
                        //if (y == 1)
                        //{
                        //    block = blocks.grass;
                        //    Console.WriteLine("X:{0} Y:{1} Z:{2}", x, y, z);
                        //}
                        if (y > 63)
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
            // Посылание данных в чанк рендринг для рендера чанка
            render.renderData = data;
            //Debug draw
            render.isDebugDraw = isDebugDraw;
            if (isDebugDraw)
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
