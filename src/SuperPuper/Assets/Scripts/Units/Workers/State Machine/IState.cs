namespace Workers.State_Machine
{
    public interface IState
    {
        void OnEnter();
        void Tick();
        void OnExit();
    }
}
