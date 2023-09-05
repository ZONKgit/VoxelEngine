using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Windows.Forms;



namespace VoxelEngine.Classes
{
    public class Camera
    {
        Player player;
        // Window vars
        public GameWindow window;
        public float Aspect;

        // Position vars
        public Vector3 position;
        public Vector3 rotation;

        public Camera(Player player)
        {
            this.player = player;
        }

        public void Rotate(float xAngle, float yAngle)
        {
            rotation.Y += yAngle;
            if (rotation.Y < 0) rotation.Z += 360;
            if (rotation.Y > 360) rotation.Z -= 360;
            rotation.X -= xAngle;
            if (rotation.X < 90) rotation.X = 90;
            if (rotation.X > 260) rotation.X = 260;
        }

        private void InputController()
        {
            if (!window.Focused) return;
            Cursor.Hide();

            float ugol = rotation.Y / 180 * (float)Math.PI;

            int basePosX = 1920 / 2;
            int basePosY = 1080 / 2;
            int mousePosX = Mouse.GetCursorState().X;
            int mousePosY = Mouse.GetCursorState().Y;
            Rotate((basePosY - mousePosY) / 25, (basePosX - mousePosX) / 25);
            Mouse.SetPosition(basePosX, basePosY);

        }

        public Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView((float)Math.PI * (45 / 180f), Aspect, 1.0f, 10000.0f);
        }

        public void Update()
        {
            InputController();
               

            GL.Rotate(rotation.X, 1, 0, 0);
            GL.Rotate(rotation.Y, 0, 1, 0);
            GL.Translate(player.position+position);
        }

    }
}
