using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpaceSocketManager : MonoBehaviour
{
    public Space space;

    private XRSocketInteractor interactor;


    // Start is called before the first frame update
    void Start()
    {
        interactor = GetComponent<XRSocketInteractor>();
        interactor.selectEntered.AddListener(OnSelectEntered);
        interactor.selectExited.AddListener(OnSelectExited);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //check of select exit object is a note
    //reset note
    void OnSelectExited(SelectExitEventArgs args)
    {
        Note note = args.interactableObject.transform.gameObject.GetComponent<Note>();
        if (note != null)
        {
            note.removeFirstTypeSocket();
            //note.clearNoteOrder();
            note.deactivateSockets();
        }

    }

    //check if selected object is a note
    //add type, type order arr and activate Sockets of the note
    void OnSelectEntered(SelectEnterEventArgs args)
    {
        Note note = args.interactableObject.transform.gameObject.GetComponent<Note>();
        if (note != null)
        {
            note.addFirstTypeSocket(space.spaceType);
            //note.generateOrderFromPastNote(space.preNote, space.direction);
            note.activateSockets();
        }
        
    }
}
