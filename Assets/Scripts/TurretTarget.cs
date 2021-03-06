﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTarget : Actor
{
    public TurretAI Turret;
    public float Health = 150;

    protected override bool ProcessDamage(Actor Source, float Value, DamageEventInfo EventInfo, Controller Instigator)
    {
        Health -= Value;

        if(Health <= 0)
        {
            Turret.Die();
            FindObjectOfType<AudioManager>().play("TowerDeath");
        }
        return base.ProcessDamage(Source, Value, EventInfo, Instigator);
    }

}
