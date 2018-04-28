using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEye : Actor
{

    public TurretAI Turret;


    protected override bool ProcessDamage(Actor Source, float Value, DamageEventInfo EventInfo, Controller Instigator)
    {
        Turret.EyeHit();
        return base.ProcessDamage(Source, Value, EventInfo, Instigator);
    }
}

