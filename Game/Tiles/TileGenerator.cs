using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerator : MonoBehaviour
{
    [Range(2, 200)]
    [SerializeField] int _noise = 2;
    public TilemapDictionary Tilemaps;
    [SerializeField] TileBase _tile;
    List<Vector3Int> _previousGenerated = new();
    [SerializeField] public string BaseMap, GenerateMap;
    public void GenerateRandom(Tilemap basemap, Tilemap generatemap) {
        _previousGenerated.Clear();
        for (int x = basemap.cellBounds.min.x; x < basemap.cellBounds.max.x; x++) {
            for (int y = basemap.cellBounds.min.y; y < basemap.cellBounds.max.y; y++) {
                var position = generatemap.WorldToCell(new Vector2(x, y));
                if (basemap.HasTile(position)) {
                    if (Random.Range(0, _noise) == 0) {
                        generatemap.SetTile(position, _tile);
                        _previousGenerated.Add(position);
                    }
                }
            }
        }
    }

    public void Clear(Tilemap tilemap) => tilemap.ClearAllTiles();

    public void Undo(Tilemap tilemap) {
       foreach (var tile in _previousGenerated)
            tilemap.SetTile(tile, null);
    }
}
