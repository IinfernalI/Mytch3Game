using UnityEngine;

namespace View
{
    public struct VectorInt 
    {
        public VectorInt(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; }
        public int Y { get; }
    }
}