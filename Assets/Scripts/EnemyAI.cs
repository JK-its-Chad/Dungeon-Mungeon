using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : PWPawn {

    Transform transRunner;
    Rigidbody rigRunner;
    public GameObject currentTarget;
    public float moveSpeed = 10;
    Vector3 moveDirection;
    public bool hasDynamicDirection = true;
    public bool hasDynamicRotation = true;

    public GameObject heartPickup, manaPickUp, bulletPickUp;

    public int personalSpace = 5;

    float timeLastShot = 0f;
    float delayBetweenShots = 1f;
    public int attackDmg = 10;

    public int Shield = 100;
    public Actor Player;


    void Start()
    {
        transRunner = gameObject.GetComponent<Transform>();
        rigRunner = gameObject.GetComponent<Rigidbody>();
        currentTarget = GameObject.FindGameObjectWithTag("Player");
        Player = currentTarget.GetComponent<PlayerPawn>();

        UpdateMoveDirection();

        switch (rollDice(4))
        {
            case 0:
                moveSpeed++;
                attackDmg--;
                break;
            case 1:
                break;
            case 2:
                moveSpeed--;
                moveSpeed--;
                moveSpeed--;
                attackDmg++;
                attackDmg++;
                attackDmg++;
                break;
            case 3:
                break;
        }
    }

    void Update()
    {
        if (hasDynamicDirection)
        {
            UpdateMoveDirection();
        }

        rigRunner.velocity = (moveDirection * moveSpeed);

        if (isCloseToTarget())
        {
            rigRunner.velocity = Vector3.zero;
            if(Time.time > timeLastShot + delayBetweenShots)
            {
                timeLastShot = Time.time;
                attack();
            }
        }
    }

    protected override bool ProcessDamage(Actor Source, float Value, DamageEventInfo EventInfo, Controller Instigator)
    {
        Shields -= Value;
        LOG(ActorName + " HP: " + Shields);
        
        if(tag == "Lizard")
        {
           
        }

        if (Shields <= 0)
        {
            Vector3 spawnHere = gameObject.transform.position;
            spawnHere.y--;
            pickProp(spawnHere);
            Destroy(gameObject);
        }
        return base.ProcessDamage(Source, Value, EventInfo, Instigator);
    }

    public float getDistanceTo(GameObject other)
    {
        Vector3 runnerPosition = transRunner.position;

        Transform transOther = other.GetComponent<Transform>();
        Vector3 otherPosition = transOther.position;

        Vector3 distanceVec = otherPosition - runnerPosition;
        float distance = distanceVec.magnitude;

        return distance;
    }

    public bool isCloseToTarget()
    {
        bool isClose = false;
        if (getDistanceTo(currentTarget) < ((moveSpeed * Time.deltaTime) + personalSpace))
        {
            isClose = true;
        }
        // removed else statement because we started with a default value of true
        // so only when the if statement is run, we assign is close to true!

        return isClose;
    }


    public void UpdateMoveDirection()
    {
        if (currentTarget)
        {
            Vector3 runnerPosition = transRunner.position;

            Transform transOther = currentTarget.GetComponent<Transform>();
            Vector3 otherPosition = transOther.position;

            Vector3 distanceVec = otherPosition - runnerPosition;
            moveDirection = distanceVec.normalized;

            if (hasDynamicRotation)
            {
                float angle = Vector3.SignedAngle(Vector3.back, moveDirection, Vector3.up);

                transRunner.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
            }
        }
        else moveDirection = Vector3.zero;
        
    }

    void attack()
    {
        Player.TakeDamage(this, attackDmg, new DamageEventInfo(typeof(BaseDamageType)), Owner);
    }

    void pickProp(Vector3 pos)
    {
        GameObject item = heartPickup; ;
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

    int rollDice(int options)
    {
        return Random.Range(0, options);
    }
}
