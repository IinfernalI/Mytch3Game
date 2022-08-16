using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using DG.Tweening;
using GameStateMashine.StatesGame;
using UnityEngine;

namespace InGameStateMashine.InGameState
{
    public class FallsAnimation : IInGameState
    {
        public event Action<Type> IsDone;
        private StartState _startState;

        public FallsAnimation(StartState startState, Action<Type> onDaneState)
        {
            _startState = startState;
            IsDone = onDaneState;
        }

        public void Init()
        {
            var datalist = _startState.Model.Elements;
            var xHor = _startState.Model.Weight;
            List<ElementData> elMoved = new();
            List<AnimateData> animation = new();
            for (var y = 0; y < xHor; y++)
            {
                var line = datalist
                    .Where(e => e.xPos == y && e.State is ElementState.Newbie or ElementState.Fail)
                    .OrderBy(i=>i.xPos);
                foreach (ElementData el in line)
                {
                    AnimateData data = new AnimateData
                    {
                        ID = el.ID,
                        FromY = el.yPos,
                        ToY = el.FallDownPos,
                        X = el.xPos,
                        type = el.type
                    };
                    animation.Add(data);
                    elMoved.Add(el);
                }
                
            }
            Debug.Log($"{String.Join('\n',animation.Select(i=>$"{i.ID} x:{i.X} y:{i.FromY}->{i.ToY} "))}");
            float duration = _startState.View.FallAnimate(animation);
            DOVirtual.DelayedCall(duration <= 0 ? 1 : duration, () =>
            {
                elMoved.ForEach(i =>
                {
                    i.State = ElementState.Idle;
                    i.yPos = i.FallDownPos;
                    i.FallDownPos = -1;
                });
                _startState.Model.SetData(elMoved);
                
                IsDone?.Invoke(GetType());
            });
        }

        public void Exit()
        {
        }
    }
}