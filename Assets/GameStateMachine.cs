
using UnityEngine;

namespace DefaultNamespace
{
    public class GameStateMachine
    {
        public IState CurrentState { get; set; }

        public void InitFirstState()
        {
            ChangeState(new BootState());
        }

        private void CurrentStateOnOnExiT(IState state)
        {
            
            Debug.Log($"DONE {state.GetType().Name}");
            if (state is BootState)
            {
                A();
            }
            else if(state is GameState)
            {
                B();
            }
        }

        private static void B()
        {
            Debug.Log($"NO");
        }

        private void A()
        {
            Debug.Log($"state is {nameof(BootState)}");
            ChangeState(new GameState());
        }

        public void ChangeState(IState newState)
        {
            if (CurrentState != null)
            {
                CurrentState.Exit();
                CurrentState.OnExiT -= CurrentStateOnOnExiT;
            }

            CurrentState = newState;
            CurrentState.OnExiT += CurrentStateOnOnExiT;
            CurrentState.StartState();
        }
        
        
    }
}