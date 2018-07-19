using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pchar:MonoBehaviour
{
    GameObject m_cam; //the camera object
    GameGrid m_gameGrid;
    cursorscrip2 _cursor;

    public int[] m_pos=new int[2]{4,2}; //current coordinate position of character

    void Start()
    {
        m_cam=GameObject.Find("maincam");
        m_gameGrid=GameObject.Find("grid").GetComponent<GameGrid>();
        _cursor=GameObject.Find("cursor").GetComponent<cursorscrip2>();
    }

    void Update()
    {
        //can be moved out later
        transform.forward=-m_cam.transform.forward;
    }

    //execute character place
    public void setLocation()
    {
        m_gameGrid.setCharacter(m_pos[0],m_pos[1],this);
        actiontest();
    }

    void actiontest()
    {
        m_gameGrid.clearHighlightEffects();

        System.Func<GameTile,bool> callback=(tile)=>{
            m_gameGrid.highlightEffect(tile);
            return true;
        };

        // m_gameGrid.lineQuery(m_pos[0],m_pos[1],1,1,callback);
        // m_gameGrid.lineQuery(m_pos[0],m_pos[1],1,-1,callback);
        // m_gameGrid.lineQuery(m_pos[0],m_pos[1],0,1,callback);
        // m_gameGrid.lineQuery(m_pos[0],m_pos[1],0,-1,callback);

        m_gameGrid.rangeQuery(m_pos[0],m_pos[1],2,0,callback);

        _cursor.queueCursorCommand((GameTile tile)=>{
            Debug.LogFormat("moving to {0},{1}",tile.Row,tile.Col);

            m_gameGrid.clearHighlightEffects();
        });
    }
}
