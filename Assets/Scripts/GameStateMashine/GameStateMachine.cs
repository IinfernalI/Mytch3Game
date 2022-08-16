using System;
using System.Collections.Generic;
using System.Linq;
using Controller;
using Data;
using GameStateMashine.StatesGame;
using Services;

namespace GameStateMashine
{
    public class GameStateMachine
    {
        private IStateGame _currentState;
        private readonly Dictionary<Type, IStateGame> _states;
        private GameStuffHolder _gameStuffHolder;

         public GameStateMachine(GameStuffHolder gameStuffHolder, GameFactory gameFactory)
        {
            _gameStuffHolder = gameStuffHolder;

            _states = new Dictionary<Type, IStateGame>()
            {
                //заливка дикшинари с типом и обьектом
                [typeof(BootState)] = new BootState(this, gameFactory),
                [typeof(StartState)] = new StartState(gameFactory),
           
            };
        }
    
        public void SetState<T>() where T : IStateGame
        {
            if (_currentState is T)
            {
                return;
            }
            _currentState?.Exit();

            _currentState = _states[typeof(T)];
            _currentState.Init();
        }
    
        public void SetFirstState()
        {
            SetState<BootState>();
        }

    }
}