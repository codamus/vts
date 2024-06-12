using System.Collections.Generic;
using UnityEngine;

public class IntervalOrder : MonoBehaviour
{
    //key: direction, value: space type 
    private Dictionary<string, IntervalType> orderMap;

    //bool false = minus, bool true = plus
    private Dictionary<string, bool> posNegMap;

    //array with all six side names
    private string[] allSideNames;

    //counter of each interval for only remove order if the last interval is removed
    private int m3Counter;
    private int M3Counter;
    private int P5Counter;

    // Start is called before the first frame update
    void Start()
    {
        orderMap = new Dictionary<string, IntervalType>();
        posNegMap = new Dictionary<string, bool>();
        allSideNames = new string[6];
        allSideNames[0] = "Top";
        allSideNames[1] = "TopRight";
        allSideNames[2] = "Right";
        allSideNames[3] = "Bottom";
        allSideNames[4] = "BottomLeft";
        allSideNames[5] = "Left";


    }

    //add the givin type the the orderMap if this type is not already in the map
    //if added type is P5 the PosNegMap for the number calculation is generated
    public void addTypeToOrder(string name, IntervalType type)
    {
        if (!orderMap.ContainsKey(name))
        {
            if (type  == IntervalType.P5 && !posNegMap.ContainsKey(name))
            {
               generatePosNegMap(name);
            }
            orderMap.Add(name, type);

            //for debugging
            //Debug.Log("Add:" + name + type);
        }
    }

    //fill the PosNegMap with position and the right bool
    private void generatePosNegMap(string name) 
    {
        //name is the direction the interval P5 is attached
        //shift to the first minus position
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

        //add the position and bool to the PosNegMap
        //go around clockwise beginning two positions away from the P5 attach
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

    //remove type in orderMap only if the last interval of a type is removed
    //if its P5 also clear the PosNegMap
    public void removeTypeInOrder(string name, IntervalType type)
    {
        if (type == IntervalType.m3 && m3Counter == 0)
        {
            orderMap.Remove(name);
        }
        else if (type == IntervalType.M3Big && M3Counter == 0) 
        { 
            orderMap.Remove(name);
        }
        else if(type == IntervalType.P5 && P5Counter == 0)
        {
            orderMap.Remove(name);
            posNegMap.Clear();
        }
    }

    //check if the interval is attached in the right direction
    //only works with the corrected direction
    public bool isRightPlace(string name, IntervalType type)
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

    //increase the givin typeCounter by 1
    public void increaseCounter(IntervalType type)
    {
        switch(type)
        {
            case IntervalType.m3:
                m3Counter++;
                break;
            case IntervalType.M3Big:
                M3Counter++;
                break;
            case IntervalType.P5:
                P5Counter++;
                break;
        }
    }

    //decrease the givin typeCounter by 1
    public void decreaseCounter(IntervalType type)
    {
        switch (type)
        {
            case IntervalType.m3:
                m3Counter--;
                break;
            case IntervalType.M3Big:
                M3Counter--;
                break;
            case IntervalType.P5:
                P5Counter--;
                break;
        }
    }
    

}
