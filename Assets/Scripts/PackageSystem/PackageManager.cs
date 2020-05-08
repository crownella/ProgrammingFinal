using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * author: Kate Howell
 * 
 * This scirpt will spawn and manage all the packages for each level
 */
public class PackageManager : MonoBehaviour
{
    public Transform packageSpawn;

    public GameObject[] packages;

    public Address[] addresses;

    GameObject packagePrefab;

    List<Package> packagesDelivered = new List<Package>();

    // Start is called before the first frame update
    void Start()
    {
        //spawn packages
        if (addresses != null) MakePackages(out packages);
        if(packageSpawn != null) Spawn();
    }

    // Creates an array of package game objects based on the public addresses var
    void MakePackages(out GameObject[] packages)
    {
        List<GameObject> packagesList = new List<GameObject>();
        foreach (Address a in addresses)
        {
            GameObject tmpObject = packagePrefab;
            Package tmpPackage = tmpObject.GetComponent<Package>();
            tmpPackage.targetAddress = a;
            packagesList.Add(tmpObject);
        }

        packages = packagesList.ToArray();
    }

    //instatiates all packages at the package spawn
    void Spawn()
    {
        foreach (GameObject package in packages)
        {
            Package tmpPackage = Instantiate(package, packageSpawn).GetComponent<Package>();
            packagesDelivered.Add(tmpPackage);
        }
    }
}
