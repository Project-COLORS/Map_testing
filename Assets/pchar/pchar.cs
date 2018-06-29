using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pchar:MonoBehaviour
{
    GameObject m_cam; //the camera object
    tgrid m_tgrid; //the tgrid

    int[] m_pos=new int[2]{3,1}; //current coordinate position of character

    void Start()
    {
        m_cam=GameObject.Find("maincam");
        m_tgrid=GameObject.Find("tgrid").GetComponent<tgrid>();

        m_tgrid.placeChar(m_pos[0],m_pos[1],this);
        transform.position=m_tgrid.coordsToRealCoords(m_pos[0],m_pos[1]);
    }

    void Update()
    {
        transform.forward=-m_cam.transform.forward;
    }
}
