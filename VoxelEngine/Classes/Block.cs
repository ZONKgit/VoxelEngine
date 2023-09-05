using System;


namespace VoxelEngine.Classes
{
    public class Block
    {
        public int id = 0;
        public float colorR = 0;
        public float colorG = 0;
        public float colorB = 0;
        public float colorA = 0;

        public Block(int id, float colorR, float colorG, float colorB, float colorA)
        {
            this.id = id;
            this.colorR = colorR;
            this.colorG = colorG;
            this.colorB = colorB;
            this.colorA = colorA;
        }
    }
}
