using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class NoteCollider : MonoBehaviour
{
    public Note note;
    public Transform attach;
    private CurrentGrabbingObject grabbingObject;
    public NoteCollider oppositeCollider;

    private int colliderSpaceType;
    private bool hasSelection;
    // Start is called before the first frame update
    void Start()
    {
        grabbingObject = GameObject.FindWithTag("Player").GetComponent<CurrentGrabbingObject>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void updateMaterial()
    {
        note.updateMaterial(GetComponent<Renderer>(), colliderSpaceType);
    }

    void OnTriggerEnter(Collider other)
    {
        if (grabbingObject.GetCurrentGrabbingObject() != null && !hasSelection)
        {
            Space space = other.gameObject.GetComponent<Space>();
            if (space != null)
            {
                /*
                if (note.isFirst)
                {
                    
                    if (note.hasNoteType(space.spaceType) && space.spaceType != colliderSpaceType)
                    {
                           return;
                    }else if(!note.hasNoteType(space.spaceType) && space.spaceType != colliderSpaceType && colliderSpaceType != 0)
                    {
                        return;
                    }
                }
                else 
                */if (!note.isRightPlace(name, space.spaceType))
                {
                    return;
                }
                if (colliderSpaceType == 0)
                {
                    colliderSpaceType = space.spaceType;
                    oppositeCollider.colliderSpaceType = this.colliderSpaceType;
                    oppositeCollider.updateMaterial();
                    updateMaterial();
                    note.addType(colliderSpaceType);
                    note.addNoteOrder(name, colliderSpaceType);
                    note.addNoteOrder(oppositeCollider.name, colliderSpaceType);
                    
                }
                updateColliderHeight(space.spaceType, (CapsuleCollider)other);
                if (space.preNote == null)
                {
                    attachObject(other.gameObject);
                    space.direction = name;
                    space.preNote = note;
                    space.activateCollider();
                }else
                {
                    space.deactivateCollider();
                }
                note.order.increaseCounter(space.spaceType);
                hasSelection = true;
                //Debug.Log("Increase Counter" + space.spaceType);
                Debug.Log("SelectSpace" + name);

            }
        }
    }

    private void attachObject(GameObject obj)
    {
        obj.GetComponent<Grabbing>().updateParentOnRelease = false;
        obj.transform.parent = attach;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
    }
    
    private void OnTriggerExit(Collider other)
    {
        Space space = other.gameObject.GetComponent<Space>();
        if (space != null && hasSelection)
        {
            
            if (!oppositeCollider.hasSelection)
            {
                note.order.decreaseCounter(space.spaceType);
                note.removeNoteOrder(name, colliderSpaceType);
                note.removeNoteOrder(oppositeCollider.name, oppositeCollider.colliderSpaceType);
                note.removeType(space.spaceType);
                colliderSpaceType = 0;
                updateMaterial();
                oppositeCollider.colliderSpaceType = 0;
                oppositeCollider.updateMaterial();
                other.gameObject.GetComponent<Grabbing>().updateParentOnRelease = true;
            }
            hasSelection = false;
            updateColliderHeight(0, (CapsuleCollider)other);
            space.direction = "";
            space.preNote = null;
            space.deactivateCollider();
            //Debug.Log("Decrease Counter" + space.spaceType);
            Debug.Log("DeselectSpace" + name);
        }
    }

    public void SetColliderType(int type)
    {
        colliderSpaceType = type;
    }

    //increase collider length to get both collider together if space is attached to a note
    //type 0: default
    private void updateColliderHeight(int type, CapsuleCollider collider)
    {
        switch (type)
        {
            case 1:
                collider.height = 4;
                break;
            case 2:
                collider.height = 3;
                break;
            case 3:
                collider.height = 2.5f;
                break;
            case 0:
                collider.height = 2;
                break;
        }
    }
    
}
