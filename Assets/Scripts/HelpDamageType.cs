using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpDamageType : BaseDamageType
{

    public HelpDamageType()
    {
        verb = "Health Healed!";
        CausedByWorld = true;
    }
}
