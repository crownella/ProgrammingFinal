using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * author: Kate Howell
 * 
 * This script will be placed on the house object to control package deliveries.
 * 
 */
public class Platform : MonoBehaviour
{
    public int timeZone;
    

    //To be called when a package is dropped on the platform
    public void PackageDelivered(Package p)
    {
        bool success = p.targetAddress.CheckTimeZone(timeZone);
        p.Delivered(success);
    }


    private void OnCollisionEnter(Collision collision)
    {
        Transform other = collision.transform;
        if (other.CompareTag("Package"))
        {
            PackageDelivered(other.GetComponent<Package>());
        }
    }
}
