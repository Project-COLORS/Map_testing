using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testui:MonoBehaviour
{
    public cursorscrip2 _cursor;

    public Transform _actions;
    public Transform _actionSelected;
    int _numberActions=3;

    int _menupos=0;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetButtonDown("downkey"))
        {
            menuNav(1);
        }

        else if (Input.GetButtonDown("upkey"))
        {
            menuNav(-1);
        }

        else if (Input.GetButtonDown("menukey"))
        {
            _cursor.setCharMenuState(false);
        }
    }

    void menuNav(int direction)
    {
        _actions.transform.GetChild(_menupos).gameObject.SetActive(true);
        _actionSelected.transform.GetChild(_menupos).gameObject.SetActive(false);

        _menupos+=direction;
        if (_menupos>=_numberActions)
        {
            _menupos=0;
        }

        else if(_menupos<0)
        {
            _menupos=_numberActions-1;
        }

        _actions.transform.GetChild(_menupos).gameObject.SetActive(false);
        _actionSelected.transform.GetChild(_menupos).gameObject.SetActive(true);
    }
}