using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum station { crafting, packaging, sorting }

/*
 * Author: Kate Howell
 * 
 * This class will control the big picture game logic
 */
public class GameManager : MonoBehaviour
{
    public bool holding;
    public bool rotating;
    public station currentStation;
    public Button[] buttons;

    public Animator[] doors; //the 3 doors for the stations

    public PackageSpawner sortingSpawner;
    public CraftOrderManager craftOrders;
    public PackagingOrderManager packagingOrders;

    List<Package> deliveredPackages = new List<Package>();

    int _points;
    public int points
    {
        get
        {
            return _points;
        }
        set
        {
            _points = value;
            print(_points);
        }
    }

    public float mouseSens = 100f;

    public Package currentPackage;

    public GameObject addressBox;


    public void Awake()
    {
        SwitchStation(station.crafting);
    }

    public void Update()
    {
        //escape game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    //add a package to the delivered packages list
    public void Add(Package p)
    {
        deliveredPackages.Add(p);
    }

    //switch current station to the new station
    public void SwitchStation(station newStation)
    {
        print(newStation.ToString());
        currentStation = newStation;

        switch (newStation)
        {
            case station.crafting: Crafting();
                break;
            case station.packaging: Packaging();
                break;
            case station.sorting: Sorting();
                break;
        }
    }

    //set station logic for crafting
    void Crafting()
    {
        craftOrders.SetActive(true);
        sortingSpawner.SetActive(false);
        packagingOrders.SetActive(false);
        OpenDoor(0);
    }

    //set station logic for packaging
    void Packaging()
    {
        craftOrders.SetActive(false);
        sortingSpawner.SetActive(false);
        packagingOrders.SetActive(true);
        OpenDoor(1);
    }

    //set station logic for sorting
    void Sorting()
    {
        craftOrders.SetActive(false);
        sortingSpawner.SetActive(true);
        packagingOrders.SetActive(false);
        OpenDoor(2);
    }

    //deactivate selection buttons
    public void DeactivateButtons()
    {
        foreach (Button b in buttons) b.Deactivate();
    }

    //activate the button at index i
    public void Activatebuttons(int i)
    {
        buttons[i].Activate();
    }

    void OpenDoor(int i)
    {
        CloseAllDoors();
        doors[i].SetBool("Open", true);
    }

    void CloseAllDoors()
    {
        foreach (Animator door in doors)
        {
            door.SetBool("Open", false);
        }
    }
}
