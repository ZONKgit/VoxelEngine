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
        public bool isDebugDraw = true;
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
                        //if (y == 1)
                        //{
                        //    block = blocks.grass;
                        //    Console.WriteLine("X:{0} Y:{1} Z:{2}", x, y, z);
                        //}
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

        void UpdateChunk()
        {
            // Посылание данных в чанк рендринг для рендера чанка
            render.renderData = data;
            //Debug draw
            render.isDebugDraw = isDebugDraw;
            if (isDebugDraw)
            {
                render.texture = textureLoader.LoadTexture("Assets/Textures/Debug/Grid.png");
            }
            //else
            //{
            //    render.texture = textureLoader.LoadTexture("Assets/Textures/World/Atlas.png");
            //}
        }

        public void Update()
        {
            render.renderChunk();
        }
    }
}
