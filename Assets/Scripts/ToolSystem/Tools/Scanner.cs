using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class Scanner : Tool
{

    GameObject addressBox;
    TextMeshProUGUI addressText;
    Address currentAddress;
    bool showingAddress;

    void Awake()
    {
        addressBox = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().addressBox;
        addressText = addressBox.GetComponentInChildren<TextMeshProUGUI>();
    }


    public override void Action()
    {
        //nothing happens, but this needs to exist

        /* this might seem like a weird thing to do,
         * but i want to possiblity to add things here later,
         * and be able to call these functions, knowing they exist
         */
    }
    public override void Action(GameObject g)
    {
        //nothing happens, but this needs to exist
    }

    public override void Action(Package p)
    {
        currentAddress = p.targetAddress;
        ShowAddress();
    }

    //Show address UI
    void ShowAddress()
    {
        showingAddress = true;

        //build address string
        StringBuilder stringBuilder = new StringBuilder();

        string[] addressLines = currentAddress.getLines();
        foreach (string s in addressLines)
        {
            stringBuilder.Append(s + "\n");
        }

        //display address text
        addressBox.SetActive(true);
        addressText.SetText(stringBuilder.ToString());

        StartCoroutine(ShowAddressTimer());
        showingAddress = false;
        
    }

    //Timer for address display
    IEnumerator ShowAddressTimer()
    {
        yield return new WaitForSeconds(3f);
        if (!showingAddress) HideAddress();
    }

    //hide address UI
    void HideAddress()
    {
        addressBox.SetActive(false);
    }

    //if the rool is switched, get rid of the address box
    private void OnDestroy()
    {
        HideAddress();
    }


}
