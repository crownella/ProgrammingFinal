using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingStation : MonoBehaviour
{
    List<GameObject> itemsList = new List<GameObject>();

    int sCubes, mCubes, lCubes, nails;
    int _sCubes, _mCubes, _lCubes, _nails; //tmp versions
    int removeSCubes, removeMCubes, removeLCubes, removeNails;
    int _removeSCubes, _removeMCubes, _removeLCubes, _removeNails; //tmp versions

    public Transform toySpawn;

    //if i had more toys, i wouldnt do this method
    public GameObject carPrefab;
    public GameObject legoPrefab;
    public GameObject blockPrefab;

    public Recipe carRecipe;
    public Recipe legoRecipe;
    public Recipe blockRecipe;

    public float itemSpawnTime;
    bool spawnToy, crafting;
    float spawnToyTimer;
    GameObject toSpawn;

    GameManager gM;

    private void Awake()
    {
        //make recipes
        carRecipe = new Recipe(4, 1, 0, 1, carPrefab);
        legoRecipe = new Recipe(6, 1, 0, 0, legoPrefab);
        blockRecipe = new Recipe(0, 1, 0, 0, blockPrefab);

        gM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void Update()
    {
        //check if spawnning
        if (spawnToy)
        {
            spawnToyTimer += Time.deltaTime;
            if (spawnToyTimer > itemSpawnTime)
            {
                Spawn(toSpawn);
            }
        }

        //check all the current items, always keep the list current
        CheckList();
        
    }

    void Spawn(GameObject g)
    {
        if(g != null)Instantiate(g, toySpawn.position, toySpawn.rotation);
        spawnToy = false;
        toSpawn = null;
        crafting = false;
    }

    //check all the current items matches the item values we have saved
    void CheckList()
    {
        GameObject[] itemArray = itemsList.ToArray();
        //count the items and save it to tmp values
        for(int i = 0; i < itemArray.Length; i++)
        {
            //if the item is null, remove it
            if (itemArray[i] == null) itemsList.Remove(itemArray[i]);

            //check each cubes size var to see what size it is
            else if (itemArray[i].CompareTag("Cube"))
            {
                Cube c = itemArray[i].GetComponent<Cube>();

                if (c.cSize == size.small) _sCubes += 1;
                else if (c.cSize == size.medium) _mCubes += 1;
                else if (c.cSize == size.large) _lCubes += 1;
                else if(c.cSize == size.nails) _nails += 1;
            }
        }

        //reset  values based on tmp values
        if (sCubes != _sCubes) sCubes = _sCubes;
        if (mCubes != _mCubes) mCubes = _mCubes;
        if (lCubes != _lCubes) sCubes = _lCubes;
        if (nails != _nails) nails = _nails;

        //reset tmp values
        _sCubes = 0;
        _mCubes = 0;
        _lCubes = 0;
        _nails = 0;
    }

    public void Add(GameObject g)
    {
        itemsList.Add(g);
    }

    public void Remove(GameObject g)
    {
        itemsList.Remove(g);
    }

    //tries to find a correct recipe, if there is one, it spawns the toy prefab for that recipe
    public void Craft()
    {
        if (crafting) return;

        Recipe[] recipes = {carRecipe, legoRecipe, blockRecipe};

        foreach (Recipe r in recipes)
        {
            if (r.CheckRecipe(sCubes, mCubes, lCubes, nails))
            {
                crafting = true;
                CraftRecipe(r);
                break; //only craft the first recipe found
            }
        }
     }

    //calls crafting functions with recipe varibles
    void CraftRecipe(Recipe recipe)
    {
        DestroyItemsOnList(recipe.sCubes, recipe.mCubes, recipe.lCubes, recipe.nails);
        spawnToy = true;
        toSpawn = recipe.item;
    }


    //Destroy the given number of gameobjects on the items list
    void DestroyItemsOnList(int _sC, int _mC, int _lC, int _n)
    {
        for (int i = 0; i < itemsList.Count; i++)
        {
            if (itemsList[i].CompareTag("Cube"))
            {
                Cube c = itemsList[i].GetComponent<Cube>();

                if (c.cSize == size.small && _sC > 0)
                {
                    Destroy(itemsList[i]);
                    _sC -= 1;
                }
                else if (c.cSize == size.medium && _mC > 0)
                {
                    Destroy(itemsList[i]);
                    _mC -= 1;
                }
                else if (c.cSize == size.large && _lC > 0)
                {
                    Destroy(itemsList[i]);
                    _lC -= 1;
                }
                else if (c.cSize == size.nails && _n > 0)
                {
                    Destroy(itemsList[i]);
                    _n -= 1;
                }
            }
        }
    }
}


//class to hold toy recipes
public class Recipe
{
    //items required to build an object
    public int sCubes, mCubes, lCubes, nails;

    public GameObject item; //object that will be spawnned when recipe is completed

    //constructor
    public Recipe(int _sCubes, int _mCubes, int _lCubes, int _nails, GameObject g)
    {
        sCubes = _sCubes;
        mCubes = _mCubes;
        lCubes = _lCubes;
        nails = _nails;
        item = g;
    }

    //check if the given ints are greater than or equal to whats needed for the recipe
    public bool CheckRecipe(int _sCubes, int _mCubes, int _lCubes, int _nails)
    {
        return _sCubes >= sCubes && _mCubes >= mCubes && _lCubes >= lCubes && _nails >= nails;
    }


}
