



using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePainter : Singleton<TilePainter> {    
    public string Key;
    [SerializeField] TileBase _tile;
    public void Paint(string key, Rect dimensions) {
        for (float x = dimensions.Dimensions.min.x; x < dimensions.Dimensions.max.x; x++)
            for (float y = dimensions.Dimensions.min.y; y < dimensions.Dimensions.max.y; y++)
                Maps[key].SetTile(new Vector3Int((int)x ,(int)y, 0), _tile);
    }

    public void Clear(string key) => Maps[key].ClearAllTiles();
}