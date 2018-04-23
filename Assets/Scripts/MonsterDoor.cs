using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDoor : Door {

    // HEY
    // SPAWN ME 25.5 AWAY
    // ROTATE ME 90 IF IM LEFT OR RIGHT

    public Door Arch;
    public DoorCollider Side1, Side2;
    public GameObject golem, knight, lizard, skeleton;
    public GameObject barrel, spikes, rocks;

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
        if(Side1.doorCollision.tag == "Player" && Side1.doorCollision.GetComponent<PlayerPawn>().Key >= Arch.KeyAccess)
        {
            Vector3 centerOfRoom = Arch.SecondRoom.transform.position;

            pickMonster(randomPos(centerOfRoom));
            pickMonster(randomPos(centerOfRoom));
            pickMonster(randomPos(centerOfRoom));

            pickProp(randomPos(centerOfRoom));
            pickProp(randomPos(centerOfRoom));

            Destroy(gameObject);
        }
        if (Side2.doorCollision.tag == "Player" && Side2.doorCollision.GetComponent<PlayerPawn>().Key >= Arch.KeyAccess)
        {
            Vector3 centerOfRoom = Arch.FirstRoom.transform.position;

            pickMonster(randomPos(centerOfRoom));
            pickMonster(randomPos(centerOfRoom));
            pickMonster(randomPos(centerOfRoom));

            pickProp(randomPos(centerOfRoom));
            pickProp(randomPos(centerOfRoom));

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
    void pickMonster(Vector3 pos)
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
        Instantiate(monster, pos, faceDoor);
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
