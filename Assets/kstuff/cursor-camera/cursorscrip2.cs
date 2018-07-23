using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class cursorscrip2:MonoBehaviour
{
    /*-- externals --*/
    public GameObject m_cam; //maincam
    public Rigidbody m_body; //self

    /*-- movement vars/cam vars --*/
    Vector3 m_moveVec=new Vector3();
    Vector3 t_moveVec=new Vector3();
    Vector3 m_posvec=new Vector3();
    float m_cursorspeed=7.9f;

    Vector3 m_camAngle=new Vector3(30,0,0);
    /*  for each camera position (4 positions):
        0: multiplier for X position shift
        1: multiplier for Z position shift
        2: Y angle of camera (currently not being used)
        3: horizontal control cursor movement axis. 0 for X, 2 for z
        4: vertical control cursor movement axis
        5: horizontal control invert. 1 for normal, -1 for inverted
        6: vertical control invert*/
    int [,] m_camPositions=new int[4,7]{{1,1,225,0,2,-1,1},{-1,1,135,2,0,-1,-1},{-1,-1,45,0,2,1,-1},{1,-1,-45,2,0,1,1}};
    //index number of camPosition array for current position
    int m_currentcamPosition=0;
    //{x position,z position,y degree} current values for
    //cam transforms, for intermediate interpolation
    float[] m_camPositionsCurrent=new float[3]{0,0,0};
    //camera angle to interpolate to. set on rotation button presses
    float m_targetcamYAngle=225f;

    /*-- grid float vars, might deprecate later --*/
    [NonSerialized]
    public Vector3 m_centrepos=new Vector3(); //current grid square coordiante's centre
    [NonSerialized]
    public int[] m_pos=new int[2]{0,0}; //current grid square coordinate

    /*-- temp grid parameters --*/
    //these should eventually be managed by the grid
    public int[] m_gridDim=new int[2]{7,7};
    public float m_tileSize=1.12f;
    float m_tileSizehalf;

    /*-- cursor command --*/
    bool _commandQueued=false;
    System.Action<GameTile> _currentCommand;

    /*-- menu interaction specific --*/
    bool _cursorDisabled=false;
    public pchar _currentChar;

    public GameObject _charMenu;
    bool _charMenucd=false;

    /*-- new grid --*/
    public GameGrid _grid;

    void Start()
    {
        m_tileSizehalf=m_tileSize/2;

        m_centrepos.x=transform.position.x;
        m_centrepos.z=transform.position.z;
    }

    void Update()
    {
        transform.forward=-m_cam.transform.forward;

        m_moveVec.x=Input.GetAxisRaw("Vertical");
        m_moveVec.z=Input.GetAxisRaw("Horizontal");

        if (!_cursorDisabled)
        {
            if (Input.GetButtonDown("selectkey"))
            {
                if (_commandQueued)
                {
                    GameTile clicktile=_grid.getTile(m_pos[0],m_pos[1]);

                    if (clicktile && clicktile.selectable)
                    {
                        _currentCommand(clicktile);
                        _commandQueued=false;
                    }
                }
            }

            else if (Input.GetButtonDown("menukey"))
            {
                setCharMenuState(true);
            }

            if (Input.GetButtonDown("rotateleft"))
            {
                m_targetcamYAngle-=90;
                if (m_currentcamPosition>=3)
                {
                    m_currentcamPosition=0;
                }

                else
                {
                    m_currentcamPosition++;
                }
            }

            else if (Input.GetButtonDown("rotateright"))
            {
                m_targetcamYAngle+=90;
                if (m_currentcamPosition<=0)
                {
                    m_currentcamPosition=3;
                }

                else
                {
                    m_currentcamPosition--;
                }
            }
        }

        else
        {
            m_moveVec.x=0;
            m_moveVec.z=0;
        }

        updatePosition();
        calcPos();

        _charMenucd=false;
    }

    void updatePosition()
    {
        m_moveVec.Normalize();
        t_moveVec.x=m_moveVec[m_camPositions[m_currentcamPosition,3]]*m_cursorspeed*m_camPositions[m_currentcamPosition,5];
        t_moveVec.z=m_moveVec[m_camPositions[m_currentcamPosition,4]]*m_cursorspeed*m_camPositions[m_currentcamPosition,6];
        // transform.Translate(m_moveVec,Space.World);
        t_moveVec=Quaternion.Euler(0,-45,0)*t_moveVec;
        m_body.velocity=t_moveVec;

        m_camPositionsCurrent[0]=Mathf.Lerp(m_camPositionsCurrent[0],5*m_camPositions[m_currentcamPosition,0],.1f);
        m_camPositionsCurrent[1]=Mathf.Lerp(m_camPositionsCurrent[1],5*m_camPositions[m_currentcamPosition,1],.1f);
        m_camPositionsCurrent[2]=Mathf.Lerp(m_camPositionsCurrent[2],m_targetcamYAngle,.1f);

        m_posvec=transform.position;
        m_posvec.x+=m_camPositionsCurrent[0];
        m_posvec.y+=4;
        m_posvec.z+=m_camPositionsCurrent[1];

        m_camAngle.y=m_camPositionsCurrent[2];

        m_cam.transform.eulerAngles=m_camAngle;
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
                m_centrepos.x+=m_tileSize;
            }

            else
            {
                m_pos[0]-=1;
                m_centrepos.x-=m_tileSize;
            }

            print(String.Format("{0},{1}",m_pos[0],m_pos[1]));
            _grid.hoverEffect(m_pos[0],m_pos[1]);
        }

        diff=transform.position.z-m_centrepos.z;
        if (Mathf.Abs(diff)>=m_tileSizehalf)
        {
            if (diff>0)
            {
                m_pos[1]+=1;
                m_centrepos.z+=m_tileSize;
            }

            else
            {
                m_pos[1]-=1;
                m_centrepos.z-=m_tileSize;
            }

            print(String.Format("{0},{1}",m_pos[0],m_pos[1]));
            _grid.hoverEffect(m_pos[0],m_pos[1]);
        }
    }

    //move this to grid control object later
    int coordsToIndex(int x,int z)
    {
        if (x<0 || x>=m_gridDim[0] || z<0 || z>=m_gridDim[1])
        {
            return -1;
        }

        return z*m_gridDim[0]+x;
    }

    //set the cursor to execute the given callback upon clicking on
    //any tile that is marked as selected. before using this function,
    //whatever function calls this function should take action to actually
    //mark tiles as selected. executing the command DOES NOT unmark tiles
    //from being selected. that is up to the callback.
    //the callback is given the clicked on tile.
    public void queueCursorCommand(System.Action<GameTile> callback)
    {
        _commandQueued=true;
        _currentCommand=callback;
    }

    //set the character menu to active or not
    public void setCharMenuState(bool state)
    {
        if (_charMenucd)
        {
            return;
        }

        _charMenucd=true;
        _cursorDisabled=state;
        _charMenu.SetActive(state);
    }
}