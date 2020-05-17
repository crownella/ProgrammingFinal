using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *author: Kate Howell
 * 
 * the base class for a toy object
 */
public class Toy : MonoBehaviour
{
    public string toyName;

    bool holding, inPackage;
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (!rb) rb = GetComponentInParent<Rigidbody>();
    }

    //pick up a toy
    //takes its new parent as a parameter
    public void PickUp(GameObject pos)
    {
        if (!holding)
        {
            holding = true;

            //Set rigid body
            this.transform.SetParent(pos.transform);
            rb.useGravity = false;
            rb.mass = 0;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    //drop a toy
    public void Drop()
    {
        if (holding)
        {
            holding = false;

            //set rigid body
            this.transform.SetParent(null);
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints.None;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if a toy collidies with an empty pacakge trigger, and is not already in a package, add it to that package
        if (other.transform.CompareTag("EmptyPackage") && !inPackage)
        {
            //audio trigger!!!

            EmptyPackageManager EPM = other.GetComponent<EmptyPackageManager>();
            EPM.Add(this);
            inPackage = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if a toy leaves an empty pacakge trigger, and is in a package, leave that package
        if (other.transform.CompareTag("EmptyPackage") && inPackage)
        {
            //audio trigger!!!

            EmptyPackageManager EPM = other.GetComponent<EmptyPackageManager>();
            EPM.Remove(this);
            inPackage = false;
        }
    }
}
