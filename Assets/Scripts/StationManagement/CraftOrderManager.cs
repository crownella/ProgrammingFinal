using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * author: Kate Howell
 * 
 * This script manages the orders for the crafting station
 * 
 */
public class CraftOrderManager : MonoBehaviour
{
    bool active; //if this station is active
    bool activated; //if the station was activated;
    bool orderActive; //if the player currently has an order;

    CraftOrder[] orders; //all the possible orders
    CraftOrder currentOrder; //the order that is active
    List<int> usedOrders = new List<int>(); //orders that have already been used 

    public GameObject[] craftOrders; //objects that can be crafted

    public Renderer[] lights;

    public Material off;
    public Material on;

    

    void Start()
    {
        if(craftOrders != null) GenerateCraftOrders();
        TurnOffButtons();
    }

    void Update()
    {
        if (active && !activated) Activate();
        if (!active && activated) Deactivate();

        if (active && activated && !orderActive)
        {
            //give the player a new order
            NewOrder();
        }
    }

    //changes the current order
    void NewOrder()
    {
        bool foundOrder = false;
        int orderIndex = 0;
        while (!foundOrder)
        {
            //pick a random index
            int random = Random.Range(0, orders.Length);

            //check if that index is used         
            if (!usedOrders.Contains(random))
            {
                //if its not used, you found an order
                foundOrder = true;
                orderIndex = random;
                usedOrders.Add(random);
            }
            //if you have used all the possible indexes
            if (usedOrders.Count == orders.Length)
            {
                //reset list
                usedOrders = new List<int>();

                foundOrder = true;
                orderIndex = random;
                usedOrders.Add(random);
            }
        }

        //set current order
        currentOrder = orders[orderIndex];
        DisplayOrder(orderIndex);
        orderActive = true;
    }

    //turns on the button for the corresponing order
    void DisplayOrder(int index)
    {
        TurnOffButtons();

        lights[index].material = on;
    }

    //turns off all the buttons
    void TurnOffButtons()
    {
        foreach(Renderer light in lights) light.material = off;
    }

    //to be called when the player completes an order
    public void CompleteOrder()
    {
        orderActive = false;
    }

    //generates craft orders of the given input
    void GenerateCraftOrders()
    {
        List<CraftOrder> _orders = new List<CraftOrder>();
        foreach (GameObject g in craftOrders)
        {
            _orders.Add(new CraftOrder(g));
        }
        orders = _orders.ToArray();
    }

    //to be called by game manager when station is activated / deavtivated
    public void SetActive(bool value)
    {
        active = value;
    }

    //i thought i would have to add more to this?
    void Activate()
    {
        activated = true;
    }

    void Deactivate()
    {
        activated = false;
    }

    //check the name of the finished toy 
    public bool CheckItem(Toy t)
    {
        Toy otherToy = currentOrder.OrderObject.GetComponent<Toy>();
        return (t.toyName.Equals(otherToy.toyName));
    }
}

//an isntrance on an order
public class CraftOrder
{
    public GameObject OrderObject;

    //constuctor
    public CraftOrder(GameObject g)
    {
        OrderObject = g;
    }
}
