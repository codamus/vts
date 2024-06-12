using UnityEngine;

public class CurrentGrabbingObject : MonoBehaviour
{
    private GameObject currentGrabbingObject;

    public void SetCurrentGrabbingObject(GameObject obj)
    {
        this.currentGrabbingObject = obj;
    }
    public GameObject GetCurrentGrabbingObject()
    {
        return currentGrabbingObject;
    }
}
