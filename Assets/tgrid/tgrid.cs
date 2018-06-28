using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ttile
{
    public int test=3;
}

public class tgrid:MonoBehaviour
{
    int[] m_gridDim=new int[2]{10,10};
    [System.NonSerialized]
    public ttile[,] m_tiles;

    void Start()
    {
        m_tiles=new ttile[m_gridDim[0],m_gridDim[1]];

        for (int x=0;x<m_gridDim[0];x++)
        {
            for (int y=0;y<m_gridDim[1];y++)
            {
                m_tiles[x,y]=new ttile();
            }
        }
    }
}
