using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpaceOrder : MonoBehaviour
{

    private Dictionary<string, int> orderMap;

    //bool false = minus, bool true = plus
    private Dictionary<string, bool> posNegMap;

    private string[] allSideNames;

    private int spaceGreenCounter;
    private int spaceOrangeCounter;
    private int spaceBlueCounter;

    // Start is called before the first frame update
    void Start()
    {
        orderMap = new Dictionary<string, int>();
        allSideNames = new string[6];
        allSideNames[0] = "Top";
        allSideNames[1] = "TopRight";
        allSideNames[2] = "Right";
        allSideNames[3] = "Bottom";
        allSideNames[4] = "BottomLeft";
        allSideNames[5] = "Left";


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addTypeToOrder(string name, int type)
    {
        if (!orderMap.ContainsKey(name))
        {
            if (type  == 3)
            {
               // generatePosNegMap(name);
            }
            orderMap.Add(name, type);
            Debug.Log("Add:" + name + type);
        }
    }
    private void generatePosNegMap(string name) 
    {
        int shift = 0;
        switch(name)
        {
            case "Top":
                shift = 2;
                break;
            case "TopRight":
                shift = 3;
                break;
            case "Right":
                shift = 4;
                break;
            case "Bottom":
                shift = 5;
                break;
            case "BottomLeft":
                shift = 6;
                break;
            case "Left":
                shift = 7;
                break;
        }
        for(int i = 0; i < allSideNames.Length; i++)
        {
            if (i < 3) 
            {
                posNegMap.Add(allSideNames[(i + shift) % 6], false);
            }
            else
            {
                posNegMap.Add(allSideNames[(i + shift) % 6], true);
            }
        }
    }

    public void removeTypeInOrder(string name, int type)
    {
        if (type == 1 && spaceGreenCounter == 0)
        {
            orderMap.Remove(name);
            Debug.Log("Remove:" + name + type);
        }
        else if (type == 2 && spaceOrangeCounter == 0) 
        { 
            orderMap.Remove(name);
            Debug.Log("Remove:" + name + type);
        }
        else if(type == 3 && spaceBlueCounter == 0)
        {
            orderMap.Remove(name);
            Debug.Log("Remove:" + name + type);
        }
    }

    public bool isRightPlace(string name, int type)
    {
        if(orderMap.ContainsValue(type) && !orderMap.ContainsKey(name))
        {
            return false;
        }


        if (!orderMap.ContainsKey(name) || orderMap[name] == type)
        {
            return true;
        }
        else
        {
            return false;
        }
       
    }

    public bool isPosOrNeg(string name)
    {
        return posNegMap[name];
    }

    public void increaseCounter(int type)
    {
        switch(type)
        {
            case 1:
                spaceGreenCounter++;
                break;
            case 2:
                spaceOrangeCounter++;
                break;
            case 3:
                spaceBlueCounter++;
                break;
        }
    }

    public void decreaseCounter(int type)
    {
        switch (type)
        {
            case 1:
                spaceGreenCounter--;
                break;
            case 2:
                spaceOrangeCounter--;
                break;
            case 3:
                spaceBlueCounter--;
                break;
        }
    }
    

}
