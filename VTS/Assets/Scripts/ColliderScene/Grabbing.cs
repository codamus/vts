using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grabbing : MonoBehaviour
{
    private Transform hand = null;
    private Transform objParent;
    public bool isGrabbed = false;
    private CurrentGrabbingObject grabbingObject;
    public bool updateParentOnRelease = true;

    private void Awake()
    {
        objParent = transform.parent;
        grabbingObject = GameObject.FindWithTag("Player").GetComponent<CurrentGrabbingObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("hand")) hand = other.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("hand")) hand = null;
    }
    public void simpleGrab()
    {
        isGrabbed = true;
        this.transform.parent = hand;
        grabbingObject.SetCurrentGrabbingObject(this.gameObject);
    }

    public void ReleaseSimpleGrab()
    {
        isGrabbed = false;

        if (updateParentOnRelease)
        {
            transform.parent = objParent;
        }
        else
        {
            updateParentOnRelease = true;
        }
        grabbingObject.SetCurrentGrabbingObject(null);
    }

}
