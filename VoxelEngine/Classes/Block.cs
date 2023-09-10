
namespace VoxelEngine.Classes
{
    public class Block
    {
        public string blockName = "Error name";

        public float colorR = 0;
        public float colorG = 0;
        public float colorB = 0;
        public float colorA = 0;

        public int upTextureID;
        public int frontTextureID;
        public int rightTextureID;
        public int backTextureID;
        public int leftTextureID;
        public int bottomTextureID;

        public bool isTransparent;

        public Block(
            string blockName = "Error name",

            float colorR = 0.0f,
            float colorG = 0.0f,
            float colorB = 0.0f,
            float colorA = 0.0f,

            int upTexID = 0,
            int frontTexID = 0,
            int rightTexID = 0,
            int backTexID = 0,
            int leftTexID = 0,
            int bottomTexID = 0,

            bool isTransparent = false
            )
        {
            this.blockName = blockName;

            this.colorR = colorR;
            this.colorG = colorG;
            this.colorB = colorB;
            this.colorA = colorA;

            upTextureID = upTexID;
            frontTextureID = frontTexID;
            rightTextureID = rightTexID;
            backTextureID = backTexID;
            leftTextureID = leftTexID;
            bottomTextureID = bottomTexID;

            this.isTransparent = isTransparent;
    }
    }
}