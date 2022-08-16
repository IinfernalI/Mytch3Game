using Controller;
using UnityEngine;

namespace View
{
    public class GameBoot : MonoBehaviour
    {
        private GameStuffHolder _game;
        private void Start()
        {
            _game = new GameStuffHolder();
            _game.GameStateMachine.SetFirstState();
        }
    
    }
    
}