using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMan : MonoBehaviour {

    public float movementSpeed = 10;
    Transform trans;
    Rigidbody rig;
    bool forward = false;
    bool back = false;
    bool left = false;
    bool right = false;

    public enum RotationAxes { MouseXandY = 0, MouseX = 1, MouseY = 2 }
    RotationAxes axes = RotationAxes.MouseXandY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    float minimumX = -360F;
    float maximumX = 360F;
    float minimumY = -60F;
    float maximumY = 60F;
    float rotationX = 0F;
    float rotationY = 0F;
    Quaternion originalRotation;


    void Start()
    {
        trans = gameObject.GetComponent<Transform>();
        rig = gameObject.GetComponent<Rigidbody>();
        originalRotation = transform.localRotation;
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

    void Update()
    {
        if (axes == RotationAxes.MouseXandY)
        {
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            rotationY = ClampAngle(rotationY, minimumY, maximumY);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
            transform.localRotation = originalRotation * xQuaternion * yQuaternion;
        }
        else if (axes == RotationAxes.MouseX)
        {
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation = originalRotation * xQuaternion;
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = ClampAngle(rotationY, minimumY, maximumY);
            Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
            transform.localRotation = originalRotation * yQuaternion;
        }

    }

    void FixedUpdate()
    {
        forward = Input.GetKey(KeyCode.W);
        back = Input.GetKey(KeyCode.S);
        left = Input.GetKey(KeyCode.A);
        right = Input.GetKey(KeyCode.D);

        rig.velocity = Vector3.zero;

        if (forward)
        {
            rig.velocity += (trans.forward * movementSpeed);
        }
        if (back)
        {
            rig.velocity += -1 * (trans.forward * movementSpeed);
        }
        if (left)
        {
            rig.velocity += -1 * (trans.right * movementSpeed);
        }
        if (right)
        {
            rig.velocity += (trans.right * movementSpeed);
        }
    }
}
