using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : Actor {

    public GameObject Eye, Body, Gem, ProjectileSpawn, Projectile, Key;

    public GameObject Player, Room;

    public Material[] Colors;
    private GameObject MathObject;

    private bool canTurn = true;
    private bool canShoot = true;

    private float fireTimer = 1;
    private float stunTimer = 0;

	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        MathObject = new GameObject();
	}
	
	// Update is called once per frame
	void Update () {
        if(stunTimer > 0)
        {
            stunTimer -= Time.deltaTime;
        }
        if (Player && stunTimer <= 0)
        {
            var lookPos = Player.transform.position - Body.transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            Body.transform.rotation = Quaternion.Slerp(Body.transform.rotation, rotation, Time.deltaTime * 5);

            MathObject.transform.position = ProjectileSpawn.transform.position;
            MathObject.transform.LookAt(Player.transform);
        }

        if (fireTimer <= 0 && stunTimer <= 0)
        {
            Instantiate(Projectile, ProjectileSpawn.transform.position, MathObject.transform.rotation);
            fireTimer = 2f;
        }
        else fireTimer -= Time.deltaTime;
    }

    public void EyeHit()
    {
        canTurn = false;
        canShoot = false;
        stunTimer = 4;
    }

    public void Die()
    {
        GameObject SpawnedKey = Instantiate(Key, new Vector3(gameObject.transform.position.x, 6, gameObject.transform.position.z), Quaternion.identity);
        SpawnedKey.GetComponent<Renderer>().material = Colors[Room.GetComponent<Room>().KeyAccess + 1];
        foreach(Transform child in SpawnedKey.transform)
        {
            child.GetComponent<Renderer>().material = Colors[Room.GetComponent<Room>().KeyAccess + 1];
        }
        Destroy(gameObject);
    }
}
