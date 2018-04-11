using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDoor : MonoBehaviour {
   
    // HEY
    // SPAWN ME 25.5 AWAY
    // ROTATE ME 90 IF IM LEFT OR RIGHT

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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Vector3 centerOfRoom = Vector3.zero;
            if (trans.rotation.y == 0)
            {
                if (other.transform.position.z > trans.position.z) // player is north of door
                {
                    centerOfRoom = new Vector3(trans.position.x, 0, trans.position.z - 25.5f);
                }
                else if (other.transform.position.z < trans.position.z) // player is south of door
                {
                    centerOfRoom = new Vector3(trans.position.x, 0, trans.position.z + 25.5f);
                }
            }
            else
            {
                if (other.transform.position.x > trans.position.x) // player is right of door
                {
                    centerOfRoom = new Vector3(trans.position.x - 25.5f, 0, trans.position.z);
                }
                else if (other.transform.position.x < trans.position.x) // player is left of door
                {
                    centerOfRoom = new Vector3(trans.position.x + 25.5f, 0, trans.position.z);
                }
            }


            pickMonster(randomPos(centerOfRoom));
            pickMonster(randomPos(centerOfRoom));
            pickMonster(randomPos(centerOfRoom));


            pickProp(randomPos(centerOfRoom));
            pickProp(randomPos(centerOfRoom));

            Destroy(gameObject);


        }
    }
}
