using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using VoxelEngine.Classes.Loaders;


namespace VoxelEngine.Classes.Renderer
{
    public class ChunkRenderer
    {
        public int chunkSize = 16;
        public Block[,,] renderData;


        public bool debugDraw = true;

        public int texture;
        

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
            if (debugDraw)
            {
                GL.Enable(EnableCap.Texture2D);
                GL.BindTexture(TextureTarget.Texture2D, texture);
                GL.Color4(1.0f, 1.0f, 1.0f, 1.0f);
            }
            else GL.Color3(block.colorR, block.colorG, block.colorB);

            GL.Begin(BeginMode.Quads);

            Vector4 Side1UV = new Vector4(0, 0, 1, 1);

            GL.TexCoord2(Side1UV.X, Side1UV.W); GL.Vertex3(-scale.X + position.X * 2, scale.Y + position.Y * 2, scale.Z + position.Z * 2);
            GL.TexCoord2(Side1UV.X, Side1UV.Y); GL.Vertex3(-scale.X + position.X * 2, scale.Y + position.Y * 2, -scale.Z + position.Z * 2);
            GL.TexCoord2(Side1UV.Z, Side1UV.Y); GL.Vertex3(-scale.X + position.X * 2, -scale.Y + position.Y * 2, -scale.Z + position.Z * 2);
            GL.TexCoord2(Side1UV.Z, Side1UV.W); GL.Vertex3(-scale.X + position.X * 2, -scale.Y + position.Y * 2, scale.Z + position.Z * 2);



            GL.TexCoord2(Side1UV.X, Side1UV.W); GL.Vertex3(scale.X + position.X * 2, scale.Y + position.Y * 2, scale.Z + position.Z * 2);
            GL.TexCoord2(Side1UV.X, Side1UV.Y); GL.Vertex3(scale.X + position.X * 2, scale.Y + position.Y * 2, -scale.Z + position.Z * 2);
            GL.TexCoord2(Side1UV.Z, Side1UV.Y); GL.Vertex3(scale.X + position.X * 2, -scale.Y + position.Y * 2, -scale.Z + position.Z * 2);
            GL.TexCoord2(Side1UV.Z, Side1UV.W); GL.Vertex3(scale.X + position.X * 2, -scale.Y + position.Y * 2, scale.Z + position.Z * 2);


            GL.TexCoord2(Side1UV.X, Side1UV.W); GL.Vertex3(scale.X + position.X * 2, -scale.Y + position.Y * 2, scale.Z + position.Z * 2);
            GL.TexCoord2(Side1UV.X, Side1UV.Y); GL.Vertex3(scale.X + position.X * 2, -scale.Y + position.Y * 2, -scale.Z + position.Z * 2);
            GL.TexCoord2(Side1UV.Z, Side1UV.Y); GL.Vertex3(-scale.X + position.X * 2, -scale.Y + position.Y * 2, -scale.Z + position.Z * 2);
            GL.TexCoord2(Side1UV.Z, Side1UV.W); GL.Vertex3(-scale.X + position.X * 2, -scale.Y + position.Y * 2, 1.0 + position.Z * 2);



            GL.TexCoord2(Side1UV.X, Side1UV.W); GL.Vertex3(scale.X + position.X * 2, scale.Y + position.Y * 2, 1.0 + position.Z * 2);
            GL.TexCoord2(Side1UV.X, Side1UV.Y); GL.Vertex3(scale.X + position.X * 2, scale.Y + position.Y * 2, -1.0 + position.Z * 2);
            GL.TexCoord2(Side1UV.Z, Side1UV.Y); GL.Vertex3(-scale.X + position.X * 2, scale.Y + position.Y * 2, -scale.Z + position.Z * 2);
            GL.TexCoord2(Side1UV.Z, Side1UV.W); GL.Vertex3(-scale.X + position.X * 2, scale.Y + position.Y * 2, scale.Z + position.Z * 2);



            GL.TexCoord2(Side1UV.X, Side1UV.W); GL.Vertex3(scale.X + position.X * 2, scale.Y + position.Y * 2, -scale.Z + position.Z * 2);
            GL.TexCoord2(Side1UV.X, Side1UV.Y); GL.Vertex3(scale.X + position.X * 2, -scale.Y + position.Y * 2, -scale.Z + position.Z * 2);
            GL.TexCoord2(Side1UV.Z, Side1UV.Y); GL.Vertex3(-scale.X + position.X * 2, -scale.Y + position.Y * 2, -scale.Z + position.Z * 2);
            GL.TexCoord2(Side1UV.Z, Side1UV.W); GL.Vertex3(-scale.X + position.X * 2, scale.Y + position.Y * 2, -scale.Z + position.Z * 2);





            GL.TexCoord2(Side1UV.X, Side1UV.W); GL.Vertex3(scale.X + position.X * 2, scale.Y + position.Y * 2, scale.Z + position.Z * 2);
            GL.TexCoord2(Side1UV.X, Side1UV.Y); GL.Vertex3(scale.X + position.X * 2, -scale.Y + position.Y * 2, scale.Z + position.Z * 2);
            GL.TexCoord2(Side1UV.Z, Side1UV.Y); GL.Vertex3(-scale.X + position.X * 2, -scale.Y + position.Y * 2, scale.Z + position.Z * 2);
            GL.TexCoord2(Side1UV.Z, Side1UV.W); GL.Vertex3(-scale.X + position.X * 2, scale.Y + position.Y * 2, scale.Z + position.Z * 2);

            GL.End();
        }
    }
}
