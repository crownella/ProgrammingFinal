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
                //if you click on a package
                if (Input.GetMouseButton(0))
                {
                    //if you have a tool active, use it
                    if (toolManager.currentTool != null)
                    {
                        toolManager.currentTool.Action(hit.transform.GetComponent<Package>());
                    }
                    //if not, try to pick it up
                    else if (toolManager.currentTool == null)
                    {
                        //if you arent currently holding something
                        if (!gameManager.holding)
                        {
                            gameManager.holding = true;
                            gameManager.currentPackage = hit.transform.GetComponent<Package>();
                            gameManager.currentPackage.PickUp(playerHold);
                        }
                    }
                }
                /*
                if (Input.GetMouseButton(1)) //ROTATING - removed
                {
                    if (gameManager.holding)
                    {
                        gameManager.rotating = true;
                        hit.transform.GetComponent<Package>().Rotate();
                    }
                    else { print("not holding"); }
                }
                */
                else
                {
                    //if (gameManager.rotating) gameManager.rotating = false; //Rotating - removed

                    if (gameManager.holding)
                    {
                        gameManager.holding = false;
                        hit.transform.GetComponent<Package>().Drop();
                    }
                }
            }
            else if(hit.transform.CompareTag("Toy"))
            {
                //pick up toy
                if (Input.GetMouseButton(0))
                {
                    Toy currentToy = hit.transform.GetComponent<Toy>();

                    //no tools can be used on toys
                    if (toolManager.currentTool == null)
                    {
                        if (!gameManager.holding)
                        {
                            gameManager.holding = true;
                            currentToy.PickUp(playerHold);
                        }
                    }
                }
                //drop toys
                else
                {
                    if (gameManager.holding)
                    {
                        gameManager.holding = false;
                        hit.transform.GetComponent<Toy>().Drop();
                    }
                }
            }
            else if (hit.transform.CompareTag("Cube"))
            {
                //pick up cube
                if (Input.GetMouseButton(0))
                {
                    Cube currentCube = hit.transform.GetComponent<Cube>();

                    //no tools can be used on cubes
                    if (toolManager.currentTool == null)
                    {
                        if (!gameManager.holding)
                        {
                            gameManager.holding = true;
                            currentCube.PickUp(playerHold);
                        }
                    }
                }
                //drop cube
                else
                {
                    if (gameManager.holding)
                    {
                        gameManager.holding = false;
                        hit.transform.GetComponent<Cube>().Drop();
                    }
                }
            }
            //controls button input for switching stations
            else if (hit.transform.CompareTag("Button"))
            {
                if (Input.GetMouseButton(0))
                {
                    int currentButton = hit.transform.GetComponent<Button>().buttonInt;

                    if (gameManager.currentStation != (station)currentButton)
                    {
                        gameManager.DeactivateButtons();
                        gameManager.SwitchStation((station)currentButton);
                        gameManager.Activatebuttons(currentButton);
                    }

                }    
            }
            //controls machine button activate when clicking on a button
            else if (hit.transform.CompareTag("MachineButton"))
            {
                if (Input.GetMouseButton(0))
                {
                    hit.transform.GetComponent<MachineButton>().Activate();
                }
            }
            else
            {
                //if you hit something besides a package
                if (Input.GetMouseButton(0))
                {
                    if (toolManager.currentTool != null)
                    {
                        toolManager.currentTool.Action(hit.transform.gameObject);
                    }
                }

            }
        }
        else
        {
            //if you dont hit anything
            if (Input.GetMouseButton(0))
            {
                if (toolManager.currentTool != null)
                {
                    toolManager.currentTool.Action();
                }
            }
        }


    }
}
