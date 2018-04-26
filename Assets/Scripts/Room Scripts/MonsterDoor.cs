using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDoor : Door {

    // HEY
    // SPAWN ME 25.5 AWAY
    // ROTATE ME 90 IF IM LEFT OR RIGHT
    public DoorCollider Side1, Side2;
    public GameObject golem, knight, lizard, skeleton, Turret;
    public GameObject barrel, spikes, rocks;
    public Door Arch;

    GameObject item;
    GameObject monster;

    Transform trans;

    Quaternion faceDoor;

	void Start ()
    {
        trans = GetComponent<Transform>();
	}

    private void Update()
    {
        if (Side1.doorCollision.tag == "Player" && Side1.doorCollision.GetComponentInParent<PlayerPawn>().Key >= Arch.KeyAccess)
        {
            if (Arch.SecondRoom.GetComponent<KeyRoom>())
            {
                Vector3 centerOfRoom = Arch.SecondRoom.transform.position;
                //GameObject MiniBoss = Instantiate(Turret, centerOfRoom, Quaternion.identity);
                //MiniBoss.transform.position = new Vector3(Arch.SecondRoom.transform.position.x, -2, Arch.SecondRoom.transform.position.z);
            }
            else
            {
                Vector3 centerOfRoom = Arch.SecondRoom.transform.position;

                //pickMonster(randomPos(centerOfRoom));
                //pickMonster(randomPos(centerOfRoom));
                //pickMonster(randomPos(centerOfRoom));


                pickProp(randomPos(centerOfRoom));
                pickProp(randomPos(centerOfRoom));
            }
           
            Destroy(gameObject);
        }
        if (Side2.doorCollision.tag == "Player" && Side2.doorCollision.GetComponentInParent<PlayerPawn>().Key >= Arch.KeyAccess)
        {
           
            if (Arch.FirstRoom.GetComponent<KeyRoom>())
            {
                Vector3 centerOfRoom = Arch.FirstRoom.transform.position;
                //GameObject MiniBoss = Instantiate(Turret, centerOfRoom, Quaternion.identity);
                //MiniBoss.transform.position = new Vector3(Arch.FirstRoom.transform.position.x, -2, Arch.FirstRoom.transform.position.z);
            }
            else
            {
                Vector3 centerOfRoom = Arch.FirstRoom.transform.position;

                //pickMonster(randomPos(centerOfRoom));
                //pickMonster(randomPos(centerOfRoom));
                //pickMonster(randomPos(centerOfRoom));


                pickProp(randomPos(centerOfRoom));
                pickProp(randomPos(centerOfRoom));
            }

            Destroy(gameObject);
        }
    }

    void pickProp(Vector3 pos)
    {
        switch (rollDice(3))
        {
            case 0:
                item = barrel;
                break;
            case 1:
                item = spikes;
                break;
            case 2:
                item = rocks;
                break;
        }
        Instantiate(item, pos, faceDoor);
    }
    GameObject pickMonster(Vector3 pos)
    {
        switch (rollDice(4))
        {
            case 0:
                monster = golem;
                break;
            case 1:
                monster = knight;
                break;
            case 2:
                monster = lizard;
                break;
            case 3:
                monster = skeleton;
                break;
        }
        return Instantiate(monster, pos, faceDoor);
    }

    int rollDice(int options)
    {
        return Random.Range(0, options);
    }

    Vector3 randomPos(Vector3 center)
    {
        Vector3 position = center;

        position.x += Random.Range(-20, 20);
        position.z += Random.Range(-20, 20);

        return position;
    }
}
