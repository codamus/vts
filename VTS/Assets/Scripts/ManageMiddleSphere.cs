using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageMiddleSphere : MonoBehaviour
{
    public ColliderSpaceCheck Collider1;
    public ColliderSpaceCheck Collider2;
    public ColliderSpaceCheck Collider3;
    public ColliderNoteCheck Collider4;
    public ColliderNoteCheck Collider5;
    public ColliderNoteCheck Collider6;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void check()
    {
        if (Collider1.isSpace == true &&  Collider2.isSpace == true && Collider3.isSpace == true && Collider4.isNote == true && Collider5.isNote == true && Collider6.isNote == true)
        {
            GetComponent<Collider>().enabled = true;
            GetComponent<MeshRenderer>().enabled = true;
        }
    }

    public void playSound()
    {
        Collider4.note.playSound();
        Collider5.note.playSound();
        Collider6.note.playSound();
    }
}
