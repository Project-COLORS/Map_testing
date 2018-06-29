using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ttile
{
    public int test=3;
    public ttile[] m_neighbours;

    public pchar m_occupyingChar;
}

public class tgrid:MonoBehaviour
{
    int[] m_gridDim=new int[2]{11,11}; //the dimensions of the map
    float m_tileSize=0.996f;
    [System.NonSerialized]
    //initial centre position of realworld grid tile,
    //obtained from the cursor which sets the initial pos
    public Vector3 m_initialCentrepos;

    [System.NonSerialized]
    public ttile[,] m_tiles; //the tile objects

    void Start()
    {
        initialiseGrid();
    }

    void initialiseGrid()
    {
        m_tiles=new ttile[m_gridDim[0],m_gridDim[1]];

        for (int x=0;x<m_gridDim[0];x++)
        {
            for (int y=0;y<m_gridDim[1];y++)
            {
                m_tiles[x,y]=new ttile();
            }
        }

        ttile[] neighbours;

        for (int x=0;x<m_gridDim[0];x++)
        {
            for (int y=0;y<m_gridDim[1];y++)
            {
                neighbours=new ttile[4]{null,null,null,null};

                if (y-1>=0)
                {
                    neighbours[0]=m_tiles[x,y-1];
                }

                if (y+1<m_gridDim[1])
                {
                    neighbours[2]=m_tiles[x,y+1];
                }

                if (x-1>=0)
                {
                    neighbours[3]=m_tiles[x-1,y];
                }

                if (x+1<m_gridDim[0])
                {
                    neighbours[1]=m_tiles[x+1,y];
                }

                m_tiles[x,y].m_neighbours=neighbours;
            }
        }
    }

    //place a char at the given coordinate
    public void placeChar(int x,int z,pchar theChar)
    {
        m_tiles[x,z].m_occupyingChar=theChar;
    }

    //return a vector of realworld coordinates given int grid
    //coordinates
    public Vector3 coordsToRealCoords(int x,int z)
    {
        return new Vector3(x*m_tileSize+m_initialCentrepos[0],m_initialCentrepos[1],z*m_tileSize+m_initialCentrepos[2]);
    }
}
