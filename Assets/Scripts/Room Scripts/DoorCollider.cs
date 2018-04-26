using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollider : MonoBehaviour {

    public Collider doorCollision;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("HELLO");
        Debug.Log(other.tag);
        if(other.tag == "Player")
        {
            doorCollision = other;
        }
    }
}
