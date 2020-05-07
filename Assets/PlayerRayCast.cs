using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is attacthed to the camera to cast a ray for interacting with objects
 */
public class PlayerRayCast : MonoBehaviour
{
    [SerializeField]
    private int rayLength = 20;

    public GameObject playerHold;

    bool holdingPackage;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Ray MouseRay = new Ray(this.transform.position, this.transform.forward);
        Debug.DrawRay(this.transform.position, this.transform.forward, Color.green);
        RaycastHit hit;

        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit) && hit.distance < rayLength)
        {

            if (hit.transform.CompareTag("Package"))
            {
                if (Input.GetMouseButton(0))
                {
                    holdingPackage = true;
                    hit.transform.GetComponent<Package>().PickUp(playerHold);
                }
                else
                {
                    if (holdingPackage)
                    {
                        holdingPackage = false;
                        hit.transform.GetComponent<Package>().Drop();
                    }
                }
            }

            
        }


    }
}
