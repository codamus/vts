using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Space : MonoBehaviour
{
    //number of the interval: 3 = m3 = green, 4 = M3 = orange, 7 = P5 = blue
    public int intervalNumber;
    //1 = green = m3, 2 = orange = M3, 3 = blue = P5, 0 = noType
    public int intervalType;
    //direction this space is attached
    public string direction;
    //note this space is attached on
    public Note preNote;
    //note attached to this interval
    public Note postNote;

    //collider of the right Object for attaching a Note
    private SphereCollider sphereCollider;
    //gameobject where the notes gets attached to
    private GameObject right;

    // Start is called before the first frame update
    void Start()
    {
        //ERROR if no child gameobject with right names
        right = transform.Find("Right").gameObject;
        sphereCollider = right.GetComponent<SphereCollider>();
    }

    //activate the Sphere Collider of the "Right" Object
    public void activateCollider()
    {
        sphereCollider.enabled = true;
    }

    //deactivate the Sphere Collider of the "Right" Object
    public void deactivateCollider()
    {
        sphereCollider.enabled = false;
    }

    //play the sounds of the pre and post note
    public void playSound()
    {
        if (preNote != null && postNote != null)
        {
            preNote.playSound();
            postNote.playSound();
        }
    }

}
