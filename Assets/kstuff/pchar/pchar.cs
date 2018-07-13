﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pchar:MonoBehaviour
{
    GameObject m_cam; //the camera object
    tgrid m_tgrid; //the tgrid

    int[] m_pos=new int[2]{4,2}; //current coordinate position of character

    void Start()
    {
        m_cam=GameObject.Find("maincam");
        m_tgrid=GameObject.Find("tgrid").GetComponent<tgrid>();
    }

    void Update()
    {
        //can be moved out later
        transform.forward=-m_cam.transform.forward;
    }

    public void initialiseChar()
    {
        m_tgrid.placeChar(m_pos[0],m_pos[1],this);
        transform.position=m_tgrid.coordsToRealCoords(m_pos[0],m_pos[1]);
    }
}