using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ttile
{
    public int test=3;
    public ttile[] m_neighbours;
}

public class tgrid:MonoBehaviour
{
    int[] m_gridDim=new int[2]{11,11};
    [System.NonSerialized]
    public ttile[,] m_tiles;

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
}
