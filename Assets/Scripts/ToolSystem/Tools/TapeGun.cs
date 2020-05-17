using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapeGun : Tool
{


    public override void Action()
    {
        //nothing happens but needs to exsit
    }

    public override void Action(Package p)
    {
        //nothing happens but needs to exsit
    }

    public override void Action(GameObject g)
    {
        if (g.CompareTag("EmptyPackage"))
        {
            EmptyPackageManager emptyPackage = g.GetComponent<EmptyPackageManager>();
            emptyPackage.Close();
        }
    }
}
