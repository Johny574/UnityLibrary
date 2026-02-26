using UnityEngine.UIElements;

public class StyleManipulator : Manipulator
{
    string _upstyle, _downstyle;

    public StyleManipulator(string upstyle, string downstyle) {
        _upstyle = upstyle;
        _downstyle = downstyle;
    }

    protected override void RegisterCallbacksOnTarget() {
        target.pickingMode = PickingMode.Position;
        target.RegisterCallback<PointerDownEvent>(OnClickEvent, TrickleDown.TrickleDown);
        target.RegisterCallback<PointerUpEvent>(OnPointerUp, TrickleDown.TrickleDown);
  
    }

    private void OnClickEvent(PointerDownEvent evt) {
        target.AddToClassList(_downstyle);
        target.RemoveFromClassList(_upstyle);
    }

    private void OnPointerUp(PointerUpEvent evt) => target.schedule.Execute(() =>
    {
        target.AddToClassList(_upstyle);
        target.RemoveFromClassList(_downstyle);
    }).StartingIn(100); // delay in milliseconds

    protected override void UnregisterCallbacksFromTarget() {
        target.UnregisterCallback<PointerDownEvent>(OnClickEvent);
        target.UnregisterCallback<PointerUpEvent>(OnPointerUp);
    }
}