using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace VoxelEngine.Classes.Renderer
{
    public class ChunkRenderer
    {
        public Chunk chunk;
        public Block[,,] renderData; // Блоки которые надо рендерить


        public bool isDebugDraw; // Рисовать ли сетку геометрии, настраиваеться из Chunk.cs
        public bool isVertexCulling = true; // Вкл/Выкл VertexCulling

        public int texture; // Текстурный алтлас. Задаеться из Chunk.cs и меняеться в зависимости от того включен ли DebugDraw в Chunk.cs

        // Вызываетсья при создании экземпляра
        public ChunkRenderer(Chunk chunk)
        {
            this.chunk = chunk;
        }

        // Номер текстуры в текстурные координаты
        public Vector4 idTextureToUV(int idTexture = 16)
        {
            int atlasSize = 256; // Размер атласа
            int textureSize = 16; // Размер одной текстуры
            int texturesPerRow = atlasSize / textureSize; // Количество текстур в одной строке атласа

            int atlasX = idTexture % texturesPerRow; // Координата X в атласе
            int atlasY = idTexture / texturesPerRow; // Координата Y в атласе

            float uMin = (float)atlasX * (float)textureSize / (float)atlasSize;
            float uMax = (float)atlasY * (float)textureSize / (float)atlasSize;
            float vMin = (float)(atlasX+1) * (float)textureSize / (float)atlasSize;
            float vMax = (float)(atlasY+1) * (float)textureSize / (float)atlasSize;


            return new Vector4(uMin, uMax, vMin, vMax); // 0,0,0.0625f,0.0625f 
        }


        public void renderChunk()
        {
            for (int x = 0; x < chunk.chunkSize.X; x++)
            {
                for (int y = 0; y < chunk.chunkSize.Y; y++)
                {
                    for (int z = 0; z < chunk.chunkSize.Z; z++)
                    {
                        if (renderData[x, y, z].blockName != "air")
                        {
                            renderBlock(x, y, z, renderData[x, y, z]);
                        }
                    }
                }
            }
        }

        Block checkBlockInChunk(Vector3 pos)
        {
            if (pos.X >= 0 && pos.X < renderData.GetLength(0) &&
                pos.Y >= 0 && pos.Y < renderData.GetLength(1) &&
                pos.Z >= 0 && pos.Z < renderData.GetLength(2))
            {
                return renderData[(int)pos.X, (int)pos.Y, (int)pos.Z];
            }
            return null;
        }

        void leftSide(int textureID, Vector3 scale, Vector3 position)
        {
            GL.TexCoord2(idTextureToUV(textureID).Z, idTextureToUV(textureID).W); GL.Vertex3(-scale.X - position.X * 2, scale.Y - position.Y * 2, scale.Z - position.Z * 2);
            GL.TexCoord2(idTextureToUV(textureID).X, idTextureToUV(textureID).W); GL.Vertex3(-scale.X - position.X * 2, scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
            GL.TexCoord2(idTextureToUV(textureID).X, idTextureToUV(textureID).Y); GL.Vertex3(-scale.X - position.X * 2, -scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
            GL.TexCoord2(idTextureToUV(textureID).Z, idTextureToUV(textureID).Y); GL.Vertex3(-scale.X - position.X * 2, -scale.Y - position.Y * 2, scale.Z - position.Z * 2);
        }
        void rightSide(int textureID, Vector3 scale, Vector3 position)
        {
            GL.TexCoord2(idTextureToUV(textureID).Z, idTextureToUV(textureID).W); GL.Vertex3(scale.X - position.X * 2, scale.Y - position.Y * 2, scale.Z - position.Z * 2);
            GL.TexCoord2(idTextureToUV(textureID).X, idTextureToUV(textureID).W); GL.Vertex3(scale.X - position.X * 2, scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
            GL.TexCoord2(idTextureToUV(textureID).X, idTextureToUV(textureID).Y); GL.Vertex3(scale.X - position.X * 2, -scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
            GL.TexCoord2(idTextureToUV(textureID).Z, idTextureToUV(textureID).Y); GL.Vertex3(scale.X - position.X * 2, -scale.Y - position.Y * 2, scale.Z - position.Z * 2);
        }
            void bottomSide(int textureID, Vector3 scale, Vector3 position)
        {
            GL.TexCoord2(idTextureToUV(textureID).X, idTextureToUV(textureID).W); GL.Vertex3(scale.X - position.X * 2, -scale.Y - position.Y * 2, scale.Z - position.Z * 2);
            GL.TexCoord2(idTextureToUV(textureID).X, idTextureToUV(textureID).Y); GL.Vertex3(scale.X - position.X * 2, -scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
            GL.TexCoord2(idTextureToUV(textureID).Z, idTextureToUV(textureID).Y); GL.Vertex3(-scale.X - position.X * 2, -scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
            GL.TexCoord2(idTextureToUV(textureID).Z, idTextureToUV(textureID).W); GL.Vertex3(-scale.X - position.X * 2, -scale.Y - position.Y * 2, scale.Z - position.Z * 2);
        }
        void upSide(int textureID, Vector3 scale, Vector3 position)
        {
            GL.TexCoord2(idTextureToUV(textureID).X, idTextureToUV(textureID).W); GL.Vertex3(scale.X - position.X * 2, scale.Y - position.Y * 2, scale.Z - position.Z * 2);
            GL.TexCoord2(idTextureToUV(textureID).X, idTextureToUV(textureID).Y); GL.Vertex3(scale.X - position.X * 2, scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
            GL.TexCoord2(idTextureToUV(textureID).Z, idTextureToUV(textureID).Y); GL.Vertex3(-scale.X - position.X * 2, scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
            GL.TexCoord2(idTextureToUV(textureID).Z, idTextureToUV(textureID).W); GL.Vertex3(-scale.X - position.X * 2, scale.Y - position.Y * 2, scale.Z - position.Z * 2);
        }
        void frontSide(int textureID, Vector3 scale, Vector3 position)
        {
            GL.TexCoord2(idTextureToUV(textureID).X, idTextureToUV(textureID).W); GL.Vertex3(scale.X - position.X * 2, scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
            GL.TexCoord2(idTextureToUV(textureID).X, idTextureToUV(textureID).Y); GL.Vertex3(scale.X - position.X * 2, -scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
            GL.TexCoord2(idTextureToUV(textureID).Z, idTextureToUV(textureID).Y); GL.Vertex3(-scale.X - position.X * 2, -scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
            GL.TexCoord2(idTextureToUV(textureID).Z, idTextureToUV(textureID).W); GL.Vertex3(-scale.X - position.X * 2, scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
        }
        void backSide(int textureID, Vector3 scale, Vector3 position)
        {
            GL.TexCoord2(idTextureToUV(textureID).X, idTextureToUV(textureID).W); GL.Vertex3(scale.X - position.X * 2, scale.Y - position.Y * 2, scale.Z - position.Z * 2);
            GL.TexCoord2(idTextureToUV(textureID).X, idTextureToUV(textureID).Y); GL.Vertex3(scale.X - position.X * 2, -scale.Y - position.Y * 2, scale.Z - position.Z * 2);
            GL.TexCoord2(idTextureToUV(textureID).Z, idTextureToUV(textureID).Y); GL.Vertex3(-scale.X - position.X * 2, -scale.Y - position.Y * 2, scale.Z - position.Z * 2);
            GL.TexCoord2(idTextureToUV(textureID).Z, idTextureToUV(textureID).W); GL.Vertex3(-scale.X - position.X * 2, scale.Y - position.Y * 2, scale.Z - position.Z * 2);
        }

        // Рендеринк блока из чанка
        void renderBlock(int x, int y, int z, Block block)
        {
            Vector3 scale = new Vector3(1, 1, 1);
            Vector3 position = new Vector3(x, y, z);
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texture);
            if (isDebugDraw) GL.Color4(block.colorR, block.colorG, block.colorB, 1.0f);


            GL.Begin(BeginMode.Quads);
            Vector4 Side1UV = new Vector4(0, 0, 1, 1);

            GL.Color3(1.0f, 1.0f, 1.0f);
            if (isVertexCulling)//USE VERTEX CULLING
            {
                Block checkBlock = new Block("test block", 0, 0, 0, 0, 0);

                //Left
                if (checkBlockInChunk(position + new Vector3(1, 0, 0)) != null)
                {
                    if (checkBlockInChunk(position + new Vector3(1, 0, 0)).isTransparent)
                    {
                        leftSide(block.leftTextureID, scale, position);
                    }
                }

                else leftSide(block.leftTextureID, scale, position);





                // Right
                if (checkBlockInChunk(position + new Vector3(-1, 0, 0)) != null)
                {
                    if (checkBlockInChunk(position + new Vector3(-1, 0, 0)).isTransparent)
                    {
                        rightSide(block.rightTextureID, scale, position);
                    }
                }
                else rightSide(block.rightTextureID, scale, position);


                // Bottom
                if (checkBlockInChunk(position + new Vector3(0, -1, 0)) != null)
                {
                    if (checkBlockInChunk(position + new Vector3(0, -1, 0)).isTransparent)
                    {
                        bottomSide(block.bottomTextureID, scale, position);
                    }
                }
                else bottomSide(block.bottomTextureID, scale, position);


                // Up
                if (checkBlockInChunk(position + new Vector3(0, -1, 0)) != null)
                {
                    if (checkBlockInChunk(position + new Vector3(0, -1, 0)).isTransparent)
                    {
                        upSide(block.upTextureID, scale, position);
                    }
                }
                else upSide(block.upTextureID, scale, position);




                // Front
                if (checkBlockInChunk(position + new Vector3(0, 0, 1)) != null)
                {
                    if (checkBlockInChunk(position + new Vector3(0, 0, 1)).isTransparent)
                    {
                        frontSide(block.frontTextureID, scale, position);
                    }
                }
                else frontSide(block.frontTextureID, scale, position);



                // Back
                if (checkBlockInChunk(position + new Vector3(0, 0, -1)) != null)
                {
                    if (checkBlockInChunk(position + new Vector3(0, 0, -1)).isTransparent)
                    {
                        backSide(block.backTextureID, scale, position);
                    }
                }
                else backSide(block.backTextureID, scale, position);


                //NO VERTEX CULLING

            }

            else
            {
                GL.TexCoord2(Side1UV.X, Side1UV.W); GL.Vertex3(-scale.X - position.X * 2, scale.Y - position.Y * 2, scale.Z - position.Z * 2);
                GL.TexCoord2(Side1UV.X, Side1UV.Y); GL.Vertex3(-scale.X - position.X * 2, scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                GL.TexCoord2(Side1UV.Z, Side1UV.Y); GL.Vertex3(-scale.X - position.X * 2, -scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                GL.TexCoord2(Side1UV.Z, Side1UV.W); GL.Vertex3(-scale.X - position.X * 2, -scale.Y - position.Y * 2, scale.Z - position.Z * 2);

                GL.TexCoord2(Side1UV.X, Side1UV.W); GL.Vertex3(scale.X - position.X * 2, scale.Y - position.Y * 2, scale.Z - position.Z * 2);
                GL.TexCoord2(Side1UV.X, Side1UV.Y); GL.Vertex3(scale.X - position.X * 2, scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                GL.TexCoord2(Side1UV.Z, Side1UV.Y); GL.Vertex3(scale.X - position.X * 2, -scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                GL.TexCoord2(Side1UV.Z, Side1UV.W); GL.Vertex3(scale.X - position.X * 2, -scale.Y - position.Y * 2, scale.Z - position.Z * 2);

                GL.TexCoord2(Side1UV.X, Side1UV.W); GL.Vertex3(scale.X - position.X * 2, -scale.Y - position.Y * 2, scale.Z - position.Z * 2);
                GL.TexCoord2(Side1UV.X, Side1UV.Y); GL.Vertex3(scale.X - position.X * 2, -scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                GL.TexCoord2(Side1UV.Z, Side1UV.Y); GL.Vertex3(-scale.X - position.X * 2, -scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                GL.TexCoord2(Side1UV.Z, Side1UV.W); GL.Vertex3(-scale.X - position.X * 2, -scale.Y - position.Y * 2, 1.0 - position.Z * 2);

                GL.TexCoord2(Side1UV.X, Side1UV.W); GL.Vertex3(scale.X - position.X * 2, scale.Y - position.Y * 2, 1.0 - position.Z * 2);
                GL.TexCoord2(Side1UV.X, Side1UV.Y); GL.Vertex3(scale.X - position.X * 2, scale.Y - position.Y * 2, -1.0 - position.Z * 2);
                GL.TexCoord2(Side1UV.Z, Side1UV.Y); GL.Vertex3(-scale.X - position.X * 2, scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                GL.TexCoord2(Side1UV.Z, Side1UV.W); GL.Vertex3(-scale.X - position.X * 2, scale.Y - position.Y * 2, scale.Z - position.Z * 2);

                GL.TexCoord2(Side1UV.X, Side1UV.W); GL.Vertex3(scale.X - position.X * 2, scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                GL.TexCoord2(Side1UV.X, Side1UV.Y); GL.Vertex3(scale.X - position.X * 2, -scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                GL.TexCoord2(Side1UV.Z, Side1UV.Y); GL.Vertex3(-scale.X - position.X * 2, -scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                GL.TexCoord2(Side1UV.Z, Side1UV.W); GL.Vertex3(-scale.X - position.X * 2, scale.Y - position.Y * 2, -scale.Z - position.Z * 2);

                GL.TexCoord2(Side1UV.X, Side1UV.W); GL.Vertex3(scale.X - position.X * 2, scale.Y - position.Y * 2, scale.Z - position.Z * 2);
                GL.TexCoord2(Side1UV.X, Side1UV.Y); GL.Vertex3(scale.X - position.X * 2, -scale.Y - position.Y * 2, scale.Z - position.Z * 2);
                GL.TexCoord2(Side1UV.Z, Side1UV.Y); GL.Vertex3(-scale.X - position.X * 2, -scale.Y - position.Y * 2, scale.Z - position.Z * 2);
                GL.TexCoord2(Side1UV.Z, Side1UV.W); GL.Vertex3(-scale.X - position.X * 2, scale.Y - position.Y * 2, scale.Z - position.Z * 2);
            }


            GL.End();
        }
    }
}