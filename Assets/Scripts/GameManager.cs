using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool holding;
    public bool rotating;

    int _points;
    public int points
    {
        get
        {
            return _points;
        }
        set
        {
            _points = value;
            print(_points);
        }
    }

    public float mouseSens = 100f;

    public Package currentPackage;

    public GameObject addressBox;
}
