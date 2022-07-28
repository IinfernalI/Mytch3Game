
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class Board : MonoBehaviour
{
    public static Board Instance { get; private set; }

    public Row[] rows;

    public Tile[,] Tiles { get; private set; }

    private readonly List<Tile> _selection = new List<Tile>();

    public int Width => Tiles.GetLength(0);
    public int Height => Tiles.GetLength(1);

    private void Awake() => Instance = this;

    private void Start()
    {
        //!Max => LiQ
        Tiles = new Tile[rows.Max(row => row.tiles.Length), rows.Length];

        for (int y = 0; y < Width; y++)
        {
            for (int x = 0; x < Height; x++)
            {
                var tile = rows[y].tiles[x];

                tile.x = x;
                tile.y = y;

                tile.Item = ItemDataBase.Items[Random.Range(0, ItemDataBase.Items.Length)];

                Tiles[x, y] = tile;
            }
        }
    }

    public void Select(Tile tile)
    {
        if (!_selection.Contains(tile)) { _selection.Add(tile); }

        
        
        if (_selection.Count < 2) { return; }
        
        
        Debug.Log($"Selected tile at ({_selection[0].x}) , ({_selection[0].y}) and ({_selection[1].x}) , ({_selection[1].y})");
        
        _selection.Clear();
        
    }
}
