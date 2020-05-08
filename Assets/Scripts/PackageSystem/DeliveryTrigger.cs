using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * author: Kate Howell
 * 
 * This script is attached to a collider trigger and activates package deliveries
 */
public class DeliveryTrigger : MonoBehaviour
{
    public House attachtedHouse;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Package"))
        {
            attachtedHouse.PackageDelivered(other.GetComponent<Package>());
        }
    }
}
