using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * author: Kate Howell
 * 
 * This script is on the trigger for a completed toy
 * 
 */

public class CraftTrigger : MonoBehaviour
{
    public CraftOrderManager orderManager;

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
        //if its a toy
        if (other.CompareTag("Toy"))
        {
            //get the package
            Toy t = other.GetComponent<Toy>();
            if (t == null) return;

            if (orderManager.CheckItem(t))
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
