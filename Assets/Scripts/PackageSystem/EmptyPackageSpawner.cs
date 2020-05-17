using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyPackageSpawner : MonoBehaviour
{
    public GameObject emptyPackage;


    public void Spawn()
    {
        Instantiate(emptyPackage, transform.position, transform.rotation);
    }
}
