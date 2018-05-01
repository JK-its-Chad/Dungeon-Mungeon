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

    private float fireTimer = 0.5f;
    public float fireCooldown = 0.5f;
    public float StunLength = 4;
    public float Health = 150;
    private float stunTimer = 0;

    public bool dropKey = true;

	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        MathObject = new GameObject();
        Gem.GetComponent<TurretTarget>().Health = Health;
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
            Vector3 directionToTarget = Body.transform.position - Player.transform.position;
            float angle = Vector3.Angle(Body.transform.forward, directionToTarget);
            if (Mathf.Abs(angle) > 90) {
                Instantiate(Projectile, ProjectileSpawn.transform.position, MathObject.transform.rotation);
                fireTimer = 2f;
            }
            FindObjectOfType<AudioManager>().play("TowerAttack");
        }
        else fireTimer -= Time.deltaTime;
    }

    public void EyeHit()
    {
        canTurn = false;
        canShoot = false;
        stunTimer = StunLength;
    }

    public void Die()
    {
        if (dropKey)
        { 
            GameObject SpawnedKey = Instantiate(Key, new Vector3(gameObject.transform.position.x, 6, gameObject.transform.position.z), Quaternion.identity);
            SpawnedKey.GetComponent<Renderer>().material = Colors[Room.GetComponent<Room>().KeyAccess + 1];
            foreach (Transform child in SpawnedKey.transform)
            {
                child.GetComponent<Renderer>().material = Colors[Room.GetComponent<Room>().KeyAccess + 1];
            }
        }
        Destroy(gameObject);
    }
}
