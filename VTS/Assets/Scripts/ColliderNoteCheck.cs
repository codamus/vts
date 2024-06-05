using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderNoteCheck : MonoBehaviour
{
    public ManageMiddleSphere manageMiddleSphere;
    public bool isNote;
    public Note note;
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
