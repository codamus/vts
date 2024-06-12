using UnityEngine;

public class ColliderIntervalCheck : MonoBehaviour
{
    //manager to check for completion of the triangle
    public ManageMiddleSphere manageMiddleSphere;
    //true if a interval is in this collider
    public bool isInterval;
    //interval that is in this collider
    public Interval interval;

    //manage the interval the triggers this collider
    private void OnTriggerEnter(Collider other)
    {
        Interval interval = other.gameObject.GetComponent<Interval>();

        if (interval != null)
        {
            isInterval = true;
            this.interval = interval;
            manageMiddleSphere.check();
        }
    }

    //manage the interval that leaves the trigger
    private void OnTriggerExit(Collider other)
    {
        Interval interval = other.gameObject.GetComponent<Interval>();

        if(interval != null && this.interval.Equals(interval))
        {
            isInterval = false;
            manageMiddleSphere.check();
        }
    }
}
