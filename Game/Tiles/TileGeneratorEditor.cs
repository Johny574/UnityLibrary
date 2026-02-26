using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TileGenerator))]
public class TileGeneratorEditor : Editor
{
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        var t = target as TileGenerator;
        if (GUILayout.Button("Generate"))
            t.GenerateRandom(t.Tilemaps[t.BaseMap], t.Tilemaps[t.GenerateMap]);

        if (GUILayout.Button("Clear"))
            t.Clear(t.Tilemaps[t.BaseMap]);

        if (GUILayout.Button("Undo"))
            t.Undo(t.Tilemaps[t.BaseMap]);
    }
}