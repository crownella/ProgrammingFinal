using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyPackageManager : MonoBehaviour
{
    List<Toy> contents = new List<Toy>();

    bool closed;

    public GameObject packagePrefab;

    //add toys to the list
    public void Add(Toy toy)
    {
        contents.Add(toy);
    }

    //remove toys from the list
    public void Remove(Toy toy)
    {
        contents.Remove(toy);
    }

    //replaces this package with a closed package object, destroys this object
    public void Close()
    {
        if (!closed)
        {
            closed = true;

            //spawn a package at the exact spot of this package
            Package pm = Instantiate(packagePrefab, transform.position, transform.rotation).GetComponent<Package>();
            pm.setContents(contents.ToArray());

            foreach (Toy t in contents)
            {
                t.gameObject.SetActive(false); //turn off the toy objects, they will get destroyed later with the package
            }

            Destroy(this.gameObject); //destory the empty package
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (Toy t in contents) print(t.name);
        }
    }

}
