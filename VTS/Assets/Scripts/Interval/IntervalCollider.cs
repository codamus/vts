using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class IntervalCollider: MonoBehaviour
{
    //interval of this collider
    public Interval interval;
    //the position of a attached note
    public Transform attach;

    private void OnTriggerEnter(Collider other)
    {
        Note note = other.gameObject.GetComponent<Note>();
        if (note != null)
        {
            //if note is already attached to another interval
            if(note.noteNumber != -1) 
            {
                return;
            }
            //add this interval type to the atteched note collider
            note.addTypeToFirstCollider(interval.intervalType);
            note.activateColliders();

            //adjust this collider for permanent collision with the note collider
            GetComponent<SphereCollider>().center = new Vector3(1.2f, 0, 0);
          
            //for calculation of the right interval order
            note.preDirection = interval.direction;
            note.preShiftAmount = interval.preNote.generateShiftAmountFormDirection(interval.preNote.preDirection);

            
            attachObject(other.gameObject);
            interval.postNote = note;

            note.calculateNoteNumber(interval.preNote.noteNumber, interval.getIntervalNumber());
            note.fixTextRotation();

            //for debugging
            //Debug.Log("SelectNote" + name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Note note = other.gameObject.GetComponent<Note>();
        if (note != null)
        {
            //remove the type from the note collider
            note.removeTypeFromFirstCollider();
            note.deactivateColliders();

            //reset the collider position
            GetComponent<SphereCollider>().center = new Vector3(0, 0, 0);
            //reset note variables set to default
            note.preDirection = "Top";
            note.preShiftAmount = 0;
            interval.postNote = null;
            note.resetNoteNumber();
            note.fixTextRotation();

            //for debugging
            //Debug.Log("DeselectNote" + name);
        }
    }

    //set the position of the grabbed object to the attach transform
    private void attachObject(GameObject obj)
    {
        //make sure the object keep the given position
        obj.GetComponent<Grabbing>().updateParentOnRelease = false;

        //update position
        obj.transform.parent = attach;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
    }
}
