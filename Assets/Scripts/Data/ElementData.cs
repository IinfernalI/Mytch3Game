using Enum;

namespace Data
{
    public enum ElementState
    {
        Newbie,
        Idle,
        Fail,
        IsDead
    }
    public class ElementData
    {
        public int yPos{ get; set; }
        
        public int xPos{ get; set; }

        public ElementType type { get; set; }

        private int _id;

        public ElementData(ElementType type, int id)
        {
            ID = id;
            this.type = type;
        }
    
        public int ID
        {
            get => _id;
            private set => _id = value;
        }

        public ElementState State { get; set; }

        public int FallDownPos { get; set; } = -1;
        //public bool isDead { get; set; }
    }
}