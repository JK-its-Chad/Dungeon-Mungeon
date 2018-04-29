using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour {

    public GameObject turret1, turret2, turret3, turret4;


	
	void Update ()
    {

        if (!turret1 && !turret2 && !turret3 && !turret4)
        {
            //this gets called when final boss is killed so insert sax here
        }
	}
}
