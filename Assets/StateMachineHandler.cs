using System;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class StateMachineHandler : MonoBehaviour
    {
        private GameStateMachine _SM;
        public StartSceneView view;

        private void Start()
        {
            _SM = new GameStateMachine();
            _SM.Init(new BootState(this));
        }

        public StartSceneView GetView()
        {
            return view;
        }
        
    }
}