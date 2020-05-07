using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class Scanner : Tool
{
    public GameObject addressBoxPrefab;
    GameObject addressBox;
    TextMeshProUGUI addressText;
    Transform addressSpawn;
    Address currentAddress;
    bool showingAddress;

    void Awake()
    {
        addressSpawn = GameObject.FindGameObjectWithTag("AddressSpawn").transform;
    }


    public override void Action()
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

        //spawn address box, and get its text componenet
        addressBox = Instantiate(addressBoxPrefab, addressSpawn);
        addressText = addressBox.GetComponentInChildren<TextMeshProUGUI>();

        //build address string
        StringBuilder stringBuilder = new StringBuilder();
        foreach (string s in currentAddress.addressLines)
        {
            stringBuilder.Append(s + "\n");
        }

        //display address text
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
        Destroy(addressBox);
    }

    //if the rool is switched, get rid of the address box
    private void OnDestroy()
    {
        HideAddress();
    }


}
