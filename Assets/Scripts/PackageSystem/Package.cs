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
    bool holding;
    bool rotating;
    bool delivered;
    public float speed = 4f;
    public float rotSpeed = 5f;

    public Address targetAddress;

    Rigidbody rb;
    Renderer render;

    public Material green;
    public Material red;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        render = GetComponent<Renderer>();
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
        if (!holding && !delivered)
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
        if (holding && !delivered)
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

    //this function takes a bool that stores weather it was a successsful delivery or not
    public void Delivered(bool success)
    {
        if (!delivered)
        {
            if (success) SuccessfulDelivery();
            else FailedDelivery();

            delivered = true;
        }
    }

    //if this package was delivered successfully
    void SuccessfulDelivery()
    {
        //change package color
        render.material = green;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().points += 1;
    }

    //if this package failed to deliver
    void FailedDelivery()
    {
        //change package color
        render.material = red;
    }
}
