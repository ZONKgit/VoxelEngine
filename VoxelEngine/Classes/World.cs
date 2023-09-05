using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using VoxelEngine.Classes;

namespace VoxelEngine.Classes
{
    public class World
    {
        public Player player = new Player();
        public Chunk chunk = new Chunk();


        public void Update()
        {
            player.Update();
            chunk.Update();
            
            //drawTestCube();
        }


        public void drawTestCube()
        {
            Vector3 scale = new Vector3(1, 1, 1);
            Vector3 position = new Vector3(0, 0, 0);
            // Отрисовка тестового обьекта
            GL.Begin(BeginMode.Quads);

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
