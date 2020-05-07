using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package : MonoBehaviour
{
    bool holding;
    public float speed = 4f;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
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

            this.transform.SetParent(null);
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints.None;
        }
        
    }
}
