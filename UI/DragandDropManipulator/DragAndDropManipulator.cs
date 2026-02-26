using System;
using UnityEngine;
using UnityEngine.UIElements;

public class DragAndDropManipulator : PointerManipulator
{
    bool dragging = false;
    protected VisualElement _ghosticon;
    public Func<Sprite> PointerMove;
    public Action DragStop;
    public Action<VisualElement, VisualElement> OnDrop;
    public Func<VisualElement> DropSlot;
    Vector3 origin;
    public Action _onClick;

    public DragAndDropManipulator(VisualElement ghost, Action onclick=null) {
        this._ghosticon = ghost;
        _onClick = onclick;
        activators.Add(new ManipulatorActivationFilter { button = MouseButton.LeftMouse });
    }

    protected override void RegisterCallbacksOnTarget() {
        target.RegisterCallback<PointerDownEvent>(OnPointerDown);
        target.RegisterCallback<PointerLeaveEvent>(OnPointerLeave);
        target.RegisterCallback<PointerUpEvent>(OnPointerUp);
        target.RegisterCallback<PointerMoveEvent>(OnPointerMove);
    }

    protected override void UnregisterCallbacksFromTarget() {
        target.UnregisterCallback<PointerDownEvent>(OnPointerDown);
        target.UnregisterCallback<PointerUpEvent>(OnPointerUp);
        target.UnregisterCallback<PointerLeaveEvent>(OnPointerLeave);
        target.UnregisterCallback<PointerMoveEvent>(OnPointerMove);
    }

    private void OnPointerUp(PointerUpEvent evt) {
        target.ReleasePointer(evt.pointerId);
        if (!dragging) {
            _onClick?.Invoke();
            return;
        }

        Pointer.Instance.CanDrag = true;
        dragging = false;
        _ghosticon.style.visibility = Visibility.Hidden;
        DragStop?.Invoke();
        OnDrop?.Invoke(target, DropSlot?.Invoke());
    }

    private void OnPointerDown(PointerDownEvent evt) {
        if (!CanStartManipulation(evt)) 
            return;

        Pointer.Instance.CanDrag = false;
        origin = evt.position;
        _ghosticon.BringToFront();
        target.CapturePointer(evt.pointerId);
        evt.StopPropagation();
    }

    private void OnPointerMove(PointerMoveEvent evt) {
        if (!target.HasPointerCapture(evt.pointerId)) return;
        if ((evt.position - origin).magnitude > .2f) {
            
            _ghosticon.style.backgroundImage = new StyleBackground(PointerMove?.Invoke());
            _ghosticon.style.visibility = Visibility.Visible;
            dragging = true;
            _ghosticon.BringToFront();
            _ghosticon.transform.position = evt.position - new Vector3(_ghosticon.style.width.value.value/2, _ghosticon.style.height.value.value/2);
        }
       

        evt.StopPropagation();
    }

    private void OnPointerLeave(PointerLeaveEvent evt) =>
        // target.ReleasePointer(evt.pointerId);
        evt.StopPropagation();

}