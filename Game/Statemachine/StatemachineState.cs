public class StatemachineState<S, T> where S : Statemachine<T> {
    protected S _statemachine;
    public StatemachineState(S statemachine) {
        _statemachine = statemachine;
    }
}