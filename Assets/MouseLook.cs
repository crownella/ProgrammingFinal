using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*author: Kate Howell
 * 
 * This script will be placed on the camera to contol the camera rotation based on the mouse input
 * 
 * based on youtube tutorial for character controller: https://www.youtube.com/watch?v=_QajrabyTJc
 * 
 */
public class MouseLook : MonoBehaviour
{
    public float mouseSens = 100f;

    public Transform playerBody;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSens;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens;

        xRotation -= mouseY; //rotate camera by mouse y
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //clamps look area

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //rotates camera up and down
        playerBody.Rotate(Vector3.up * mouseX); //mouse x controls rotation on y axis

    }
}
