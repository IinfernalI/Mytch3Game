using System;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameState : IState
    {

        public event Action<IState> OnExiT;
        
        public void StartState()
        {
            Debug.Log($" DO ");
            DOVirtual.DelayedCall(3f, () => { OnExiT?.Invoke(this); });
        }

        public IState Exit()
        {
            return this;
        }
    }
}