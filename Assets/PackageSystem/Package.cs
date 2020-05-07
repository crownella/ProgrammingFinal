using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package : MonoBehaviour
{ 
    bool holding;
    bool rotating;
    public float speed = 4f;
    public float rotSpeed = 5f;

    public Address targetAddress;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (rotating)
        {
            float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
            float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

            transform.Rotate(Vector3.up, -rotX);
            transform.Rotate(Vector3.down, -rotX);
            transform.Rotate(Vector3.left, rotY);
            transform.Rotate(Vector3.right, -rotY);
        }
    }
    public void PickUp(GameObject pos)
    {
        if (!holding)
        {
            print("pick up");
            holding = true;

            this.transform.SetParent(pos.transform);
            rb.useGravity = false;
            rb.mass = 0;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    public void Drop()
    {
        if (holding)
        {
            print("drop");
            holding = false;
            rotating = false;

            this.transform.SetParent(null);
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints.None;
        }
        
    }

    public void Rotate()
    {
        rotating = true;
    }
}
