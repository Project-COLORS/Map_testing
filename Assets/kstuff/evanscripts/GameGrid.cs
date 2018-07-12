using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    public GameObject BoundObject;

    private GameTile[,] _tiles;

    private void Start()
    {
        var retrievedTiles = BoundObject.GetComponentsInChildren<GameTile>();

        int xdim=retrievedTiles.Max(tile => tile.Row)-retrievedTiles.Min(tile => tile.Row)+1;
        int zdim=retrievedTiles.Max(tile => tile.Col)-retrievedTiles.Min(tile => tile.Col)+1;
        _tiles = new GameTile[xdim,zdim];

        print(xdim);
        print(zdim);
    }
}
