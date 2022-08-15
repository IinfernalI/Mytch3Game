using System;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class StateMachineHandler : MonoBehaviour
    {
        private GameStateMachine _SM;

        private void Start()
        {
            _SM = new GameStateMachine();
            _SM.InitFirstState();
        }
    }
}