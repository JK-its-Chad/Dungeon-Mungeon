﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PWPawn : Pawn {

    public float StartingEnergy = 100.0f;
    public float StartingShields = 100.0f;

    public float Energy = 100.0f;
    public float Shields = 100.0f;
    public int bullets = 30;
    public int Key = 0;
    public bool paused = false;


    public virtual void Move(float value, float z)
    {

    }

    public virtual void Horizontal(float value)
    {

    }

    public virtual void Vertical(float value)
    {

    }

    public virtual void MouseX(float value)
    {

    }

    public virtual void MouseY(float value)
    {

    }

    public virtual void Trigger1(float value)
    {

    }

    public virtual void Trigger2(float value)
    {

    }

    public virtual void Fire1(bool value)
    {
       
    }

    public virtual void Fire2(bool value)
    {
       
    }

    public virtual void Fire3(bool value)
    {
        
    }

    public virtual void Fire4(bool value)
    {
       
    }

}
