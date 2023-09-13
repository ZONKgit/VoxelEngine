using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using VoxelEngine.Classes.Renderer;

namespace VoxelEngine.Classes
{
    public class World
    {
        public Player player = new Player();
        public Chunk chunk = new Chunk();

        float[] vertices;// = { 0, 0, 0, 0, 1, 0, 1, 0, 0 };    // for test VBO/EBOcube
        int vertexVBO;// for test VBO/EBOcube
        int indexEBO; // for test VBO/EBOcube
        int[] indexes;// = {0,1,2};// for test VBO/EBOcube

        public World()
        {
            generateVBOTestCube();
        }

        public void Update()
        {
            player.Update();
            chunk.Update();

            //drawVBOTestCube();
            //drawTestCube();
        }

        public void generateVBOTestCube()
        {
            vertices = vertices.AddCord(0, 0, 0);
            vertices = vertices.AddCord(0, 1, 0);
            vertices = vertices.AddCord(1, 0, 0);

            indexes = indexes.AddCord(0, 1, 2);

            vertexVBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(int), vertices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            indexEBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, indexEBO);
            GL.BufferData(BufferTarget.ArrayBuffer, indexes.Length * sizeof(int), indexes, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        }
        public void drawVBOTestCube()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexVBO);
             GL.VertexPointer(3, VertexPointerType.Float, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.EnableClientState(ArrayCap.VertexArray);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexEBO);
                    GL.Color3(50 / 255f, 168 / 255f, 82 / 255f);
                    GL.DrawArrays(BeginMode.Triangles, 0, vertices.Length);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.DisableClientState(ArrayCap.VertexArray);
        }

        public void drawTestCube()
        {
            Vector3 scale = new Vector3(1, 1, 1);
            Vector3 position = new Vector3(0, 0, 0);
            // Отрисовка тестового обьекта
            GL.Begin(BeginMode.Quads);

            GL.Color3(50/255f, 168/255f, 82/255f);
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
