using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class BootState : IState
    {
        
        StartSceneView _sceneView;
        public event Action<IState> OnExiT;
        public event Action<IState> OnMoved;
        
        
        
        //reload merge git

        public void StartState()
        {
            var r = SceneManager.LoadSceneAsync("StartScene");
             r.completed += LoadSceneCompleted;
            
        }

        private void LoadSceneCompleted(AsyncOperation obj)
        {
            obj.completed -= LoadSceneCompleted;
            _sceneView = UnityEngine.GameObject.FindObjectOfType<StartSceneView>();
            _sceneView.OnClickStart += LoadGameScene;
            
        }

        public void LoadGameScene()
        {
            var r = SceneManager.LoadSceneAsync("GameScene");
            r.completed += LoadGameSceneCompleted;

            void LoadGameSceneCompleted(AsyncOperation obj)
            {
                obj.completed -= LoadGameSceneCompleted;
                OnExiT?.Invoke(this);
            }
        }

       

        public IState Exit()
        {
            return this;
        }
    }
}