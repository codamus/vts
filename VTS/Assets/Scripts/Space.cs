using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Space : MonoBehaviour
{
    public int spaceNumber;
    //1 = green = m3, 2 = orange = M3, 3 = blue = P5, 0 = noType
    public int spaceType;
    //direction this space is attached
    public string direction;
    //note this space is attached on
    public Note preNote;
    public Note postNote;

    private XRSocketInteractor interactor;
    private GameObject right;

    // Start is called before the first frame update
    void Start()
    {
        //ERROR if no child gameobject with right names
        right = transform.Find("Right").gameObject;
        interactor = right.GetComponent<XRSocketInteractor>();
    }

    public void activateSocket()
    {
       interactor.socketActive = true;
    }
    public void deactivateSocket()
    {
        interactor.socketActive = false;
    }

    public void activateCollider()
    {
        interactor.enabled = true;
    }

    public void deactivateCollider()
    {
        interactor.enabled = false;
    }

    public void playSound()
    {
        if (preNote != null && postNote != null)
        {
            preNote.playSound();
            postNote.playSound();
        }
    }

}
