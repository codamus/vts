using UnityEngine;

public class SpaceCollider: MonoBehaviour
{
    public Space space;
    public Transform attach;
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
        Note note = other.gameObject.GetComponent<Note>();
        if (note != null)
        {
            note.addFirstTypeCollider(space.spaceType);
            //note.generateOrderFromPastNote(space.preNote, space.direction);
            note.activateColliders();
            if (space.spaceType < 3)
            {
                GetComponent<SphereCollider>().center = new Vector3(1.1f, 0, 0);
            }
            note.preDirection = space.direction;
            note.preShiftAmount = space.preNote.generateShiftAmountFormDirection(space.preNote.preDirection);
            attachObject(other.gameObject);
            space.postNote = note;
            note.calculateNoteNumber(space.preNote.noteNumber, space.spaceNumber);
            note.fixTextRotation();
            Debug.Log("SelectNote" + name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Note note = other.gameObject.GetComponent<Note>();
        if (note != null)
        {
            note.removeFirstTypeCollider();
            //note.clearNoteOrder();
            note.deactivateColliders();
            if (space.spaceType < 3)
            {
                GetComponent<SphereCollider>().center = new Vector3(0, 0, 0);
            }
            note.preDirection = "Top";
            note.preShiftAmount = 0;
            space.postNote = null;
            note.noteNumber = -1;
            note.updateSound();
            note.fixTextRotation();
            Debug.Log("DeselectNote" + name);
        }
    }

    private void attachObject(GameObject obj)
    {
        obj.GetComponent<Grabbing>().updateParentOnRelease = false;
        obj.transform.parent = attach;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
    }
}
