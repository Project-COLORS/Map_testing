using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pchar:MonoBehaviour
{
    GameObject m_cam; //the camera object

    int[] m_pos=new int[2]{4,2}; //current coordinate position of character

    void Start()
    {
        m_cam=GameObject.Find("maincam");
    }

    void Update()
    {
        //can be moved out later
        transform.forward=-m_cam.transform.forward;
    }
}
