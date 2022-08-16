using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using GameStateMashine.StatesGame;

namespace InGameStateMashine.InGameState
{
    public class CheckElements : IInGameState
    {
        private StartState _startState;

        public event Action<Type> IsDone;

        public CheckElements(StartState startState, Action<Type> callback)
        {
            _startState = startState;
            IsDone = callback;
        }

        public void Init()
        {
            if (_startState.Controller.TryFoundMutches(out List<List<ElementData>> list))
            {
                _startState.Model.ScoreAdd(list.Select(i=>i.Count).Sum() * 100);
                _startState.Controller.SetDeadElements(list.SelectMany(i=> i.Select(e=>e)).ToList());
            }
            IsDone?.Invoke(GetType());
        }

        public void Exit()
        {
        }
    }
}