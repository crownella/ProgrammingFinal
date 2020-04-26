using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*author: Kate Howell
 * 
 * This script will be placed on the player and control the players movement based on the input
 * 
 * based on youtube tutorial for character controller: https://www.youtube.com/watch?v=_QajrabyTJc
 * 
 */
public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    Vector3 velocity = Vector3.zero;

    public Transform groundCheck;
    public float groundDistance = 3f; //radius of spehre used to check
    public LayerMask groundMask;

    public bool isGrounded;


    // Update is called once per frame
    void Update()
    {
        // X and Z axis
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z; //direction you want to move based on way the player is facing
        controller.Move(move * speed * Time.deltaTime);

        // Y axis 
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) velocity.y = -2.0f; //keeping veloctiy less than 0 constantly forces the player downward, feels better

        

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); //the video didnt explain why this formal works
        }

        velocity.y += gravity;

        controller.Move(velocity * Time.deltaTime); //t^2 is how free fall works

        
    }
}
