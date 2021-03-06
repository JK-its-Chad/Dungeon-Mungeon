﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Material[] Colors = new Material[20];
    public int KeyAccess;
    public GameObject FirstRoom;
    public GameObject SecondRoom;

    public virtual void setColor()
    {
        gameObject.GetComponent<Renderer>().material = Colors[KeyAccess];
    }
}
