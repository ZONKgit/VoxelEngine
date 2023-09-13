using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace VoxelEngine.Classes.Renderer
{
    public static class RenderExtensions
    {
        public static T[] AddCord<T>(this T[] array, T itemX, T itemY, T itemZ)
        {
            if (array == null)
            {
                return new T[] { itemX, itemY, itemZ };
            }
            Array.Resize(ref array, array.Length + 3);
            array[array.Length - 3] = itemX;
            array[array.Length - 2] = itemY;
            array[array.Length - 1] = itemZ;

            return array;
        }
    }

    public class ChunkRenderer
    {
        public Chunk chunk;
        int vertexVBO;
        int indexEBO;
        List<float> vertices = new List<float>();
        List<int> indexes = new List<int>();

        public byte renderMode = 0; // Рисовать ли сетку геометрии, настраиваеться из Chunk.cs
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

        public void GenerateMesh()
        {

            for (int x = 0; x < chunk.chunkSize.X; x++)
            {
                for (int y = 0; y < chunk.chunkSize.Y; y++)
                {
                    for (int z = 0; z < chunk.chunkSize.Z; z++)
                    {
                        if (chunk.data[x, y, z].blockName != "air") continue;
                        GenerateBlock(x, y, z);
                    }
                }
            }


            vertexVBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Count * sizeof(float), vertices.ToArray(), BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            indexEBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, indexEBO);
            GL.BufferData(BufferTarget.ArrayBuffer, indexes.Count * sizeof(int), indexes.ToArray(), BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void RenderMesh()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexVBO);
            GL.VertexPointer(3, VertexPointerType.Float, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.EnableClientState(ArrayCap.VertexArray);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexEBO);
            GL.Color3(50 / 255f, 168 / 255f, 82 / 255f);
            GL.DrawElements(BeginMode.Triangles, indexes.Count, DrawElementsType.UnsignedInt, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.DisableClientState(ArrayCap.VertexArray);
        }

        public void GenerateTopFace(float x, float y, float z)
        {
            // Вершины верхней стороны
            vertices.Add(x); vertices.Add(y + 1); vertices.Add(z);
            vertices.Add(x + 1); vertices.Add(y + 1); vertices.Add(z);
            vertices.Add(x); vertices.Add(y + 1); vertices.Add(z + 1);
            vertices.Add(x + 1); vertices.Add(y + 1); vertices.Add(z + 1);

            // Индексы для верхней стороны
            int lastIndex = vertices.Count / 3 - 4;
            indexes.Add(lastIndex);
            indexes.Add(lastIndex + 1);
            indexes.Add(lastIndex + 2);
            indexes.Add(lastIndex + 1);
            indexes.Add(lastIndex + 3);
            indexes.Add(lastIndex + 2);
        }

        public void GenerateBottomFace(float x, float y, float z)
        {
            // Вершины нижней стороны
            vertices.Add(x); vertices.Add(y); vertices.Add(z);
            vertices.Add(x + 1); vertices.Add(y); vertices.Add(z);
            vertices.Add(x); vertices.Add(y); vertices.Add(z + 1);
            vertices.Add(x + 1); vertices.Add(y); vertices.Add(z + 1);

            // Индексы для нижней стороны
            int lastIndex = vertices.Count / 3 - 4;
            indexes.Add(lastIndex);
            indexes.Add(lastIndex + 2);
            indexes.Add(lastIndex + 1);
            indexes.Add(lastIndex + 1);
            indexes.Add(lastIndex + 2);
            indexes.Add(lastIndex + 3);
        }

        public void GenerateFrontFace(float x, float y, float z)
        {
            // Вершины передней стороны
            vertices.Add(x); vertices.Add(y); vertices.Add(z + 1);
            vertices.Add(x + 1); vertices.Add(y); vertices.Add(z + 1);
            vertices.Add(x); vertices.Add(y + 1); vertices.Add(z + 1);
            vertices.Add(x + 1); vertices.Add(y + 1); vertices.Add(z + 1);

            // Индексы для передней стороны
            int lastIndex = vertices.Count / 3 - 4;
            indexes.Add(lastIndex);
            indexes.Add(lastIndex + 2);
            indexes.Add(lastIndex + 1);
            indexes.Add(lastIndex + 1);
            indexes.Add(lastIndex + 2);
            indexes.Add(lastIndex + 3);
        }

        public void GenerateBackFace(float x, float y, float z)
        {
            // Вершины задней стороны
            vertices.Add(x); vertices.Add(y); vertices.Add(z);
            vertices.Add(x + 1); vertices.Add(y); vertices.Add(z);
            vertices.Add(x); vertices.Add(y + 1); vertices.Add(z);
            vertices.Add(x + 1); vertices.Add(y + 1); vertices.Add(z);

            // Индексы для задней стороны
            int lastIndex = vertices.Count / 3 - 4;
            indexes.Add(lastIndex);
            indexes.Add(lastIndex + 1);
            indexes.Add(lastIndex + 2);
            indexes.Add(lastIndex + 1);
            indexes.Add(lastIndex + 3);
            indexes.Add(lastIndex + 2);
        }

        public void GenerateLeftFace(float x, float y, float z)
        {
            // Вершины левой стороны
            vertices.Add(x); vertices.Add(y); vertices.Add(z);
            vertices.Add(x); vertices.Add(y); vertices.Add(z + 1);
            vertices.Add(x); vertices.Add(y + 1); vertices.Add(z);
            vertices.Add(x); vertices.Add(y + 1); vertices.Add(z + 1);

            // Индексы для левой стороны
            int lastIndex = vertices.Count / 3 - 4;
            indexes.Add(lastIndex);
            indexes.Add(lastIndex + 1);
            indexes.Add(lastIndex + 2);
            indexes.Add(lastIndex + 1);
            indexes.Add(lastIndex + 3);
            indexes.Add(lastIndex + 2);
        }

        public void GenerateRightFace(float x, float y, float z)
        {
            // Вершины правой стороны
            vertices.Add(x + 1); vertices.Add(y); vertices.Add(z);
            vertices.Add(x + 1); vertices.Add(y); vertices.Add(z + 1);
            vertices.Add(x + 1); vertices.Add(y + 1); vertices.Add(z);
            vertices.Add(x + 1); vertices.Add(y + 1); vertices.Add(z + 1);

            // Индексы для правой стороны
            int lastIndex = vertices.Count / 3 - 4;
            indexes.Add(lastIndex);
            indexes.Add(lastIndex + 2);
            indexes.Add(lastIndex + 1);
            indexes.Add(lastIndex + 1);
            indexes.Add(lastIndex + 2);
            indexes.Add(lastIndex + 3);
        }


        public void GenerateBlock(int x, int y, int z)
        {

            GenerateTopFace(x, y, z);
            GenerateBottomFace(x, y, z);
            GenerateFrontFace(x, y, z);
            GenerateBackFace(x, y, z);
            GenerateLeftFace(x, y, z);
            GenerateRightFace(x, y, z);
        }

        
    }
}