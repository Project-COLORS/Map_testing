using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basechar:MonoBehaviour
{
    cursorscrip1 m_cursor;
    tilecontrol m_tilecontrol;

    public int[] m_pos=new int[2];

    void Start()
    {
        m_cursor=GameObject.Find("cursor").GetComponent<cursorscrip1>();
        m_tilecontrol=GameObject.Find("tilesystem").GetComponent<tilecontrol>();

        move();
    }

    void move()
    {
        m_cursor.enterSelectMode(relocate);
    }

    void relocate()
    {
        Vector3 newpos=new Vector3(m_cursor.m_selectedTile.m_centre[0],.64f,m_cursor.m_selectedTile.m_centre[1]);

        transform.position=newpos;
    }
}
