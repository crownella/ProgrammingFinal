using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * author: Kate Howell
 * 
 * this script will be be attatched to a package object and control interaction and delivery
 */
public class Package : MonoBehaviour
{ 
    bool holding, rotating, delivered, success;

    Toy[] contents; //the toy contents of a package
    
    //public float rotSpeed = 5f;

    public Address targetAddress;

    Rigidbody rb;
    Renderer render;

    public Material green, red;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        render = GetComponent<Renderer>();
    }

    private void Update()
    {
        /* Couldnt get this working, supposed to rotate when u hold down right button but its rotates on the wrong axis
        if (rotating)
        {
            float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
            float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

            transform.Rotate(Vector3.up, -rotX);
            transform.Rotate(Vector3.down, -rotX);
            transform.Rotate(Vector3.left, rotY);
            transform.Rotate(Vector3.right, -rotY);
        }
        */
    }

    //pick up a package
    public void PickUp(GameObject pos)
    {
        if (!holding && !delivered)
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

    //drop a pacakge
    public void Drop()
    {
        if (holding && !delivered)
        {
            holding = false;
            //rotating = false; - not used

            //set rigid body
            this.transform.SetParent(null);
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints.None;
        }
        
    }

    public void SetAddress(Address a)
    {
        targetAddress = a;
    }

    public void Rotate()
    {
        rotating = true;
    }

    //this function takes a bool that stores whether it was a successsful delivery or not
    public void Delivered(bool success)
    {
        if (!delivered)
        {
            if (success) SuccessfulDelivery();
            else FailedDelivery();

            delivered = true; //makes the package not be able to be picked up
        }
    }

    //if this package was delivered successfully
    void SuccessfulDelivery()
    {
        //change package color
        render.material = green;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().points += 1;
        StartCoroutine(Deactivate());
    }

    //if this package failed to deliver
    void FailedDelivery()
    {
        //change package color
        render.material = red;
        StartCoroutine(Deactivate());
    }

    //deactivates teh package once its delivered
    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(1f);

        //add this to the gM to save the delivery details
        GameManager gM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        gM.Add(this);
        gameObject.SetActive(false);
    }

    public void setContents(Toy[] toys)
    {
        contents = toys;
    }

    public Toy[] getContents()
    {
        return contents;
    }

    //When a package is destroyed, destroy the toys too
    private void OnDestroy()
    {
        if (contents != null)
        {
            foreach (Toy t in contents)
            {
                if(t != null) Destroy(t.gameObject);
            }
        }
    }
}
