using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tilecontrol:MonoBehaviour
{
    public int[] m_gridDim=new int[2]{7,7}; //width,height (add 1 to the actual desired size)
    public float m_tileSize=1.12f;
    [System.NonSerialized]
    public float[] m_initialTilePos;

    int m_gridsize;

    [System.NonSerialized]
    public tile[] m_tiles;

    void Start()
    {
        m_gridsize=m_gridDim[0]*m_gridDim[1];
        m_tiles=new tile[m_gridsize];
    }

    public void genGrid()
    {
        int xcoord=0;
        int zcoord=0;
        for (int x=0;x<m_gridsize;x++)
        {
            m_tiles[x]=new tile(coordsToCentre(xcoord,zcoord));

            print(string.Format("{0},{1}",m_tiles[x].m_centre[0],m_tiles[x].m_centre[1]));

            xcoord++;

            if (xcoord>=m_gridDim[0])
            {
                xcoord=0;
                zcoord++;
            }
        }
    }

    //given x,z coords, return array index of associated tile, or
    //-1 if out of range
    public int coordsToIndex(int x,int z)
    {
        if (x<0 || x>=m_gridDim[0] || z<0 || z>=m_gridDim[1])
        {
            return -1;
        }

        return z*m_gridDim[0]+x;
    }

    //given grid coords, return the ingame centre point coords
    float[] coordsToCentre(int x,int z)
    {
        return new float[2]{m_tileSize*x+m_initialTilePos[0],m_tileSize*z+m_initialTilePos[1]};
    }
}
