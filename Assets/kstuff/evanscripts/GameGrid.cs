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

        //get the lowest coordinate of the tiles
        int xmin=retrievedTiles.Min(tile => tile.Row);
        int zmin=retrievedTiles.Min(tile => tile.Col);
        Debug.LogFormat("tile min coordinate: {0},{1}",xmin,zmin);

        //determine the dimensions of the grid, calculated by the max minus the min +1
        //manually confirm that this is correct (cross check with physical map)
        int xdim=retrievedTiles.Max(tile => tile.Row)-xmin+1;
        int zdim=retrievedTiles.Max(tile => tile.Col)-zmin+1;
        Debug.LogFormat("tile array size: [{0},{1}]",xdim,zdim);

        _tiles = new GameTile[xdim,zdim];

        //assign the retrieved tiles to the tile array
        int currentCoordx;
        int currentCoordz;
        for (int x=0;x<retrievedTiles.Length;x++)
        {
            //offset the coordinate to 0 (so it is possible for tiles to have negative x/z values)
            currentCoordx=retrievedTiles[x].Row-xmin;
            currentCoordz=retrievedTiles[x].Col-zmin;

            //detect tiles with identical coordinates (round error occured)
            //if the dimensions calculated earlier are correct and there are no duplicate coordinates,
            //it should guarantee that every tile position is correct
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

        initialiseChars();
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

    public void setCharacter(int xpos,int zpos,pchar character)
    {
        _tiles[xpos,zpos].occupyChar=character;
        Vector3 charposition=new Vector3(_tiles[xpos,zpos].transform.position[0],character.transform.position[1],_tiles[xpos,zpos].transform.position[2]);
        character.transform.position=charposition;
    }

    void initialiseChars()
    {
        pchar[] pchars=GameObject.Find("chars").GetComponentsInChildren<pchar>();

        for (var x=0;x<pchars.Length;x++)
        {
            pchars[x].setLocation();
        }
    }
}