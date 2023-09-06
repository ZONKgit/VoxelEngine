using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using VoxelEngine.Classes.Loaders;


namespace VoxelEngine.Classes.Renderer
{
    public class ChunkRenderer
    {
        public Chunk chunk;
        public Block[,,] renderData;


        public bool isDebugDraw;
        public bool isVertexCulling = true;

        public int texture;
        
        public ChunkRenderer(Chunk chunk)
        {
            this.chunk = chunk;
        }

        public void renderChunk()
        {
            for (int x = 0; x < chunk.chunkSize.X; x++)
            {
                for (int y = 0; y < chunk.chunkSize.Y; y++)
                {
                    for (int z = 0; z < chunk.chunkSize.Z; z++)
                    {
                        if (renderData[x, y, z].id != 0)
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


        // Рендеринк блока из чанка
        void renderBlock(int x, int y, int z, Block block)
        {
            Vector3 scale = new Vector3(1, 1, 1);
            Vector3 position = new Vector3(x, y, z);
            if (isDebugDraw)
            {
                GL.Enable(EnableCap.Texture2D);
                GL.BindTexture(TextureTarget.Texture2D, texture);
                GL.Color4(block.colorR, block.colorG, block.colorB, 1.0f);
            }
            
            GL.Begin(BeginMode.Quads);
            Vector4 Side1UV = new Vector4(0, 0, 1, 1);

            if (!isDebugDraw)
            {
                GL.Color3(block.colorR, block.colorG, block.colorB);
            }

            if (isVertexCulling)//USE VERTEX CULLING
            {
                Block checkBlock = new Block(0, 0, 0, 0, 0);

                //Left
                if (checkBlockInChunk(position + new Vector3(1, 0, 0)) != null)
                {
                    if (checkBlockInChunk(position + new Vector3(1, 0, 0)).id == 0)
                    {
                        GL.TexCoord2(Side1UV.X, Side1UV.W);GL.Vertex3(-scale.X - position.X * 2, scale.Y - position.Y * 2, scale.Z - position.Z * 2);
                        GL.TexCoord2(Side1UV.X, Side1UV.Y); GL.Vertex3(-scale.X - position.X * 2, scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                        GL.TexCoord2(Side1UV.Z, Side1UV.Y); GL.Vertex3(-scale.X - position.X * 2, -scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                        GL.TexCoord2(Side1UV.Z, Side1UV.W); GL.Vertex3(-scale.X - position.X * 2, -scale.Y - position.Y * 2, scale.Z - position.Z * 2);
                    }
                }
            
                else
                {
                    GL.TexCoord2(Side1UV.X, Side1UV.W); GL.Vertex3(-scale.X - position.X * 2, scale.Y - position.Y * 2, scale.Z - position.Z * 2);
                    GL.TexCoord2(Side1UV.X, Side1UV.Y); GL.Vertex3(-scale.X - position.X * 2, scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                    GL.TexCoord2(Side1UV.Z, Side1UV.Y); GL.Vertex3(-scale.X - position.X * 2, -scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                    GL.TexCoord2(Side1UV.Z, Side1UV.W); GL.Vertex3(-scale.X - position.X * 2, -scale.Y - position.Y * 2, scale.Z - position.Z * 2);
                }




                // Right
                if (checkBlockInChunk(position + new Vector3(-1, 0, 0)) != null )
                    {
                        if (checkBlockInChunk(position + new Vector3(-1, 0, 0)).id == 0)
                        {
                            GL.TexCoord2(Side1UV.X, Side1UV.W); GL.Vertex3(scale.X - position.X * 2, scale.Y - position.Y * 2, scale.Z - position.Z * 2);
                            GL.TexCoord2(Side1UV.X, Side1UV.Y); GL.Vertex3(scale.X - position.X * 2, scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                            GL.TexCoord2(Side1UV.Z, Side1UV.Y); GL.Vertex3(scale.X - position.X * 2, -scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                            GL.TexCoord2(Side1UV.Z, Side1UV.W); GL.Vertex3(scale.X - position.X * 2, -scale.Y - position.Y * 2, scale.Z - position.Z * 2);
                        }
                    }
                else
                    {
                        GL.TexCoord2(Side1UV.X, Side1UV.W); GL.Vertex3(scale.X - position.X * 2, scale.Y - position.Y * 2, scale.Z - position.Z * 2);
                        GL.TexCoord2(Side1UV.X, Side1UV.Y); GL.Vertex3(scale.X - position.X * 2, scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                        GL.TexCoord2(Side1UV.Z, Side1UV.Y); GL.Vertex3(scale.X - position.X * 2, -scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                        GL.TexCoord2(Side1UV.Z, Side1UV.W); GL.Vertex3(scale.X - position.X * 2, -scale.Y - position.Y * 2, scale.Z - position.Z * 2);
                    }


                // Bottom
                if (checkBlockInChunk(position + new Vector3(0, 1, 0)) != null)
                {
                    if (checkBlockInChunk(position + new Vector3(0, 1, 0)).id == 0)
                    {
                        if (isDebugDraw) GL.TexCoord2(Side1UV.X, Side1UV.W); GL.Vertex3(scale.X - position.X * 2, -scale.Y - position.Y * 2, scale.Z - position.Z * 2);
                        if (isDebugDraw) GL.TexCoord2(Side1UV.X, Side1UV.Y); GL.Vertex3(scale.X - position.X * 2, -scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                        if (isDebugDraw) GL.TexCoord2(Side1UV.Z, Side1UV.Y); GL.Vertex3(-scale.X - position.X * 2, -scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                        if (isDebugDraw) GL.TexCoord2(Side1UV.Z, Side1UV.W); GL.Vertex3(-scale.X - position.X * 2, -scale.Y - position.Y * 2, 1.0 - position.Z * 2);
                    }
                }
                else
                {
                    GL.TexCoord2(Side1UV.X, Side1UV.W); GL.Vertex3(scale.X - position.X * 2, -scale.Y - position.Y * 2, scale.Z - position.Z * 2);
                    GL.TexCoord2(Side1UV.X, Side1UV.Y); GL.Vertex3(scale.X - position.X * 2, -scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                    GL.TexCoord2(Side1UV.Z, Side1UV.Y); GL.Vertex3(-scale.X - position.X * 2, -scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                    GL.TexCoord2(Side1UV.Z, Side1UV.W); GL.Vertex3(-scale.X - position.X * 2, -scale.Y - position.Y * 2, 1.0 - position.Z * 2);
                }


                // Up
                if (checkBlockInChunk(position + new Vector3(0, -1, 0)) != null)
                {
                    if (checkBlockInChunk(position + new Vector3(0, -1, 0)).id == 0)
                    {
                        GL.TexCoord2(Side1UV.X, Side1UV.W); GL.Vertex3(scale.X - position.X * 2, scale.Y - position.Y * 2, 1.0 - position.Z * 2);
                        GL.TexCoord2(Side1UV.X, Side1UV.Y); GL.Vertex3(scale.X - position.X * 2, scale.Y - position.Y * 2, -1.0 - position.Z * 2);
                        GL.TexCoord2(Side1UV.Z, Side1UV.Y); GL.Vertex3(-scale.X - position.X * 2, scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                        GL.TexCoord2(Side1UV.Z, Side1UV.W); GL.Vertex3(-scale.X - position.X * 2, scale.Y - position.Y * 2, scale.Z - position.Z * 2);
                    }
                }
                else
                {
                    GL.TexCoord2(Side1UV.X, Side1UV.W); GL.Vertex3(scale.X - position.X * 2, scale.Y - position.Y * 2, 1.0 - position.Z * 2);
                    GL.TexCoord2(Side1UV.X, Side1UV.Y); GL.Vertex3(scale.X - position.X * 2, scale.Y - position.Y * 2, -1.0 - position.Z * 2);
                    GL.TexCoord2(Side1UV.Z, Side1UV.Y); GL.Vertex3(-scale.X - position.X * 2, scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                    GL.TexCoord2(Side1UV.Z, Side1UV.W); GL.Vertex3(-scale.X - position.X * 2, scale.Y - position.Y * 2, scale.Z - position.Z * 2);
                }



                // Front
                if (checkBlockInChunk(position + new Vector3(0, 0, 1)) != null)
                {
                    if (checkBlockInChunk(position + new Vector3(0, 0, 1)).id == 0)
                    {

                        if (isDebugDraw) GL.TexCoord2(Side1UV.X, Side1UV.W); GL.Vertex3(scale.X - position.X * 2, scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                        if (isDebugDraw) GL.TexCoord2(Side1UV.X, Side1UV.Y); GL.Vertex3(scale.X - position.X * 2, -scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                        if (isDebugDraw) GL.TexCoord2(Side1UV.Z, Side1UV.Y); GL.Vertex3(-scale.X - position.X * 2, -scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                        if (isDebugDraw) GL.TexCoord2(Side1UV.Z, Side1UV.W); GL.Vertex3(-scale.X - position.X * 2, scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                    }
                }
                else
                {
                    if (isDebugDraw) GL.TexCoord2(Side1UV.X, Side1UV.W); GL.Vertex3(scale.X - position.X * 2, scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                    if (isDebugDraw) GL.TexCoord2(Side1UV.X, Side1UV.Y); GL.Vertex3(scale.X - position.X * 2, -scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                    if (isDebugDraw) GL.TexCoord2(Side1UV.Z, Side1UV.Y); GL.Vertex3(-scale.X - position.X * 2, -scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                    if (isDebugDraw) GL.TexCoord2(Side1UV.Z, Side1UV.W); GL.Vertex3(-scale.X - position.X * 2, scale.Y - position.Y * 2, -scale.Z - position.Z * 2);
                }


                // Back
                if (checkBlockInChunk(position + new Vector3(0, 0, -1)) != null)
                {
                    if (checkBlockInChunk(position + new Vector3(0, 0, -1)).id == 0)
                    {


                        if (isDebugDraw) GL.TexCoord2(Side1UV.X, Side1UV.W); GL.Vertex3(scale.X - position.X * 2, scale.Y - position.Y * 2, scale.Z - position.Z * 2);
                        if (isDebugDraw) GL.TexCoord2(Side1UV.X, Side1UV.Y); GL.Vertex3(scale.X - position.X * 2, -scale.Y - position.Y * 2, scale.Z - position.Z * 2);
                        if (isDebugDraw) GL.TexCoord2(Side1UV.Z, Side1UV.Y); GL.Vertex3(-scale.X - position.X * 2, -scale.Y - position.Y * 2, scale.Z - position.Z * 2);
                        if (isDebugDraw) GL.TexCoord2(Side1UV.Z, Side1UV.W); GL.Vertex3(-scale.X - position.X * 2, scale.Y - position.Y * 2, scale.Z - position.Z * 2);
                    }
                }
                else
                {
                    if (isDebugDraw) GL.TexCoord2(Side1UV.X, Side1UV.W); GL.Vertex3(scale.X - position.X * 2, scale.Y - position.Y * 2, scale.Z - position.Z * 2);
                    if (isDebugDraw) GL.TexCoord2(Side1UV.X, Side1UV.Y); GL.Vertex3(scale.X - position.X * 2, -scale.Y - position.Y * 2, scale.Z - position.Z * 2);
                    if (isDebugDraw) GL.TexCoord2(Side1UV.Z, Side1UV.Y); GL.Vertex3(-scale.X - position.X * 2, -scale.Y - position.Y * 2, scale.Z - position.Z * 2);
                    if (isDebugDraw) GL.TexCoord2(Side1UV.Z, Side1UV.W); GL.Vertex3(-scale.X - position.X * 2, scale.Y - position.Y * 2, scale.Z - position.Z * 2);
                }


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
