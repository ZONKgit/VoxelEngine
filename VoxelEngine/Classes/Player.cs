using System;
using OpenTK;
using OpenTK.Input;
using System.Windows.Forms;


namespace VoxelEngine.Classes
{
    public class Player
    {
        public Camera camera;
        public GameWindow window;

        // Переменные трансформации
        public Vector3 position;
        public Vector3 rotation;

        public float SPEED = 0.5f;

        public Player()
        {
            camera = new Camera(this);
        }

        private void InputController()
        {
            if (!window.Focused) return;
            Cursor.Hide();

            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            float speed = 0;
            float moveAngle = camera.rotation.Y / 180 * (float)Math.PI;

            if (keyboardState.IsKeyDown(Key.W)) speed = SPEED;
            if (keyboardState.IsKeyDown(Key.S)) speed = -SPEED;
            if (keyboardState.IsKeyDown(Key.A))
            {
                speed = SPEED;
                moveAngle += (float)Math.PI * 0.5f;
            }
            if (keyboardState.IsKeyDown(Key.D))
            {
                speed = SPEED;
                moveAngle -= (float)Math.PI * 0.5f;
            }


            if (keyboardState.IsKeyDown(Key.Space)) position.Y += SPEED;
            if (keyboardState.IsKeyDown(Key.ShiftLeft)) position.Y -= SPEED;

            if (speed != 0)
            {
                position.X += (float)Math.Sin(moveAngle) * speed;
                position.Z -= (float)Math.Cos(moveAngle) * speed;
            }
        }


        public void Update()
        {
            InputController();
            camera.Update();
        }
    }
}
