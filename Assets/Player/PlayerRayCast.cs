using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is attacthed to the camera to cast a ray for interacting with objects
 */
public class PlayerRayCast : MonoBehaviour
{
    [SerializeField]
    private int rayLength = 20;

    public GameObject playerHold;

    GameManager gameManager;

    ToolManager toolManager;



    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        toolManager = GameObject.FindGameObjectWithTag("ToolManager").GetComponent<ToolManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray MouseRay = new Ray(this.transform.position, this.transform.forward);
        Debug.DrawRay(this.transform.position, this.transform.forward, Color.green);
        RaycastHit hit;

        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit) && hit.distance < rayLength)
        {


            if (hit.transform.CompareTag("Package"))
            {
                if (Input.GetMouseButton(0))
                {
                    if (toolManager.currentTool != null)
                    {
                        toolManager.currentTool.Action(hit.transform.GetComponent<Package>());
                    }
                    else if (toolManager.currentTool == null)
                    {
                        if (!gameManager.holding)
                        {
                            gameManager.holding = true;
                            gameManager.currentPackage = hit.transform.GetComponent<Package>();
                            gameManager.currentPackage.PickUp(playerHold);
                        }
                    }
                }

                if (Input.GetMouseButton(1))
                {
                    if (gameManager.holding)
                    {
                        print("rotate");
                        gameManager.rotating = true;
                        hit.transform.GetComponent<Package>().Rotate();
                    }
                    else { print("not holding"); }
                }

                if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
                {
                    if (gameManager.rotating) gameManager.rotating = false;

                    if (gameManager.holding)
                    {
                        gameManager.holding = false;
                        hit.transform.GetComponent<Package>().Drop();
                    }
                }
            }
            else
            {
                if (toolManager.currentTool != null)
                {
                    toolManager.currentTool.Action();
                }
            }

            
        }


    }
}
