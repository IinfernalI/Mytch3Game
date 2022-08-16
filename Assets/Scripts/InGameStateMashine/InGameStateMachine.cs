using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using DG.Tweening;
using GameStateMashine.StatesGame;
using InGameStateMashine.InGameState;
using Services;
using UnityEngine;

namespace InGameStateMashine
{
    public class InGameStateMachine
    {
        private IInGameState _currentState;
        private readonly Dictionary<Type, IInGameState> _states;
        private StartState _startState;

        public event Action PlayStateDone;
        private Dictionary<Type, Action> doneActionDictionary = new Dictionary<Type, Action>();
        private GameFactory _gameFactory;


        public InGameStateMachine(StartState StartState, GameFactory gameFactory)
        {
            _startState = StartState;
            _gameFactory = gameFactory;

            var checkElements = new CheckElements(_startState, OnDaneState);
            var createNewElements = new CreateNewElements(_startState, _gameFactory, OnDaneState);
            var destroyDeadElements = new DestroyDeadElements(_startState, OnDaneState);
            var playerTurn = new PlayerTurn(_startState, OnDaneState);
            var falls = new FallsAnimation(_startState, OnDaneState);
            _states = new Dictionary<Type, IInGameState>()
            {
                //заливка дикшинари с типом и обьектом
                [checkElements.GetType()] = checkElements,
                [createNewElements.GetType()] = createNewElements,
                [destroyDeadElements.GetType()] = destroyDeadElements,
                [playerTurn.GetType()] = playerTurn,
                [falls.GetType()] = falls,
            };
            Action done = OnCheckElementsDone;
            doneActionDictionary.Add(typeof(CheckElements), done);
            doneActionDictionary.Add(typeof(CreateNewElements), OnCreateNewElementsDone);
            doneActionDictionary.Add(typeof(DestroyDeadElements), OnDestroyDeadElementsDone);
            doneActionDictionary.Add(typeof(PlayerTurn), OnPlayerTurnDone);
            doneActionDictionary.Add(typeof(FallsAnimation), OnFallsAnimationDone);
        }

        private void OnDaneState(Type stateType)
        {
            if (doneActionDictionary.ContainsKey(stateType) == false)
            {
                Debug.LogWarning($"cannot find action for done {stateType}");
                return;
            }

            Action action = doneActionDictionary[stateType];
            action?.Invoke();
        }

        public void SetState<T>() where T : IInGameState
        {
            if (_currentState is T)
                return;

            _currentState?.Exit();
            Debug.Log($"InGame State => {typeof(T).Name}");
            _currentState = _states[typeof(T)];
            _currentState.Init();
        }

        public void SetFirstState() =>
            DOVirtual.DelayedCall(0.2f, () => { SetState<FallsAnimation>(); });

        private void OnFallsAnimationDone() =>
            SetState<CheckElements>();


        private void OnDestroyDeadElementsDone() =>
            SetState<CreateNewElements>();

        private void OnPlayerTurnDone() =>
            SetState<CheckElements>();

        private void OnCreateNewElementsDone()
        {
            if (_startState.Model.Elements.Any(i => i.State is ElementState.Fail or ElementState.Newbie))
                SetState<FallsAnimation>();
            else
                SetState<CheckElements>();
        }

        private void OnCheckElementsDone()
        {
            if (_startState.Model.AnyDeadElements())
                SetState<DestroyDeadElements>();
            else
                SetState<PlayerTurn>();
        }
    }
}