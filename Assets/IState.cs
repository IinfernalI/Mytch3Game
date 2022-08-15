using System;

namespace DefaultNamespace
{
    public interface IState
    {
        public void StartState();
        public IState Exit();
        public event Action<IState> OnExiT;
    }
}