using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class SpaceCollider: MonoBehaviour
{
    public Space interval;
    public Transform attach;

    private void OnTriggerEnter(Collider other)
    {
        Note note = other.gameObject.GetComponent<Note>();
        if (note != null)
        {
            if(note.noteNumber != -1) 
            {
                return;
            }
            note.addFirstTypeCollider(interval.intervalType);
            //note.generateOrderFromPastNote(space.preNote, space.direction);
            note.activateColliders();
            if (interval.intervalType < 3)
            {
                GetComponent<SphereCollider>().center = new Vector3(1.1f, 0, 0);
            }
            note.preDirection = interval.direction;
            note.preShiftAmount = interval.preNote.generateShiftAmountFormDirection(interval.preNote.preDirection);
            attachObject(other.gameObject);
            interval.postNote = note;
            note.calculateNoteNumber(interval.preNote.noteNumber, interval.intervalNumber);
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
            if (interval.intervalType < 3)
            {
                GetComponent<SphereCollider>().center = new Vector3(0, 0, 0);
            }
            note.preDirection = "Top";
            note.preShiftAmount = 0;
            interval.postNote = null;
            note.resetNoteNumber();
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
