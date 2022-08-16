using System.Collections.Generic;

namespace View
{
    public class SimplePoolObject<T>
    {
        private Stack<T> stack = new Stack<T>();
    
        public virtual void Push(T obj)
        {
            stack.Push(obj);
        }
    
        public virtual bool TryGet(out T obj) => 
            stack.TryPop(out obj);
    }
}