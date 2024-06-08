using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderNoteCheck : MonoBehaviour
{
    public ManageMiddleSphere manageMiddleSphere;
    public bool isNote;
    public Note note;

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
