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

        int xmin=retrievedTiles.Min(tile => tile.Row);
        int zmin=retrievedTiles.Min(tile => tile.Col);
        Debug.LogFormat("tile min coordinate: {0},{1}",xmin,zmin);

        int xdim=retrievedTiles.Max(tile => tile.Row)-xmin+1;
        int zdim=retrievedTiles.Max(tile => tile.Col)-zmin+1;
        Debug.LogFormat("tile array size: [{0},{1}]",xdim,zdim);

        _tiles = new GameTile[xdim,zdim];

        int currentCoordx;
        int currentCoordz;
        for (int x=0;x<retrievedTiles.Length;x++)
        {
            currentCoordx=retrievedTiles[x].Row-xmin;
            currentCoordz=retrievedTiles[x].Col-zmin;

            if (_tiles[currentCoordx,currentCoordz])
            {
                Debug.LogFormat("conflict at {0},{1}",currentCoordx+xmin,currentCoordz+zmin);
                Debug.LogFormat("real coord: {0},{1}",retrievedTiles[x].transform.position.x,retrievedTiles[x].transform.position.z);
                Debug.LogFormat("real coord rounded: {0},{1}",
                    Mathf.Round(retrievedTiles[x].transform.position.x),
                    Mathf.Round(retrievedTiles[x].transform.position.z));
            }

            _tiles[currentCoordx,currentCoordz]=retrievedTiles[x];
        }
    }

    void gridCoverTest(int xdim,int zdim)
    {
        string res="";
        for (int x=0;x<xdim;x++)
        {
            for (int y=0;y<zdim;y++)
            {
                if (_tiles[x,y]!=null)
                {
                    res+="X ";
                }

                else
                {
                    res+="O ";
                }
            }

            res+="\n";
        }

        print(res);
    }
}