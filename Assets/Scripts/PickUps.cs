using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : Actor {

    public bool heart;
    public bool mana;
    public bool bullet;
    public bool key;

    public int healAmount = 10;
    public int manaAmount = 10;
    public int ammoAmount = 10;

    private Transform trans;

    void Start ()
    {
        trans = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        trans.Rotate(0, 4, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerPawn OtherActor = other.gameObject.GetComponentInParent<PlayerPawn>();

        if (OtherActor && other.tag == "Player")
        {
            if(heart)
            {
                OtherActor.TakeDamage(this, -healAmount, new DamageEventInfo(typeof(SpikeDamageType)));
                print("HEAL");
            }
            if(mana)
            {
                OtherActor.Energy += manaAmount;
                print("MANA");
            }
            if(bullet)
            {
                OtherActor.bullets += ammoAmount;
                print("BULLET");
            }
            if (key)
            {
                OtherActor.Key++;
                print("Next Key Acquired");
            }
            Destroy(gameObject);
        }
    }
}
