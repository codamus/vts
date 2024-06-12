using UnityEngine;

public class ColliderNoteCheck : MonoBehaviour
{
    //manager to check for completion of the triangle
    public ManageMiddleSphere manageMiddleSphere;
    //true if a note is in this collider
    public bool isNote;
    //note that is in this collider
    public Note note;

    //manage the note the triggers this collider
    private void OnTriggerEnter(Collider other)
    {
        Note note = other.gameObject.GetComponent<Note>();

        if (note != null)
        {
            isNote = true;
            this.note = note;
            manageMiddleSphere.check();
        }
    }

    //manage the note that leaves the trigger
    private void OnTriggerExit(Collider other)
    {
        Note note = other.gameObject.GetComponent<Note>();

        if (note != null && this.note.Equals(note))
        {
            isNote = false;
            this.note = null;
            manageMiddleSphere.check();
        }
    }
}
