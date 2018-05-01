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

    private float minimumX = -360F;
    private float maximumX = 360F;
    private float minimumY = -60F;
    private float maximumY = 60F;
    private float rotationX = 0f;
    private float rotationY = 0f;
    public float sensitivityX = 3f;
    public float sensitivityY = 3f;
    private Quaternion originalRotation;
    private Quaternion xQuaternion;
    private Quaternion yQuaternion;

    public int Key = 0;

    public Transform ProjectileSpawn, MagicSpawn;
    public GameObject Projectile1, Projectile2, Camera;
    GameObject currentProjectile;

    int bulletdmg = 25;
    public GameObject SwordThing;
    public GameObject SwordTrigger;
    bool SwordSwing = false;

    private float coolDown1 = 0f;
    private float coolDown2 = 0f;
    private float coolDown3 = 0f;


    public virtual void Start()
    {
        IsSpectator = false;

        // Add and Set up Rigid Body
        rb = gameObject.GetComponent<Rigidbody>();

        originalRotation = Camera.transform.localRotation;
        xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
        yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.up); ;


        Energy = StartingEnergy * 2;
        Shields = StartingShields;
        bullets = 30;
        currentProjectile = Projectile1;

        //GunThing.SetActive(true);
        SwordThing.SetActive(false);
        SwordTrigger.SetActive(false);

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
        if(Shields > StartingShields * 2)
        {
            Shields = StartingShields * 2;
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


        if(Energy < StartingEnergy && Time.time > coolDown3)
        {
            Energy++;
            coolDown3 = Time.time + 1f;
        }
        else if (Energy > StartingEnergy * 2)
        {
            Energy--;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle <= -360F)
        {
            angle += 360F;
        }
        if (angle >= 360F)
        {
            angle -= 360F;
        }
        return Mathf.Clamp(angle, min, max);
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
            //transform.eulerAngles += new Vector3(0, value * 4, 0);
            rotationX += value * sensitivityX;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            gameObject.transform.localRotation = originalRotation * xQuaternion;
        }
    }

    public override void MouseY(float value)
    {
        if (value != 0)
        {
            //Camera.transform.eulerAngles += new Vector3(-value * 4, 0, 0);
            rotationY += value * sensitivityY;
            rotationY = ClampAngle(rotationY, minimumY, maximumY);
            yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
            Camera.transform.localRotation = originalRotation * yQuaternion;
        }
    }

    public override void Trigger1(float value)
    {
        if (value != 0 && Energy >= 30 && Time.time > coolDown1)
        {
            Energy -= 30;
            coolDown1 = Time.time + 1f;
            Factory(currentProjectile, MagicSpawn.position, MagicSpawn.rotation, controller);
        }
    }

    public override void Trigger2(float value)
    {
        if (value != 0)
        {
            if (!SwordSwing && bullets > 0 && Time.time > coolDown2)
            {
                bullets--;
                coolDown2 = Time.time + .33f;
                RaycastHit hit;
                if (Physics.Raycast(ProjectileSpawn.position, ProjectileSpawn.forward, out hit))
                {
                    Actor monster = hit.collider.GetComponent<Actor>();
                    if (monster)
                    {
                        monster.TakeDamage(this, bulletdmg, new DamageEventInfo(typeof(ProjectileDamageType)), Owner);
                    }
                }
                print("pew");
            }
            else if(SwordSwing && Time.time > coolDown2)
            {
                coolDown2 = Time.time + .5f;
                SwordTrigger.SetActive(true);
                print("swing");
            }
        }
    }

    public override void Fire1(bool value)
    {
        if(value)
        {
            if(SwordSwing)
            {
                //GunThing.SetActive(true);
                SwordThing.SetActive(false);
                SwordSwing = false;
            }
            else
            {
                //GunThing.SetActive(false);
                SwordThing.SetActive(true);
                SwordSwing = true;
            }
        }
    }

    public override void Fire2(bool value)
    {
        if (value)
        {

        }
    }

    public override void Fire3(bool value)
    {
        if (value)
        {
            if(currentProjectile == Projectile1)
            {
                currentProjectile = Projectile2;
            }
            else if (currentProjectile == Projectile2)
            {
                currentProjectile = Projectile1;
            }
        }
    }
}

