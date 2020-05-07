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
}
