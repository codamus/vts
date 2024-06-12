using UnityEngine;

public class NoteCollider : MonoBehaviour
{
    //note this collider is at
    public Note note;
    //position where the intervals gets attached
    public Transform attach;
    //the opposite collider of the note
    public NoteCollider oppositeCollider;

    private CurrentGrabbingObject grabbingObject;

    //interval type that is or can be attached to this note
    private IntervalType colliderIntervalType = IntervalType.noType;

    //indicates if this collider has an interval attached
    private bool hasSelection;

    // Start is called before the first frame update
    void Start()
    {
        //need a Gameobject with tag Player and a CurrentGrabbingObject on it
        grabbingObject = GameObject.FindWithTag("Player").GetComponent<CurrentGrabbingObject>();
    }

    //updateMaterial of this sphere
    public void updateMaterial()
    {
        note.updateMaterial(GetComponent<Renderer>(), colliderIntervalType);
    }

    void OnTriggerEnter(Collider other)
    {
        if (grabbingObject.GetCurrentGrabbingObject() != null && !hasSelection)
        {
            Interval interval = other.gameObject.GetComponent<Interval>();
            if (interval != null)
            {
                //do not attach if its not the right place
                if (!note.isRightPlace(name, interval.intervalType))
                {
                    return;
                }
                //if this collider has no Type set the type and update material and the order
                //also update these parameters from the opposite collider
                if (colliderIntervalType == IntervalType.noType)
                {
                    //set intervalTypes to note colliders
                    colliderIntervalType = interval.intervalType;
                    oppositeCollider.colliderIntervalType = this.colliderIntervalType;

                    //update materials
                    oppositeCollider.updateMaterial();
                    updateMaterial();

                    //update note order
                    note.addNoteOrder(name, colliderIntervalType);
                    note.addNoteOrder(oppositeCollider.name, colliderIntervalType);
                    
                }
    
                updateColliderHeight(interval.intervalType, (CapsuleCollider)other);

                //only attach if the interval is not already attached to a note
                if (interval.preNote == null)
                {
                    attachObject(other.gameObject);

                    interval.direction = name;
                    interval.preNote = note;

                    interval.activateCollider();
                }else
                {
                    interval.postNote = note;

                    interval.deactivateCollider();
                }

                note.order.increaseCounter(interval.intervalType);
                
                hasSelection = true;

                //for debugging
                //Debug.Log("SelectInterval" + name);

            }
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
    
    private void OnTriggerExit(Collider other)
    {
        Interval space = other.gameObject.GetComponent<Interval>();
        if (space != null && hasSelection)
        {
            //only reset note order, materials and collider type if the opposite collider has no selection
            if (!oppositeCollider.hasSelection)
            {
                note.removeNoteOrder(name, colliderIntervalType);
                note.removeNoteOrder(oppositeCollider.name, oppositeCollider.colliderIntervalType);

                //reset interval types 
                colliderIntervalType = IntervalType.noType;
                oppositeCollider.colliderIntervalType = IntervalType.noType;

                //reset material
                updateMaterial();
                oppositeCollider.updateMaterial();
            }

            note.order.decreaseCounter(space.intervalType);

            hasSelection = false;

            //reset collider height
            updateColliderHeight(IntervalType.noType, (CapsuleCollider)other);

            space.direction = "";
            space.preNote = null;
            space.deactivateCollider();

            //for debugging
            //Debug.Log("DeselectInterval" + name);
        }
    }

    public void SetColliderType(IntervalType type)
    {
        colliderIntervalType = type;
    }

    //increase collider length to get both collider together if space is attached to a note
    //noType: default
    private void updateColliderHeight(IntervalType type, CapsuleCollider collider)
    {
        switch (type)
        {
            case IntervalType.m3:
                collider.height = 4;
                break;
            case IntervalType.M3Big:
                collider.height = 3;
                break;
            case IntervalType.P5:
                collider.height = 2.5f;
                break;
            case IntervalType.noType:
                collider.height = 2;
                break;
        }
    }
    
}
