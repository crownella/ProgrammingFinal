using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * author: Kate Howell
 * 
 * This manages the current tool, and the changing of tools
 */
public class ToolManager : MonoBehaviour
{
    public GameObject[] toolPrefabs; //all tools
    public Transform ToolSpawn; //where to spawn the tool
    public Tool currentTool; //what tool the player is using

    public float scrollSens;
    int currentToolIndex = 0; //0 is no tool

    private void Update()
    {
        //Scroll Wheeel Input
        var d = Input.GetAxis("Mouse ScrollWheel");
        if (d > scrollSens) MoveUp();
        else if (d < -scrollSens) MoveDown();
    }

    //Changes to the tool above this one, with wrapping
    void MoveUp()
    {
        if (currentToolIndex < toolPrefabs.Length) currentToolIndex += 1;
        else  currentToolIndex = 0; 

        ChangeTool(currentToolIndex);
    }

    //Changes the tool to the one below this, with wrapping
    void MoveDown()
    {

        if (currentToolIndex > 0) currentToolIndex -= 1;
        else currentToolIndex = toolPrefabs.Length;

        ChangeTool(currentToolIndex);
    }

    //Changes the tool based on an index
    void ChangeTool(int i)
    {
        //destroy current tool
        if (currentTool != null)
        {
            Destroy(currentTool.gameObject);
            currentTool = null;
        }

        //spawn new tool
        if (i - 1 < toolPrefabs.Length && i > 0)
        {
            currentTool = Instantiate(toolPrefabs[i - 1], ToolSpawn).GetComponent<Tool>();
            currentTool.transform.SetParent(ToolSpawn);
        }
    }
}
