


using UnityEngine;

public class CursorManager : Singleton<CursorManager>
{
    [SerializeField] Texture2D _cursor;

    // todo implement a dictionary of each cursor
    Vector2 _hotspot;
    protected override void Awake() {
        base.Awake();
        Cursor.SetCursor(_cursor, _hotspot, CursorMode.ForceSoftware);
    }    
}