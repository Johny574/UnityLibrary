using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Grid))]
public class Tilegrid : Singleton<Tilegrid> 
{
    Grid grid;
    [SerializeField] Tilemap _floormap;
    public Rect Dimensions;
    protected override void Awake() {
        base.Awake();
        grid = GetComponent<Grid>();
    }

    public Vector2 WorldToCellCenter(Vector2 pos) => grid.GetCellCenterWorld(grid.WorldToCell(pos));
    public Vector3Int WorldToCell(Vector2 pos) => grid.WorldToCell(pos);
    public Vector3 RandomPosition() => new Vector3(Random.Range(Dimensions.min.x, Dimensions.max.x), Random.Range(Dimensions.Dimensions.min.y, Dimensions.Dimensions.max.y), 0);
}