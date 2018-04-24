using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollider : MonoBehaviour {

    public Collider doorCollision;

    private void OnTriggerEnter(Collider other)
    {
        doorCollision = other;
    }
}
