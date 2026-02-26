using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class TabStatemachineState<S, T>: StatemachineState<S, T>, IStatemachineState where S : Statemachine<T>
{
    protected VisualElement _menu, _tabRoot;
    Tab _tab;
    public T Key { get; private set; }
    protected AudioSource _buttonSfx, _tabSfx;
    Label _heading; 
    protected abstract string _headingText {get;}

    public TabStatemachineState(S statemachine, VisualElement menuRoot, VisualTreeAsset menu_t, VisualElement tabRoot, VisualTreeAsset tab_t, T key, AudioSource buttonSfx, AudioSource tabSfx) : base(statemachine) {
        _tabRoot = tabRoot;
        
        tabSfx = _tabSfx;

        _menu = menu_t.CloneTree().Children().First();
        _menu.style.display = DisplayStyle.None;
        
        _heading = menuRoot.parent.parent.Q<Label>("heading");
        menuRoot.Add(_menu);

        _tab = tab_t.CloneTree().Children().First() as Tab;
        _tab.label = key.ToString();
        _tabRoot.Add(_tab);
        _tab.AddManipulator(new StyleManipulator("button-dark", "button-light"));

        Key = key;
        _tab.selected += (evt) =>
        {
            _tabSfx?.Play();
            _statemachine.ChangeState(key);
        };

    }

    public bool GetTransitionCondition() => false;

    public virtual void OnAwake() {
    }

    public void Tick() {
    }

    public virtual void TransitionEnter() {
        _menu.style.display = DisplayStyle.Flex;
        _heading.text = _headingText;   
        _tabRoot.style.display = DisplayStyle.Flex;
    }

    public void TransitionExit() {
        _menu.style.display = DisplayStyle.None;
        _tabRoot.style.display = DisplayStyle.None;
    }
}