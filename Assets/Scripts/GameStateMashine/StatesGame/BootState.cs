using Services;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using View;

namespace GameStateMashine.StatesGame
{
    public class BootState : IStateGame
    {
        private GameStateMachine _gameStateMachine;
        private GameFactory _gameFactory;

        public BootState(GameStateMachine gameStateMachine, GameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
        }

        public void Init()
        {
            var curScene = SceneManager.GetActiveScene();
            SceneManager.CreateScene("TestScene");
            AsyncOperation operation = SceneManager.UnloadSceneAsync(curScene);

            operation.completed += OperationOncompleted;
        }

        public void Exit()
        {
            Debug.Log($"Exit: {this.GetType().Name} ");
        }

        private void OperationOncompleted(AsyncOperation op)
        {
            op.completed -= OperationOncompleted;
            InitScene2();
        }

        public void InitScene2()
        {
            CreateEventSystem();
            var camera = _gameFactory.CreateCamera("IT_IS_MY_SUPERPUPER_CAMERA");
            InstantiateAndInitCanvas(camera);
        }
        
        private void CreateEventSystem()
        {
            var go = new GameObject("EventSystem");
            go.AddComponent<EventSystem>();
            go.AddComponent<StandaloneInputModule>();
        }
        
        private void InstantiateAndInitCanvas(Camera camera)
        {
            ButtonStart buttonStart = _gameFactory.GetInstance<ButtonStart>();
            Canvas canvas = buttonStart.GetComponent<Canvas>();
            canvas.worldCamera = camera;
            buttonStart.OnClick += OnClickHandler;

            void OnClickHandler()
            {
                buttonStart.OnClick -= OnClickHandler;
                ButtonStartOnOnClick();
            }
        }
        
        public void ButtonStartOnOnClick()
        {
            _gameStateMachine.SetState<StartState>();
        }
    }
}