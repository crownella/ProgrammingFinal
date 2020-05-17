using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public int buttonInt;
    Renderer render;

    public Material green, red;

    private void Awake()
    {
        render = GetComponent<Renderer>();

        if (buttonInt == 0) Activate();
    }

    public void Activate()
    {
        render.material = red;
    }

    public void Deactivate()
    {
        render.material = green;
    }
}
