using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*author: Kate Howell
 * 
 * Controls the cube cutter, which spawns a smaller cube
 * 
 * 
 */
public class CubeCutter : MonoBehaviour
{

    public Transform[] spawns;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            GameObject toSpawn = other.GetComponent<Cube>().SizeDown();
            Destroy(other.gameObject);
            foreach (Transform spawn in spawns)
            {
                Instantiate(toSpawn, spawn.position, spawn.rotation);
            }
            
        }
    }
}
