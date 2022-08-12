using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class BootState : IState
    {
        StateMachineHandler _handler;
        StartSceneView _sceneView;

        public BootState(StateMachineHandler stateMachineHandler)
        {
            _handler = stateMachineHandler;
        }

        public void Start()
        {
            SceneManager.LoadScene("StartScene");
            _sceneView = _handler.view;
            _sceneView.OnClickStart += LoadGameScene;
        }

        public void LoadGameScene()
        {
            SceneManager.LoadScene("GameScene");
            Exit();
        }
        
        public IState Exit()
        {
            return this;
        }
    }
}