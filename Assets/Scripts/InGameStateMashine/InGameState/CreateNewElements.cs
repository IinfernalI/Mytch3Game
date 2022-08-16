using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using DG.Tweening;
using Enum;
using GameStateMashine.StatesGame;
using Services;

namespace InGameStateMashine.InGameState
{
    public class DestroyDeadElements : IInGameState
    {
        private StartState _startState;

        public DestroyDeadElements(StartState startState, Action<Type> callback)
        {
            _startState = startState;
            IsDone = callback;
        }

        public event Action<Type> IsDone;
        public void Init()
        {
            float duration = _startState.View
                .AnimateDestroyElementFor(_startState.Model.Elements.Where(i => i.State == ElementState.IsDead).ToList());
            
            DOVirtual.DelayedCall(duration <= 0 ? 1 : duration, () =>{ IsDone?.Invoke(GetType());});
        }

        public void Exit()
        {
           
        }
    }
    public class CreateNewElements : IInGameState
    {
        private StartState _startState;
        private Random _random = new ();
        private int _enumCount;
        private GameFactory _gameFactory;
        public event Action<Type> IsDone;


        public CreateNewElements(StartState startState, GameFactory gameFactory, Action<Type> callback)
        {
            _gameFactory = gameFactory;
            _enumCount = System.Enum.GetNames(typeof(ElementType)).Length;
            _startState = startState;
            IsDone = callback;
        }

        public void Init()
        {
            CheckForNewElements(_startState.Model.Elements);
            IsDone?.Invoke(GetType());
        }

        private void CheckForNewElements(List<ElementData> elements)
        {
            elements.ForEach(i=>
                {
                    i.FallDownPos = -1;
                    i.State = i.State == ElementState.Fail ? ElementState.Idle : i.State;
                });

            foreach (ElementData el in elements)
            {
                if (el.State == ElementState.IsDead)
                {
                    el.type = (ElementType)_random.Next(0, _enumCount);
                    IEnumerable<ElementData> needFalls = elements.Where(e => e.xPos == el.xPos && e.yPos < el.yPos);
                    foreach (var downfall in needFalls)
                    {
                        downfall.State =downfall.State == ElementState.Idle ? ElementState.Fail : downfall.State;
                        downfall.FallDownPos = downfall.FallDownPos < 0 ? downfall.yPos + 1 : downfall.FallDownPos + 1;
                    }
                    el.yPos = elements.Where(e => e.xPos == el.xPos).Min(m=>m.yPos) -1;
                    el.State = ElementState.Newbie;
                    el.FallDownPos = 0;
                }
            }
        }

        public void Exit()
        {
        
        }

        public void TestUpdateStateElements(List<ElementData> elements)
        {
            CheckForNewElements(elements);
        }
    }
}