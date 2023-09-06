using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using VoxelEngine.Classes;
using VoxelEngine.Classes.Renderer;


namespace VoxelEngine
{
    public class Game
    {
        public GameWindow window;
        public World world = new World();

        FontRenderer testFont = new FontRenderer();

        public Game(GameWindow window)
        {
            this.window = window;

            // Назначение window классам
            world.player.window = this.window;
            world.player.camera.window = this.window;

            Start();
        }

        void Start()
        {
            window.Load += loaded;
            window.Resize += resize;
            window.RenderFrame += renderF;
            window.Run(1.0 / 60);
        }

        void resize(object ob, EventArgs e)
        {
            GL.Viewport(0, 0, window.Width, window.Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            // Передача значений о окне камере
            world.player.camera.Aspect = window.Height/window.Width;

            Matrix4 matrix = world.player.camera.GetProjectionMatrix();
            GL.LoadMatrix(ref matrix);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        void renderF(object o, EventArgs e)
        {
            GL.LoadIdentity();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Обновление кадра
            world.Update();
            this.testFont.drawText(new Vector3(0,0,0), new Vector4(0.0f,0.0f,0.0f,1.0f), "The quick brown fox jumps over the lazy dog!?./| 0123456789-=+");





            //UI test

            

            //End UI test

            window.SwapBuffers();

        }

        void loaded(object o, EventArgs e)
        {
            GL.ClearColor(0.3f, 0.3f, 0.3f, 0.5f);
            GL.Enable(EnableCap.DepthTest);
            //GL.Enable(EnableCap.AlphaTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            GL.Enable(EnableCap.AlphaTest);
            GL.AlphaFunc(AlphaFunction.Greater, 0.01f);
        }
    }
}
