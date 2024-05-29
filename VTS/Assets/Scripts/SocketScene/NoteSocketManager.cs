using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NoteSocketManager : MonoBehaviour
{

    public Note note;
    public NoteSocketManager oppositeSide;


    private int socketSpaceType;
    private XRSocketInteractor interactor;

    public enum SocketSpaceType { Orange, Green, Blue }
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

    //default 0
    public void setSocketSpaceType(int type)
    {
        socketSpaceType = type;
    }

    //change the material from this socket circle
    public void updateMaterial()
    {
        note.updateMaterial(GetComponent<Renderer>(), socketSpaceType);
    }
    //TODO: auslagern
    //add a type to the note because one type is only once allowed
    private void addTypeToNote(int type)
    {
        note.addType(type);
    }

    //TODO: Auslagern
    //check if note has already that type
    //true if hasType
    private bool hasNoteType(int type)
    {
       return note.hasNoteType(type);
    }

    //check if selected object is a space and allowed to attach
    //wrong: deacitvate Socket
    //right: update space, socket and opposite socket information
    void OnSelectEntered(SelectEnterEventArgs args)
    {
        
        Space space = args.interactableObject.transform.gameObject.GetComponent<Space>();
        if (space != null)
        {
            /*
            if (note.isFirst)
            {
                if (hasNoteType(space.spaceType) && space.spaceType != socketSpaceType)
                {
                    interactor.socketActive = false;
                    return;
                }
            }
            else */if(!note.isRightPlace(name, space.spaceType))
            {
                interactor.socketActive = false;
                return;
            }
            if (socketSpaceType == 0)
            {
                setSocketSpaceType(space.spaceType);
                oppositeSide.setSocketSpaceType(socketSpaceType);
                oppositeSide.updateMaterial();
                updateMaterial();
                addTypeToNote(socketSpaceType);
                note.addNoteOrder(name, socketSpaceType);
                oppositeSide.note.addNoteOrder(oppositeSide.name, socketSpaceType);
            }
            space.direction = name;
            space.preNote = note;
            space.activateSocket();
            Debug.Log("Selected" + name);

        }
            
    }

    //check if select exit is a space
    //wrong: nothing
    //right: update space, socket and opposite socket information
    void OnSelectExited(SelectExitEventArgs args)
    {
        Space space = args.interactableObject.transform.gameObject.GetComponent<Space>();
        if (space != null)
        {
            if(!interactor.socketActive)
            {
                interactor.socketActive = true;
            }else if (!oppositeSide.interactor.hasSelection)
                {
                    note.removeNoteOrder(name, socketSpaceType);
                    oppositeSide.note.removeNoteOrder(oppositeSide.name, oppositeSide.socketSpaceType);
                    setSocketSpaceType(0);
                    oppositeSide.setSocketSpaceType(0);
                    oppositeSide.updateMaterial();
                    updateMaterial();
            }
            space.direction = "";
            space.preNote = null;
            space.deactivateSocket();
            Debug.Log("Deselect" + name);
        }
    }
}
