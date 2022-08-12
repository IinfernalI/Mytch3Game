using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameStateMachine
    {
        public IState CurrentState { get; set; }

        public void Init(IState startState)
        {
            CurrentState = startState;
            CurrentState.Start();
        }

        public void ChangeState(IState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Start();
        }
    }
}