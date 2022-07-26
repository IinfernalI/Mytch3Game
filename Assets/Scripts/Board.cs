using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class Board : MonoBehaviour
{
    public static Board Instance { get; private set; }

    public Row[] rows;

    public Tile[,] Tiles { get; private set; }

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

                tile.ItemProp = ItemDataBase.ItemsProp[Random.Range(0, ItemDataBase.ItemsProp.Length)];

                Tiles[x, y] = tile;
            }
        }
    }
}
