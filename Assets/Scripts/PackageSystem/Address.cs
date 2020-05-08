using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * author: Kate Howell
 * 
 * this is a class representation of an Address, storing the address lines as a string array
 */
 [CreateAssetMenu(fileName = "address", menuName = "ScritableObjects/Address", order = 1)]
public class Address : ScriptableObject
{
    public string[] addressLines;

    //to compare two Address objects
    public bool Equals(Address a)
    {
        //evaulate their address lines
        if (addressLines.Length != a.addressLines.Length) return false;
        for (int i = 0; i < addressLines.Length; i++)
        {
            if (addressLines[i] != a.addressLines[i]) return false;
        }
        return true;
    }
}
