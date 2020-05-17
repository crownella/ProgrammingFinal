using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//cubes can also be nails to save time
public enum size { small, medium, large, nails };

public class Cube : MonoBehaviour
{
    public size cSize; //current Size

    public GameObject[] cubePrefabs; //cube prefabs, sorted by size

    //for picking up
    bool holding, onTable;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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

    private void OnCollisionEnter(Collision collision)
    {
        //if u collide with the crafting table, and ur not on the table
        if (collision.transform.CompareTag("Craft") && !onTable)
        {
            CraftingStation cS = collision.transform.GetComponent<CraftingStation>();
            cS.Add(this.gameObject);
            onTable = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //if u exit  the crafting table, and  on the table
        if (collision.transform.CompareTag("Craft") && onTable)
        {
            CraftingStation cS = collision.transform.GetComponent<CraftingStation>();
            cS.Remove(this.gameObject);
            onTable = false;
        }
    }

    //returns the cube prefab a size down from the current size, if possible
    public GameObject SizeDown()
    {
        if ((int)cSize == 0)
        {
            return cubePrefabs[0];
        }
        else
        {
            return cubePrefabs[(int)cSize - 1];
        } 
    }
}
