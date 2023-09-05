using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace VoxelEngine.Classes.Renderer
{
    public class ChunkRenderer
    {
        public int chunkSize = 16;
        public Block[,,] renderData;

        public void renderChunk()
        {
            for (int x = 0; x < chunkSize; x++)
            {
                for (int y = 0; y < chunkSize; y++)
                {
                    for (int z = 0; z < chunkSize; z++)
                    {
                        if (renderData[x, y, z].id != 0)
                        {
                            renderBlock(x, y, z, renderData[x, y, z]);
                        }
                    }
                }
            }
        }

        // Рендеринк блока из чанка
        void renderBlock(int x, int y, int z, Block block)
        {
            Vector3 scale = new Vector3(1, 1, 1);
            Vector3 position = new Vector3(x, y, z);
            // Отрисовка тестового обьекта
            GL.Begin(BeginMode.Quads);

            GL.Color3(block.colorR, block.colorG, block.colorB);
            GL.Vertex3(-scale.X + position.X * 2, scale.Y + position.Y * 2, scale.Z + position.Z * 2);
            GL.Vertex3(-scale.X + position.X * 2, scale.Y + position.Y * 2, -scale.Z + position.Z * 2);
            GL.Vertex3(-scale.X + position.X * 2, -scale.Y + position.Y * 2, -scale.Z + position.Z * 2);
            GL.Vertex3(-scale.X + position.X * 2, -scale.Y + position.Y * 2, scale.Z + position.Z * 2);



            GL.Vertex3(scale.X + position.X * 2, scale.Y + position.Y * 2, scale.Z + position.Z * 2);
            GL.Vertex3(scale.X + position.X * 2, scale.Y + position.Y * 2, -scale.Z + position.Z * 2);
            GL.Vertex3(scale.X + position.X * 2, -scale.Y + position.Y * 2, -scale.Z + position.Z * 2);
            GL.Vertex3(scale.X + position.X * 2, -scale.Y + position.Y * 2, scale.Z + position.Z * 2);


            GL.Vertex3(scale.X + position.X * 2, -scale.Y + position.Y * 2, scale.Z + position.Z * 2);
            GL.Vertex3(scale.X + position.X * 2, -scale.Y + position.Y * 2, -scale.Z + position.Z * 2);
            GL.Vertex3(-scale.X + position.X * 2, -scale.Y + position.Y * 2, -scale.Z + position.Z * 2);
            GL.Vertex3(-scale.X + position.X * 2, -scale.Y + position.Y * 2, 1.0 + position.Z * 2);



            GL.Vertex3(scale.X + position.X * 2, scale.Y + position.Y * 2, 1.0 + position.Z * 2);
            GL.Vertex3(scale.X + position.X * 2, scale.Y + position.Y * 2, -1.0 + position.Z * 2);
            GL.Vertex3(-scale.X + position.X * 2, scale.Y + position.Y * 2, -scale.Z + position.Z * 2);
            GL.Vertex3(-scale.X + position.X * 2, scale.Y + position.Y * 2, scale.Z + position.Z * 2);



            GL.Vertex3(scale.X + position.X * 2, scale.Y + position.Y * 2, -scale.Z + position.Z * 2);
            GL.Vertex3(scale.X + position.X * 2, -scale.Y + position.Y * 2, -scale.Z + position.Z * 2);
            GL.Vertex3(-scale.X + position.X * 2, -scale.Y + position.Y * 2, -scale.Z + position.Z * 2);
            GL.Vertex3(-scale.X + position.X * 2, scale.Y + position.Y * 2, -scale.Z + position.Z * 2);





            GL.Vertex3(scale.X + position.X * 2, scale.Y + position.Y * 2, scale.Z + position.Z * 2);
            GL.Vertex3(scale.X + position.X * 2, -scale.Y + position.Y * 2, scale.Z + position.Z * 2);
            GL.Vertex3(-scale.X + position.X * 2, -scale.Y + position.Y * 2, scale.Z + position.Z * 2);
            GL.Vertex3(-scale.X + position.X * 2, scale.Y + position.Y * 2, scale.Z + position.Z * 2);

            GL.End();
        }
    }
}
