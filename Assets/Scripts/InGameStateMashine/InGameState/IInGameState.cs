using System;

namespace InGameStateMashine.InGameState
{
    public interface IInGameState
    {
        public event Action<Type> IsDone;
        void Init();
        void Exit();
    }
}