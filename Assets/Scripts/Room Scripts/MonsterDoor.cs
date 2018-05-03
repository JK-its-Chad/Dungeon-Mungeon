using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDoor : Door {

    // HEY
    // SPAWN ME 25.5 AWAY
    // ROTATE ME 90 IF IM LEFT OR RIGHT
    public DoorCollider Side1, Side2;
    public GameObject golem, knight, lizard, skeleton, Turret, FinalTurret;
    public GameObject barrel, spikes, rocks, Lock;
    public Door Arch;

    GameObject item;
    GameObject monster;

    Transform trans;

    Quaternion faceDoor;

	void Start ()
    {
        trans = GetComponent<Transform>();
        KeyAccess = Arch.KeyAccess;
        setColor();
    }

    private void Update()
    {
        if (Side1.doorCollision.tag == "Player" && Side1.doorCollision.GetComponentInParent<PlayerPawn>().Key >= Arch.KeyAccess)
        {
            if (Arch.SecondRoom.GetComponent<Room>().canSpawn)
            {
                if (Arch.SecondRoom.GetComponent<KeyRoom>())
                {
                    if (Arch.SecondRoom.GetComponent<KeyRoom>().KeyAccess + 1 == GameObject.Find("Spawn").GetComponent<MapGenerator>().KeyRooms)
                    {
                        GameObject endGame = new GameObject();
                        BossRoom turrets = endGame.AddComponent<BossRoom>();

                        Vector3 centerOfRoom = Arch.SecondRoom.transform.position;

                        GameObject MiniBoss = Instantiate(Turret, centerOfRoom, Quaternion.identity);
                        turrets.turret1 = MiniBoss;
                        MiniBoss.transform.position = new Vector3(Arch.SecondRoom.transform.position.x + 10, -2, Arch.SecondRoom.transform.position.z + 10);
                        MiniBoss.GetComponent<TurretAI>().Room = Arch.SecondRoom;

                        MiniBoss = Instantiate(Turret, centerOfRoom, Quaternion.identity);
                        turrets.turret2 = MiniBoss;
                        MiniBoss.transform.position = new Vector3(Arch.SecondRoom.transform.position.x + 10, -2, Arch.SecondRoom.transform.position.z - 10);
                        MiniBoss.GetComponent<TurretAI>().Room = Arch.SecondRoom;

                        MiniBoss = Instantiate(Turret, centerOfRoom, Quaternion.identity);
                        turrets.turret3 = MiniBoss;
                        MiniBoss.transform.position = new Vector3(Arch.SecondRoom.transform.position.x - 10, -2, Arch.SecondRoom.transform.position.z - 10);
                        MiniBoss.GetComponent<TurretAI>().Room = Arch.SecondRoom;

                        MiniBoss = Instantiate(Turret, centerOfRoom, Quaternion.identity);
                        turrets.turret4 = MiniBoss;
                        MiniBoss.transform.position = new Vector3(Arch.SecondRoom.transform.position.x - 10, -2, Arch.SecondRoom.transform.position.z + 10);
                        MiniBoss.GetComponent<TurretAI>().Room = Arch.SecondRoom;
                    }
                    else
                    {
                        Vector3 centerOfRoom = Arch.SecondRoom.transform.position;
                        GameObject MiniBoss = Instantiate(Turret, centerOfRoom, Quaternion.identity);
                        MiniBoss.transform.position = new Vector3(Arch.SecondRoom.transform.position.x, -2, Arch.SecondRoom.transform.position.z);
                        MiniBoss.GetComponent<TurretAI>().Room = Arch.SecondRoom;
                    }
                }
                else
                {
                    Vector3 centerOfRoom = Arch.SecondRoom.transform.position;

                    for (int i = 0; i < Arch.KeyAccess + 1; i++)
                    {
                        pickMonster(randomPos(centerOfRoom));
                        pickMonster(randomPos(centerOfRoom));
                    }
                    pickMonster(randomPos(centerOfRoom));

                    pickProp(randomPos(centerOfRoom));
                    pickProp(randomPos(centerOfRoom));
                    pickSpikes(randomPos(centerOfRoom));
                    pickSpikes(randomPos(centerOfRoom));

                    Arch.SecondRoom.GetComponent<Room>().canSpawn = false;
                }
            }
           
            Destroy(gameObject);
        }
        if (Side2.doorCollision.tag == "Player" && Side2.doorCollision.GetComponentInParent<PlayerPawn>().Key >= Arch.KeyAccess)
        {
            if (Arch.FirstRoom.GetComponent<Room>().canSpawn)
            {
                if (Arch.FirstRoom.GetComponent<KeyRoom>())
                {
                    if (Arch.FirstRoom.GetComponent<KeyRoom>().KeyAccess + 1 == GameObject.Find("Spawn").GetComponent<MapGenerator>().KeyRooms)
                    {
                        GameObject endGame = new GameObject();
                        BossRoom turrets = endGame.AddComponent<BossRoom>();

                        Vector3 centerOfRoom = Arch.FirstRoom.transform.position;

                        GameObject MiniBoss = Instantiate(Turret, centerOfRoom, Quaternion.identity);
                        turrets.turret1 = MiniBoss;
                        MiniBoss.transform.position = new Vector3(Arch.FirstRoom.transform.position.x + 10, -2, Arch.FirstRoom.transform.position.z + 10);
                        MiniBoss.GetComponent<TurretAI>().Room = Arch.FirstRoom;

                        MiniBoss = Instantiate(Turret, centerOfRoom, Quaternion.identity);
                        turrets.turret2 = MiniBoss;
                        MiniBoss.transform.position = new Vector3(Arch.FirstRoom.transform.position.x + 10, -2, Arch.FirstRoom.transform.position.z - 10);
                        MiniBoss.GetComponent<TurretAI>().Room = Arch.FirstRoom;

                        MiniBoss = Instantiate(Turret, centerOfRoom, Quaternion.identity);
                        turrets.turret3 = MiniBoss;
                        MiniBoss.transform.position = new Vector3(Arch.FirstRoom.transform.position.x - 10, -2, Arch.FirstRoom.transform.position.z - 10);
                        MiniBoss.GetComponent<TurretAI>().Room = Arch.FirstRoom;

                        MiniBoss = Instantiate(Turret, centerOfRoom, Quaternion.identity);
                        turrets.turret4 = MiniBoss;
                        MiniBoss.transform.position = new Vector3(Arch.FirstRoom.transform.position.x - 10, -2, Arch.FirstRoom.transform.position.z + 10);
                        MiniBoss.GetComponent<TurretAI>().Room = Arch.FirstRoom;
                    }
                    else
                    { 
                        Vector3 centerOfRoom = Arch.FirstRoom.transform.position;
                        GameObject MiniBoss = Instantiate(Turret, centerOfRoom, Quaternion.identity);
                        MiniBoss.transform.position = new Vector3(Arch.FirstRoom.transform.position.x, -2, Arch.FirstRoom.transform.position.z);
                        MiniBoss.GetComponent<TurretAI>().Room = Arch.FirstRoom;
                    }
                }
                else
                {
                    Vector3 centerOfRoom = Arch.FirstRoom.transform.position;

                    for (int i = 0; i < Arch.KeyAccess + 1; i++)
                    {
                        pickMonster(randomPos(centerOfRoom));
                        pickMonster(randomPos(centerOfRoom));
                    }
                    pickMonster(randomPos(centerOfRoom));

                    pickProp(randomPos(centerOfRoom));
                    pickProp(randomPos(centerOfRoom));
                    pickSpikes(randomPos(centerOfRoom));
                    pickSpikes(randomPos(centerOfRoom));

                    Arch.FirstRoom.GetComponent<Room>().canSpawn = false;
                }
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
    void pickSpikes(Vector3 pos)
    {
        item = spikes;
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

    public override void setColor()
    {
        Lock.GetComponent<Renderer>().material = Colors[KeyAccess];
        foreach (Transform child in Lock.transform)
        {
            child.gameObject.GetComponent<Renderer>().material = Colors[KeyAccess];
        }
    }
}
