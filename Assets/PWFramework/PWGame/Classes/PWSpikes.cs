using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PWSpikes : Actor {
    
	public float DamageAmount = 10.0f;
   
    private void OnTriggerEnter(Collider other)
    {
        Actor OtherActor = other.gameObject.GetComponentInParent<Actor>();
        if (OtherActor && other.tag == "Player")
        {
            OtherActor.TakeDamage(
                this, 
                DamageAmount, 
                new DamageEventInfo(typeof(SpikeDamageType)));
        }
    }


}
