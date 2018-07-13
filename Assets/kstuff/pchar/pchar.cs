using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pchar:MonoBehaviour
{
    GameObject m_cam; //the camera object
    GameGrid m_gameGrid;

    public int[] m_pos=new int[2]{4,2}; //current coordinate position of character

    void Start()
    {
        m_cam=GameObject.Find("maincam");
        m_gameGrid=GameObject.Find("grid").GetComponent<GameGrid>();
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
    }
}
