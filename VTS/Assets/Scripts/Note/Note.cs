using TMPro;
using UnityEngine;

public class Note : MonoBehaviour
{
    //order the intervals has to placed
    public IntervalOrder order;

    //material of the different intervals
    public Material materialIntervalm3;
    public Material materialIntervalM3;
    public Material materialIntervalP5;
    public Material materialDefault;

    //soundmanager for playing sounds
    public PlaySound soundManager;

    //default "Top"
    public string preDirection = "Top";

    public int preShiftAmount;

    //default -1
    public int noteNumber;

    //text field on the note
    public TMP_Text textField;

    //all colliders around the note to attach intervals
    private GameObject top;
    private GameObject bottom;
    private GameObject right;
    private GameObject left;
    private GameObject topRight;
    private GameObject bottomLeft;

    // Start is called before the first frame update
    void Start()
    {
        //ERROR if no child gameobject with right names
        top = transform.Find("Top").gameObject;
        bottom = transform.Find("Bottom").gameObject;
        right = transform.Find("Right").gameObject;
        left = transform.Find("Left").gameObject;
        topRight = transform.Find("TopRight").gameObject;
        bottomLeft = transform.Find("BottomLeft").gameObject;

        //Gameobject with an IntervalOrder script attached and need the tag IntervalOrder
        order = GameObject.FindWithTag("IntervalOrder").GetComponent<IntervalOrder>();

    }

    //add the givin type to the bottom collider and update the material
    //also for opposite
    //all notes attach with bottom to the interval
    public void addTypeToFirstCollider(IntervalType type)
    {
        NoteCollider bottomCollider = bottom.GetComponent<NoteCollider>();
        bottomCollider.SetColliderType(type);
        bottomCollider.oppositeCollider.SetColliderType(type);
        bottomCollider.updateMaterial();
        bottomCollider.oppositeCollider.updateMaterial();
    }

    //remove the type and update material from the bottom collider
    //also for opposite
    public void removeTypeFromFirstCollider()
    {
        NoteCollider bottomCollider = bottom.GetComponent<NoteCollider>();
        bottomCollider.SetColliderType(IntervalType.noType);
        bottomCollider.oppositeCollider.SetColliderType(IntervalType.noType);
        bottomCollider.updateMaterial();
        bottomCollider.oppositeCollider.updateMaterial();
    }

    //do not activate bottom because bottom is always attached to the first interval
    public void activateColliders()
    {
        top.GetComponent<Collider>().enabled = true;
        left.GetComponent<Collider>().enabled = true;
        right.GetComponent<Collider>().enabled = true;
        topRight.GetComponent<Collider>().enabled = true;
        bottomLeft.GetComponent<Collider>().enabled = true;
    }

    //do not deactivate bottom because bottom is never activated
    public void deactivateColliders()
    {
        top.GetComponent<Collider>().enabled = false;
        left.GetComponent<Collider>().enabled = false;
        right.GetComponent<Collider>().enabled = false;
        topRight.GetComponent<Collider>().enabled = false;
        bottomLeft.GetComponent<Collider>().enabled = false;
    }

    //correct the direction and add the type to the interval order
    public void addNoteOrder(string direction, IntervalType type)
    {
        order.addTypeToOrder(correctDirection(direction), type);
    }

    //correct the direction and remove the type from the interval order
    public void removeNoteOrder(string direction, IntervalType type)
    {
        order.removeTypeInOrder(correctDirection(direction), type);
    }

    //check if the interval is at the right place
    public bool isRightPlace(string direction, IntervalType type)
    {
        return order.isRightPlace(correctDirection(direction), type);
    }

    //correct the direction because the notes may turn around on attachment
    //for the correct interval map and check of rightplace the direction will correct as the note were with "Top" at the top
    private string correctDirection(string direction)
    {
        string[] directions = new string[6];
        directions[0] = "Top";
        directions[1] = "TopRight";
        directions[2] = "Right";
        directions[3] = "Bottom";
        directions[4] = "BottomLeft";
        directions[5] = "Left";
        switch (preDirection)
        {
            case "Top":
                return directions[generateShiftAmountFormDirection(direction)];
            case "TopRight":
                return directions[(generateShiftAmountFormDirection(direction) + 1) % 6];
            case "Right":
                return directions[(generateShiftAmountFormDirection(direction) + 2) % 6];
            case "Bottom":
                return directions[(generateShiftAmountFormDirection(direction) + 3) % 6];
            case "BottomLeft":
                return directions[(generateShiftAmountFormDirection(direction) + 4) % 6];
            case "Left":
                return directions[(generateShiftAmountFormDirection(direction) + 5) % 6];
            default:
                return null;

        }

    }

    //get the number of how often the note is turned around
    //also work with the shift amount of the last note
    public int generateShiftAmountFormDirection(string direction)
    {
        switch (direction)
        {
            case "Left":
                return (preShiftAmount + 5) % 6;
            case "BottomLeft":
                return (preShiftAmount + 4) % 6;

            case "Bottom":
                return (preShiftAmount + 3) % 6;

            case "Right":
                return (preShiftAmount + 2) % 6;

            case "TopRight":
                return (preShiftAmount + 1) % 6;

            case "Top":
                return preShiftAmount;

            default:
                return -1;
        }
    }

    //fix the rotation of the text that it will always display in the same direction on all notes
    public void fixTextRotation()
    {
        float rotation = 0;
        switch (correctDirection("Bottom"))
        {
            case "BottomLeft":
                rotation = 60f;
                break;
            case "Left":
                rotation = 120f;
                break;
            case "Top":
                rotation = 180f;
                break;
            case "TopRight":
                rotation = 240f;
                break;
            case "Right":
                rotation = 300f;
                break;
            case "Bottom":
                rotation = 0f;
                break;
        }
        Vector3 currentRotation = textField.transform.localEulerAngles;
        currentRotation.z = rotation;
        textField.transform.localEulerAngles = currentRotation;
    }

    //change the material from the given render according to the given interval type
    public void updateMaterial(Renderer renderer, IntervalType type)
    {
        switch (type)
        {
            case IntervalType.noType:
                renderer.material = materialDefault;
                break;
            case IntervalType.m3:
                renderer.material = materialIntervalm3;
                break;
            case IntervalType.M3Big:
                renderer.material = materialIntervalM3;
                break;
            case IntervalType.P5:
                renderer.material = materialIntervalP5;
                break;
        }
    }

    public void calculateNoteNumber(int preNoteNumber, int intervalNumber)
    {
        //plus if true
        if (order.isPosOrNeg(correctDirection("Top")))
        {
            noteNumber = (preNoteNumber + intervalNumber) % 12;
        }
        else
        {
            //positive number
            if(preNoteNumber >= intervalNumber)
            {
                noteNumber = preNoteNumber - intervalNumber;
            }
            //negative number
            else
            {
                noteNumber = 12 + (preNoteNumber - intervalNumber);
            }
        }

        updateSound();
        updateTextOnNote();
    }

    public void resetNoteNumber()
    {
        noteNumber = -1;
        updateSound();
        updateTextOnNote();

    }

    private void updateTextOnNote()
    {
        if(noteNumber == -1)
        {
            textField.text = "";
        }
        else 
        { 
            textField.text = noteNumber.ToString();
        }
        
    }

    public void updateSound()
    {
        soundManager.changeSound(noteNumber.ToString());
    }

    public void playSound()
    {
        soundManager.playSound();
    }


}
