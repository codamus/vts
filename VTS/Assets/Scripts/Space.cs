using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Space : MonoBehaviour
{
    public int spaceNumber;
    //1 = green, 2 = orange, 3 = blue, 0 = noType
    public int spaceType;
    //direction this space is attached
    public string direction;
    //note this space is attached on
    public Note preNote;
    public Note postNote;


    private GameObject right;

    // Start is called before the first frame update
    void Start()
    {
        //ERROR if no child gameobject with right names
        right = transform.Find("Right").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void activateSocket()
    {
        right.GetComponent<XRSocketInteractor>().socketActive = true;
    }
    public void deactivateSocket()
    {
        right.GetComponent<XRSocketInteractor>().socketActive = false;
    }

    public void activateCollider()
    {
        right.GetComponent<SphereCollider>().enabled = true;
    }

    public void deactivateCollider()
    {
        right.GetComponent<SphereCollider>().enabled = false;
    }

    public void playSound()
    {
        if (preNote != null)
        {
            preNote.playSound();
        }
        if (postNote != null)
        {
            postNote.playSound();
        }
    }

}
