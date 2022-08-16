using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using InGameStateMashine;
using InGameStateMashine.InGameState;
using Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameStateMashine.StatesGame
{
    public class StartState : IStateGame
    {
        public Model Model { get; private set; }

        public Controller.Controller Controller { get; private set; }

        public View.View View{ get; private set; }

        private IInGameState _curState;
        private InGameStateMachine _inGameStateMachine;

        private GameFactory _gameFactory;

        private int? _score;
        private List<ElementData> _elements = null;

        public StartState( GameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            
            _inGameStateMachine = new InGameStateMachine(this, _gameFactory);
            _inGameStateMachine.PlayStateDone += PlayStateDoneHandler;
        }

        private void PlayStateDoneHandler()
        {
            Debug.Log($"DONE");
        }

        public void Init()
        {
            Debug.Log($"init state: {this.GetType().Name} ");
            LoadScene();
        }

        public void Exit()
        {
        }

        private void LoadScene()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
            operation.completed += OperationOncompleted;
        }

        private void OperationOncompleted(AsyncOperation asyncOperation)
        {
            asyncOperation.completed -= OperationOncompleted;
            InitScene();
        }

        private void InitScene()
        {
            Model = new Model();
            if (_score.HasValue && _elements != null )
            {
                Controller = new Controller.Controller(Model,_score.Value, _elements);
                _score = null;
                _elements = null;
            }
            else
            {
                Controller = new Controller.Controller(Model);
            }
            
            Controller.OnLoad += OnLoadHandler;
            var view = _gameFactory.GetInstance<View.View>();
            View = view;
            View.Init(Model, _gameFactory);

            _inGameStateMachine.SetFirstState();
        }

        private void OnLoadHandler(int score, List<ElementData> elements)
        {
            Controller.OnLoad -= OnLoadHandler;
            _elements = elements;
            _score = score;
            LoadScene();
        }
    }
}