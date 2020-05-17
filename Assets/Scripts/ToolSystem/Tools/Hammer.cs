using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Tool
{
    public override void Action()
    {
        //not needed
    }

    public override void Action(Package p)
    {
        //not needed
    }

    public override void Action(GameObject g)
    {
        //if you click on the crafting table with the hammar
        if (g.CompareTag("Craft"))
        {
            CraftingStation craftingStation = g.GetComponent<CraftingStation>();
            craftingStation.Craft();
        }
    }


}
