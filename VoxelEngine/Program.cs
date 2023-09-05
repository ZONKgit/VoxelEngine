using System;
using OpenTK;

namespace VoxelEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            GameWindow window = new GameWindow(720, 720);
            Game gm = new Game(window);
        }
    }
}
