using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : Actor {

    public GameObject Eye, Body, Gem, ProjectileSpawn, Projectile;

    public GameObject Player;
    private GameObject MathObject;

    private bool canTurn = true;
    private bool canShoot = false;

    private float fireTimer = 2;

	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        MathObject = new GameObject();
	}
	
	// Update is called once per frame
	void Update () {

        if (Player && canTurn)
        {
            var lookPos = Player.transform.position - Body.transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            Body.transform.rotation = Quaternion.Slerp(Body.transform.rotation, rotation, Time.deltaTime * 5);

            MathObject.transform.position = ProjectileSpawn.transform.position;
            MathObject.transform.LookAt(Player.transform);
        }
        else Player = GameObject.FindGameObjectWithTag("Player");

        if (fireTimer <= 0)
        {
            Instantiate(Projectile, ProjectileSpawn.transform.position, MathObject.transform.rotation);
            fireTimer = 2f;
        }
        else fireTimer -= Time.deltaTime;
    }
}
