using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class cursorscrip1:MonoBehaviour
{
    /*-- externals --*/
    public GameObject m_cam; //maincam
    public Rigidbody m_body; //self

    public Transform m_gridthing; //the blue grid square, to be replaced!!!

    public tilecontrol m_tilecontrol;

    /*-- movement vars --*/
    Vector3 m_moveVec=new Vector3();
    Vector3 m_posvec=new Vector3();
    float m_cursorspeed=7.6f;

    /*-- grid float vars, might deprecate later --*/
    [NonSerialized]
    public Vector3 m_centrepos=new Vector3(); //current grid square coordiante's centre
    [NonSerialized]
    public int[] m_pos=new int[2]{0,0}; //current grid square coordinate

    float m_tileSizehalf;

    /*-- selection system --*/
    bool m_selectActive=false;
    Action m_currentSelectCommand;
    [NonSerialized]
    public tile m_selectedTile;

    void Start()
    {
        m_tileSizehalf=m_tilecontrol.m_tileSize/2;

        m_centrepos.x=transform.position.x;
        m_centrepos.z=transform.position.z;
        m_centrepos.y=m_gridthing.position.y;

        m_tilecontrol.m_initialTilePos=new float[2]{m_centrepos[0],m_centrepos[2]};
        m_tilecontrol.genGrid();
    }

    void Update()
    {
        transform.forward=-m_cam.transform.forward;

        m_moveVec.x=Input.GetAxisRaw("Vertical");
        m_moveVec.z=Input.GetAxisRaw("Horizontal");

        updatePosition();
        calcPos();

        if (Input.GetButtonDown("selectkey"))
        {
            if (m_selectActive)
            {
                selectRequest();
            }
        }
    }

    void updatePosition()
    {
        m_moveVec.Normalize();
        m_moveVec.x=m_moveVec.x*m_cursorspeed*-1;
        m_moveVec.z=m_moveVec.z*m_cursorspeed;
        // transform.Translate(m_moveVec,Space.World);
        m_body.velocity=m_moveVec;

        m_posvec=transform.position;
        m_posvec.x+=5;
        m_posvec.y+=4;
        m_posvec.z+=5;

        m_cam.transform.position=m_posvec;
    }

    void calcPos()
    {
        float diff=transform.position.x-m_centrepos.x;
        if (Mathf.Abs(diff)>=m_tileSizehalf)
        {
            //instead of recalculating pos, just increment or decrement
            if (diff>0)
            {
                m_pos[0]+=1;
                m_centrepos.x+=m_tilecontrol.m_tileSize;
            }

            else
            {
                m_pos[0]-=1;
                m_centrepos.x-=m_tilecontrol.m_tileSize;
            }

            if (m_tilecontrol.coordsToIndex(m_pos[0],m_pos[1])>=0)
            {
                m_gridthing.position=m_centrepos;
            }

            print(String.Format("{0},{1}",m_pos[0],m_pos[1]));
        }

        diff=transform.position.z-m_centrepos.z;
        if (Mathf.Abs(diff)>=m_tileSizehalf)
        {
            if (diff>0)
            {
                m_pos[1]+=1;
                m_centrepos.z+=m_tilecontrol.m_tileSize;
            }

            else
            {
                m_pos[1]-=1;
                m_centrepos.z-=m_tilecontrol.m_tileSize;
            }

            if (m_tilecontrol.coordsToIndex(m_pos[0],m_pos[1])>=0)
            {
                m_gridthing.position=m_centrepos;
            }

            print(String.Format("{0},{1}",m_pos[0],m_pos[1]));
        }
    }

    //request the cursor go into select mode. give it the function that
    //will be executed on valid selection.
    public void enterSelectMode(Action command)
    {
        m_selectActive=true;
        m_currentSelectCommand=command;
    }

    //attempt to perform the current queued select command, after doing checks
    void selectRequest()
    {
        //once tile system is implemented, do checks to see if
        //the tile is actually selectable before activating the callback

        int tileindex=m_tilecontrol.coordsToIndex(m_pos[0],m_pos[1]);
        if (tileindex<0)
        {
            return;
        }

        m_selectedTile=m_tilecontrol.m_tiles[tileindex];
        m_currentSelectCommand();
        m_currentSelectCommand=null;
        m_selectActive=false;
    }
}