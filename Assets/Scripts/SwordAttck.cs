using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttck : Actor {

    public float DamageAmount = 30.0f;

    void Start()
    {
        Destroy(gameObject, .5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Actor OtherActor = other.gameObject.GetComponentInParent<Actor>();
        if (OtherActor)
        {
            OtherActor.TakeDamage(
                this,
                DamageAmount,
                new DamageEventInfo(typeof(BaseDamageType)));
        }
    }

}
