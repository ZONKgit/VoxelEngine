

namespace VoxelEngine.Classes
{
    class Blocks
    {
        public Block air = new Block("air", 0, 0, 0, 0, 16,16,16,16,16,16, true);
        public Block grass = new Block("grass", 0, 1, 0, 1,       4,1,1,1,1,2, false);
        public Block stone = new Block("stone", 0.3f, 0.3f, 0.3f, 1, 3,3,3,3,3,3, false);
    }
}