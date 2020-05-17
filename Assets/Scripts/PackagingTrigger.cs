using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * author: Kate Howell
 * 
 * This script is on the trigger for a completed package
 * 
 */
public class PackagingTrigger : MonoBehaviour
{
    public PackagingOrderManager orderManager;

    public Renderer indicator;

    public Material good;
    public Material bad;
    public Material nuetral;

    public void Awake()
    {
        indicator.material = nuetral;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if its a package
        if (other.CompareTag("Package"))
        {
            //get the package
            Package p = other.GetComponent<Package>();

            Toy[] content = p.getContents();

            if (content == null) return;

            //if the package contents match the current order, complete the order
            if (orderManager.CheckOrder(content))
            {
                orderManager.CompleteOrder();
                indicator.material = good;
            }
            else
            {
                indicator.material = bad;
            }

            StartCoroutine(GoBack());
            Destroy(other.gameObject);

        }
    }

    //reset indicator to nuetral
    IEnumerator GoBack()
    {
        yield return new WaitForSeconds(3f);
        indicator.material = nuetral;
    }

}
