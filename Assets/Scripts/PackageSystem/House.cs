using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * author: Kate Howell
 * 
 * This script will be placed on the house object to control package deliveries.
 * 
 */
public class House : MonoBehaviour
{
    public Address houseAddress;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //To be called when a package is dropped in the yard
    public void PackageDelivered(Package p)
    {
        bool success = houseAddress.Equals(p.targetAddress);
        p.Delivered(success);
    }
}
