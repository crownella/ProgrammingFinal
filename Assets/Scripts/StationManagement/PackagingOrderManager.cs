using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * author: Kate Howell
 * 
 * This script manages the orders for the packaging station
 * 
 */
public class PackagingOrderManager : MonoBehaviour
{
    bool active, activated, orderActive; //control active logic 

    public EmptyPackageSpawner spawner; //to spawn empty packages when orders are completed;

    //set order values on side
    public Sprite[] numSprites; //0-5
    public SpriteRenderer[] nums; //the nums displays in order {BLOCKS, LEGOS, CARS}

    PackageOrder currentOrder;

    public Toy[] orderToys;

    void Awake()
    {
        DisplayNull();   
    }

    private void Update()
    {
        if (active && !activated) Activate();
        if (!active && activated) Deactivate();

        if (active && activated && !orderActive)
        {
            //give the player a new order
            NewOrder();
        }
    }

    //to be called when the player completes an order
    public void CompleteOrder()
    {
        orderActive = false;
    }

    void NewOrder()
    {
        currentOrder = new PackageOrder(); //makes a random package order
        Display(currentOrder);
        spawner.Spawn();
        orderActive = true;
    }

    //set num displays to the correct num sprite based on the order values
    void Display(PackageOrder order)
    {
        nums[0].sprite = numSprites[order.blocks];
        nums[1].sprite = numSprites[order.legos];
        nums[2].sprite = numSprites[order.cars];
    }

    //set all num displays to zero
    void DisplayNull()
    {
        nums[0].sprite = numSprites[0];
        nums[1].sprite = numSprites[0];
        nums[2].sprite = numSprites[0];
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
        orderActive = false;
    }

    void Deactivate()
    {
        activated = false;
        orderActive = false;
    }

    //check the order with the contents of a package
    public bool CheckOrder(Toy[] contents)
    {
        int blocks = 0;
        int legos = 0;
        int cars = 0;

        foreach (Toy t in contents)
        {
            //its a block
            if (t.toyName == "Block") blocks += 1;
            if (t.toyName == "Car") cars += 1;
            if (t.toyName == "Lego") legos += 1;
        }
        //check the current order
        return (currentOrder.Check(blocks, cars, legos));
    }
}

public class PackageOrder
{
    public int blocks;
    public int legos;
    public int cars;

    //constructor picks random values
    public PackageOrder()
    {
        int[] randoms = { 0, 0, 0 }; //to be filled with random numbers between 0-5

        for(int i = 0; i < randoms.Length; i++)
        {
            //a number 0 -2
            randoms[i] = Random.Range(0, 2);

            //20% to add 1
            int chance = Random.Range(0, 100);  
            if (chance > 80) randoms[i] += 1;

        }

        blocks = randoms[0];
        legos = randoms[1];
        cars = randoms[2];

        if (blocks == 0 && legos == 0 && cars == 0) blocks += 1; //to keep everything from being 0
    }

    //checks if the entered values matches this orders value
    public bool Check(int b, int l, int c)
    {
        return (b == blocks && l == legos && c == cars);
    }
}
