using Enum;

namespace InGameStateMashine.InGameState
{
    public class AnimateData
    {
        public int ID { get; set; }
        public int FromY { get; set; }
        public int ToY { get; set; }
        public int X { get; set; }
        public ElementType type { get; set; }
    }
}