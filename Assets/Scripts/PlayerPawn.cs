using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPawn : PWPawn
{

    Rigidbody rb;
    public float MoveSpeed = 15f;
    public float RotateSpeed = 180f;
    public float MinVelocity = .5f;
    public float MaxVelocity = 20f;

    private float xVelocity;
    private float zVelocity;

    public int Key = 0;

    public Transform ProjectileSpawn;
    public GameObject Projectile1, Projectile2, Camera;
    GameObject currentProjectile;


    public virtual void Start()
    {
        IsSpectator = false;

        // Add and Set up Rigid Body
        rb = gameObject.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.drag = 10f;


        Energy = StartingEnergy;
        Shields = StartingShields;
        currentProjectile = Projectile1;

    }

    protected override bool ProcessDamage(Actor Source, float Value, DamageEventInfo EventInfo, Controller Instigator)
    {
        Shields -= Value;
        LOG(ActorName + " HP: " + Shields);

        if (Shields <= 0)
        {
            controller.RequestSpectate();
            //Destroy(gameObject);

        }

        return base.ProcessDamage(Source, Value, EventInfo, Instigator);

    }

    public override void OnUnPossession()
    {
        IgnoresDamage = true;
    }

    public virtual void FixedUpdate()
    {
        if (rb.velocity.magnitude < MinVelocity)
        {
            rb.velocity = Vector3.zero;
            xVelocity = 0;
            zVelocity = 0;
        }
    }

    public override void Move(float x, float z)
    {
        if(x != 0 || z !=0)
        {
           
            Vector3 Direction = new Vector3(0, 0, 0);
            Direction.x = (gameObject.transform.right.x * x) + (gameObject.transform.forward.x * z);
            Direction.z = (gameObject.transform.right.z * x) + (gameObject.transform.forward.z * z);
            rb.velocity = Direction * MoveSpeed;
        }
    }

    public override void Horizontal(float value)
    {
        if (value != 0)
        {
            xVelocity = value;
            Move(xVelocity, zVelocity);
        }
        else xVelocity = 0;
    }
    public override void Vertical(float value)
    {
        if (value != 0)
        {
            zVelocity = value;
            Move(xVelocity, zVelocity);
        }
        else zVelocity = 0;
    }


    public override void MouseX(float value)
    {
        if (value != 0)
        {
            transform.eulerAngles += new Vector3(0, value * 4, 0);
        }
    }

    public override void MouseY(float value)
    {
        if (value != 0)
        {
            Camera.transform.eulerAngles += new Vector3(-value * 4, 0, 0);
        }
    }

    public override void Fire1(bool value)
    {
        if (value)
        {
            // Fire Projectile
            Factory(currentProjectile, ProjectileSpawn.position, ProjectileSpawn.rotation, controller);
        }
    }

    public override void Fire2(bool value)
    {
        if (value)
        {
            // Set Current Projectile to Prijectile 1
            currentProjectile = Projectile1;
        }
    }

    public override void Fire3(bool value)
    {
        if (value)
        {
            // Set Current Projectile to Prijectile 2
            currentProjectile = Projectile2;
        }
    }
}

