using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelItems : Actor {

    public GameObject heartPickup, manaPickUp, bulletPickUp;

    GameObject item;

    Transform trans;

    void Start()
    {
        trans = GetComponent<Transform>();
    }

    int rollDice(int options)
    {
        return Random.Range(0, options);
    }

    Vector3 randomPos(Vector3 center)
    {
        Vector3 position = center;

        position.x += Random.Range(-3, 3);
        position.z += Random.Range(-3, 3);
        position.y--;

        return position;
    }

    void pickProp(Vector3 pos)
    {
        switch (rollDice(3))
        {
            case 0:
                item = heartPickup;
                break;
            case 1:
                item = manaPickUp;
                break;
            case 2:
                item = bulletPickUp;
                break;
        }
        Factory(item, pos, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        Actor OtherActor = other.gameObject.GetComponentInParent<Actor>();
        if (OtherActor && other.tag == "Player")
        {
            pickProp(randomPos(trans.position));
            pickProp(randomPos(trans.position));
            pickProp(randomPos(trans.position));
            Destroy(gameObject);
        }
    }
}
