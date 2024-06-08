using TMPro;
using UnityEngine;

public class Note : MonoBehaviour
{
    public SpaceOrder order;

    public Material materialSpaceOne;
    public Material materialSpaceTwo;
    public Material materialSpaceThree;
    public Material materialDefault;

    public PlaySound soundManager;

    public string preDirection = "Top";

    public int preShiftAmount;

    public int noteNumber;

    public TMP_Text textField;

    private GameObject top;
    private GameObject bottom;
    private GameObject right;
    private GameObject left;
    private GameObject topRight;
    private GameObject bottomLeft;

    private bool hasSpaceGreen;  
    private bool hasSpaceOrange;  
    private bool hasSpaceBlue;


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
        order = GameObject.FindWithTag("SpaceOrder").GetComponent<SpaceOrder>();

    }


    public void setHasSpaceGreen(bool hasSpaceGreen)
    {
        this.hasSpaceGreen = hasSpaceGreen;
    }
    public void setHasSpaceOrange(bool hasSpaceOrange)
    {
        this.hasSpaceOrange = hasSpaceOrange;
    }
    public void setHasSpaceBlue(bool hasSpaceBlue) 
    {  
        this.hasSpaceBlue = hasSpaceBlue;
    }
    public bool getHasSpaceOrange() {  return this.hasSpaceOrange; }
    
    public bool getHasSpaceGreen() {  return this.hasSpaceGreen; }
    public bool getHasSpaceBlue() {  return this.hasSpaceBlue; }

    public void addFirstTypeCollider(int type)
    {
        NoteCollider bottomCollider = bottom.GetComponent<NoteCollider>();
        bottomCollider.SetColliderType(type);
        bottomCollider.oppositeCollider.SetColliderType(type);
        bottomCollider.updateMaterial();
        bottomCollider.oppositeCollider.updateMaterial();
    }

    public void removeFirstTypeCollider()
    {
        NoteCollider bottomCollider = bottom.GetComponent<NoteCollider>();
        bottomCollider.SetColliderType(0);
        bottomCollider.oppositeCollider.SetColliderType(0);
        bottomCollider.updateMaterial();
        bottomCollider.oppositeCollider.updateMaterial();
    }

    public void activateColliders()
    {
        top.GetComponent<Collider>().enabled = true;
        left.GetComponent<Collider>().enabled = true;
        right.GetComponent<Collider>().enabled = true;
        topRight.GetComponent<Collider>().enabled = true;
        bottomLeft.GetComponent<Collider>().enabled = true;
    }

    public void deactivateColliders()
    {
        top.GetComponent<Collider>().enabled = false;
        left.GetComponent<Collider>().enabled = false;
        right.GetComponent<Collider>().enabled = false;
        topRight.GetComponent<Collider>().enabled = false;
        bottomLeft.GetComponent<Collider>().enabled = false;
    }


    //new Code for Colliders
    public void addNoteOrder(string direction, int type)
    {
        order.addTypeToOrder(correctDirection(direction), type);
    }

    public void removeNoteOrder(string direction, int type)
    {
        order.removeTypeInOrder(correctDirection(direction), type);
    }
    public bool isRightPlace(string direction, int type)
    {
        Debug.Log(correctDirection(direction));
        return order.isRightPlace(correctDirection(direction), type);
    }
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

    //change the material from this socket circle
    public void updateMaterial(Renderer renderer, int socketSpaceType)
    {
        //Debug.Log("updateMaterial" + name);
        switch (socketSpaceType)
        {
            case 0:
                renderer.material = materialDefault;
                break;
            case 1:
                renderer.material = materialSpaceOne;
                break;
            case 2:
                renderer.material = materialSpaceTwo;
                break;
            case 3:
                renderer.material = materialSpaceThree;
                break;
        }
    }
    //Add type 1,2 or 3 to the note
    public void addType(int type)
    {
        switch (type)
        {
            case 1:
                hasSpaceGreen = true;
                break;
            case 2:
                hasSpaceOrange = true;
                break;
            case 3:
                hasSpaceBlue = true;
                break;
        }
    }

    //return if the note have at least one of the searched type
    public bool hasNoteType(int type)
    {
        switch (type)
        {
            case 1:
                return hasSpaceGreen;
            case 2:
                return hasSpaceOrange;
            case 3:
                return hasSpaceBlue;
            default:
                return false;
        }
    }

    //remove the type 1,2 or 3 from the note
    //only remove if no space of this type is attached to the note
    public void removeType(int type)
    {
        switch (type)
        {
            case 1:
                hasSpaceGreen = false;
                break;
            case 2:
                hasSpaceOrange = false;
                break;
            case 3:
                hasSpaceBlue = false;
                break;
        }
    }

    public void calculateNoteNumber(int preNoteNumber, int spaceNumber)
    {
        if (order.isPosOrNeg(correctDirection("Top")))
        {
            noteNumber = (preNoteNumber + spaceNumber) % 12;
        }
        else
        {
            if(preNoteNumber >= spaceNumber)
            {
                noteNumber = preNoteNumber - spaceNumber;
            }
            else
            {
                noteNumber = 12 + (preNoteNumber - spaceNumber);
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
