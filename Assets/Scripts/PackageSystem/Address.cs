using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimeZone { Pacific, Mountain, Central, Eastern };

/*
 * author: Kate Howell
 * 
 * this is a class representation of an Address, storing the address lines as a string array, and its timezone
 * An address is a scriptable object, in order to save the data inside. It is then used when making packages.
 */
 [CreateAssetMenu(fileName = "address", menuName = "ScriptableObject/Address", order = 1)]
public class Address : ScriptableObject
{
    //private variables, protected from other classes
    public string[] addressLines;
    public TimeZone timeZone;

    //Constructor - if called from another class
    Address(string[] lines, int time)
    {
        addressLines = lines;
        timeZone = (TimeZone)time;
    }

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

    public string[] getLines()
    {
        return addressLines;
    }

    //to check an address time zone
    public bool CheckTimeZone(int i)
    {
        return (int)timeZone == i;

    }
}
