using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentGrabbingObject : MonoBehaviour
{
    private GameObject currentGrabbingObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrentGrabbingObject(GameObject obj)
    {
        this.currentGrabbingObject = obj;
    }
    public GameObject GetCurrentGrabbingObject()
    {
        return currentGrabbingObject;
    }
}
