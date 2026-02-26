using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Statemachine<T> : MonoBehaviour {
    public T CurrentState { get; private set; }
    public Dictionary<T, IStatemachineState> States { get; private set; }
    protected List<StatemachineTrasition<T>> _transitions;
    protected List<StatemachineTrasition<T>> _anyTransitions;
    protected abstract Dictionary<T, IStatemachineState> CreateStates();
    protected abstract List<StatemachineTrasition<T>> CreateTransitions();
    protected abstract List<StatemachineTrasition<T>> CreateAnyTransitions();

    [SerializeField] bool _enableDebugging = false;
    
    public virtual void Start() {
        States = CreateStates();
        _transitions = CreateTransitions();
        _anyTransitions = CreateAnyTransitions();
        CurrentState = States.First().Key;

        foreach (var state in States) {
            state.Value?.OnAwake();
        }

        ChangeState(CurrentState);
    }

    public virtual void ChangeState(T newState) {
        States[CurrentState]?.TransitionExit();
        CurrentState = newState;

        if (_enableDebugging)
            Debug.Log(CurrentState);

        States[CurrentState]?.TransitionEnter();
    }

    public virtual void Update() {
        var transition = GetTransition();
        if (transition != null && States[transition.To] != States[CurrentState]) {
            ChangeState(transition.To);
        }
        States[CurrentState]?.Tick();
    }

    protected StatemachineTrasition<T> GetTransition()  {
        var at = GetAnyTransitionReference();

        if (at != null){
            return at; 
        } 
        
        return GetTransitionReference();
    } 

    protected StatemachineTrasition<T> GetAnyTransitionReference() {
        foreach (var transition in _anyTransitions) {
            if (transition.Condition()){
                return transition;
            }
        }
        return null;
    }

    protected StatemachineTrasition<T> GetTransitionReference() {
        foreach (var transition in _transitions) {
            if (States[CurrentState] == States[transition.From]){
                if (transition.Condition()){
                    return transition;
                }
            }
        }
        return null;
    }
}