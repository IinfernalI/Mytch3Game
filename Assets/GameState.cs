namespace DefaultNamespace
{
    public class GameState : IState
    {
        StateMachineHandler _handler;

        public GameState(StateMachineHandler handler)
        {
            _handler = handler;
        }
        public void Start()
        {
            throw new System.NotImplementedException();
        }

        public IState Exit()
        {
            return this;
        }
    }
}