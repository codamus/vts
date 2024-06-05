using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ColliderSpaceCheck : MonoBehaviour
{
    public ManageMiddleSphere manageMiddleSphere;
    public bool isSpace;
    public Space space;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Space space = other.gameObject.GetComponent<Space>();

        if (space != null)
        {
            isSpace = true;
            this.space = space;
            manageMiddleSphere.check();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Space space = other.gameObject.GetComponent<Space>();

        if(space != null && this.space.Equals(space))
        {
            isSpace = false;
            manageMiddleSphere.check();
        }
    }
}
