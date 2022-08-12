namespace DefaultNamespace
{
    public interface IState
    {
        public void Start();
        public IState Exit();
    }
}