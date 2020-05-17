using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*author: Kate Howell
 * 
 * This script controls a machine button that can be pressed to make an object spawn
 */
public class MachineButton : MonoBehaviour
{

    public GameObject prefabToSpawn;
    public Transform SpawnLocation;
    public float delay;

    bool activated;
    float timer;

    void Update()
    {
        //activation delay
        if (activated)
        {
            timer += Time.deltaTime;
            if (timer > delay)
            {
                timer = 0;
                activated = false;
            }
        }
    }

    //called from player raycast
    public void Activate()
    {
        if (activated) return;

        Instantiate(prefabToSpawn, SpawnLocation.position, SpawnLocation.rotation);
        activated = true;
    }
}
