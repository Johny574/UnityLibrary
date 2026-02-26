public interface IStatemachineState  {
    public abstract bool GetTransitionCondition();
    public abstract void OnAwake();
    public abstract void Tick();
    public abstract void TransitionEnter();
    public abstract void TransitionExit();
}