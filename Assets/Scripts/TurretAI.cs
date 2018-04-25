using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : Actor {

    public GameObject Eye, Body, Gem, ProjectileSpawn;
    private GameObject Player;
    private bool canTurn = true;

	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

        if (Player && canTurn)
        {
            var lookPos = Player.transform.position - Body.transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            Body.transform.rotation = Quaternion.Slerp(Body.transform.rotation, rotation, Time.deltaTime * 5);
        }
        else Player = GameObject.FindGameObjectWithTag("Player");
    }
}
