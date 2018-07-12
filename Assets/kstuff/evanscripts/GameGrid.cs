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
        GameTile[] retrievedTiles = BoundObject.GetComponentsInChildren<GameTile>();

        System.Array.Sort(retrievedTiles,(tile1,tile2)=>{
            if (tile1.Row==tile2.Row)
            {
                return 0;
            }

            if (tile1.Row>tile2.Row)
            {
                return 1;
            }

            return -1;
        });

        int xdim=retrievedTiles.Max(tile => tile.Row)-retrievedTiles.Min(tile => tile.Row)+1;
        int zdim=retrievedTiles.Max(tile => tile.Col)-retrievedTiles.Min(tile => tile.Col)+1;

        _tiles = new GameTile[xdim,zdim];

        print(xdim);
        print(zdim);

        for (var x=0;x<retrievedTiles.Length;x++)
        {
            print(retrievedTiles[x].Row+","+retrievedTiles[x].Col);
        }
    }
}
