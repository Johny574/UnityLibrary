
using UnityEngine;
using UnityEngine.UIElements;

public class ScaleManipulator : Manipulator
{
    Vector2  _downScale, _upscale;
    public ScaleManipulator(Vector2 downscale, Vector2 upscale) {
        _downScale = downscale;
        _upscale = upscale; 
    }

    protected override void RegisterCallbacksOnTarget() {
        target.RegisterCallback<PointerDownEvent>(OnClickEvent);
        target.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    private void OnClickEvent(PointerDownEvent evt) => target.style.scale = new StyleScale(new Scale(_downScale));

    private void OnPointerUp(PointerUpEvent evt) => target.schedule.Execute(() =>
    {
        target.style.scale = new StyleScale(new Scale(_upscale));
    }).StartingIn(100); // delay in milliseconds

    protected override void UnregisterCallbacksFromTarget() {
        target.UnregisterCallback<PointerDownEvent>(OnClickEvent);
        target.UnregisterCallback<PointerUpEvent>(OnPointerUp);
    }
}