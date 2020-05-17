using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * author: Kate Howell
 * 
 * This class spawns packages, at a random interval from a given range, at the given transform
 * 
 */
public class PackageSpawner : MonoBehaviour
{
    [Tooltip("Where packages will spawn")]
    public Transform PackageSpawn;

    [Tooltip("All possible Address Objects to be assigned to packages")]
    public Address[] allAddressObjects;

    [Tooltip("Time between package spawns range, (min,max)")]
    public Vector2 IntervalRange;

    [Tooltip("Prefab to be spawned")]
    public GameObject packagePrefab;

    //timer logic
    float previousTime;
    float cInterval;

    bool active;

    //dont use the same address twice, if possible. Saves the index of used addresses
    List<int> addressUsed = new List<int>(); 

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            //this will cause a delay when the station is set active equal to cInterval
            previousTime = Time.time;
            return;
        }

        //if the time since the last spawn is greater than the current interval
        if (Time.time - previousTime > cInterval)
        {
            //Spawn
            Spawn();

            //Adjust time logic
            previousTime = Time.time;
            cInterval = SetInterval();
        }
    }

    public void SetActive(bool value)
    {
        active = value;
    }

    //Call to spawn a package
    void Spawn()
    {
        //spawn the prefab at the spawn location
        Package p = Instantiate(packagePrefab, PackageSpawn.position, PackageSpawn.rotation).GetComponent<Package>();
        SetAddress(p);
    }

    //Set the address of a given package
    void SetAddress(Package p)
    {
        bool foundAddress = false;
        int randomIndex = 0;

        //find a index we have not already used, makes it a random order each time
        while (!foundAddress)
        {
            //pick a random index
            int random = Random.Range(0, allAddressObjects.Length);

            //check if its been used, if not, we found a valid index
            if (!addressUsed.Contains(random)) foundAddress = true;

            //if we have used all the possible indexes
            if (addressUsed.Count == allAddressObjects.Length)
            {
                //reset the list
                addressUsed = new List<int>();
                foundAddress = true;
            }

            //use this index
            randomIndex = random;
            addressUsed.Add(random);
        }

        //set the address of the given package
        Address tmpAddress = allAddressObjects[randomIndex];
        p.SetAddress(tmpAddress);
    }

    //returns a random value in the interval range
    float SetInterval()
    {
        return Random.Range(IntervalRange.x, IntervalRange.y);
    }
}
