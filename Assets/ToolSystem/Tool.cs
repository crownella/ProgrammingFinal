using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * author: Kate Howell
 * 
 * This class represents an abstract Tool object
 */
public abstract class Tool : MonoBehaviour
{
    //The tool need to have what it does when you click on nothing
    abstract public void Action();
    //the tool needs to have what it does when you click on a package
    abstract public void Action(Package p);
}
