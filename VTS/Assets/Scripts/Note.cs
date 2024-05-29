using Microsoft.MixedReality.OpenXR;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static NoteSocketManager;

public class Note : MonoBehaviour
{
    //public bool isFirst;

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

    //position: 0=top, 1=topRight, 2=right, 3=bottom, 4=leftBottom, 5=left
    //type: 1=spaceOne (green), 2=spaceTwo (orange), 3=spaceThree (blue)
    private int[] typeOrder;

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
        typeOrder = new int[6];

    }

    // Update is called once per frame
    void Update()
    {
        
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

    //add socket information to bottom socket because on attach an note to a space bottom is always attached
    public void addFirstTypeSocket(int type)
    {
        NoteSocketManager bottomManager = bottom.GetComponent<NoteSocketManager>();
        bottomManager.setSocketSpaceType(type);
        bottomManager.updateMaterial();
        bottomManager.oppositeSide.setSocketSpaceType(type);
        bottomManager.oppositeSide.updateMaterial();
    }

    //remove socket information from first socket
    public void removeFirstTypeSocket()
    {
        NoteSocketManager bottomManager = bottom.GetComponent<NoteSocketManager>();
        bottomManager.setSocketSpaceType(0);
        bottomManager.updateMaterial();
        bottomManager.oppositeSide.setSocketSpaceType(0);
        bottomManager.oppositeSide.updateMaterial();
    }

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

    //activate all sockets beside bottom 
    public void activateSockets()
    {
        top.GetComponent<XRSocketInteractor>().socketActive = true;
        left.GetComponent<XRSocketInteractor>().socketActive = true;
        right.GetComponent<XRSocketInteractor>().socketActive = true;
        topRight.GetComponent<XRSocketInteractor>().socketActive = true;
        bottomLeft.GetComponent<XRSocketInteractor>().socketActive = true;
    }

    //Deactivate all sockets
    public void deactivateSockets()
    {
        top.GetComponent<XRSocketInteractor>().socketActive = false;
        bottom.GetComponent<XRSocketInteractor>().socketActive = false;
        left.GetComponent<XRSocketInteractor>().socketActive = false;
        right.GetComponent<XRSocketInteractor>().socketActive = false;
        topRight.GetComponent<XRSocketInteractor>().socketActive = false;
        bottomLeft.GetComponent<XRSocketInteractor>().socketActive = false;
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

    /**
     * Old Code for sockets
    //add one entry in noteOrder Array
    public void addNoteOrder(string direction, int type)
    {
        int position = getOrderPositionFromDirection(direction);
        if (typeOrder[position] == 0 && isFirst)
        {
            typeOrder[position] = type;
        }
    }

    //remove one noteOrder Array entry
    public void removeNoteOrder(string direction)
    {
        if(isFirst)
        {
            typeOrder[getOrderPositionFromDirection(direction)] = 0;
        }
    }

    public void clearNoteOrder()
    {
        typeOrder = new int[6];
    }

    //true if the type is in given direction
    public bool isRightPlace(string direction, int type)
    {
        return typeOrder[getOrderPositionFromDirection(direction)] == type;
    }

    //create new typeOrder from past typeOrder with the right shift amount get by direction
    public void generateOrderFromPastNote(Note note, string direction) 
    {
        int shiftAmount = 0;
        switch (direction)
        {
            case "Left":
                shiftAmount = 5;
                break;
            case "BottomLeft":
                shiftAmount = 4;
                break;
            case "Bottom":
                shiftAmount = 3;
                break;
            case "Right":
                shiftAmount = 2;
                break;
            case "TopRight":
                shiftAmount = 1;
                break;

                
        }
        pastePreTypeOrderInTypeOrder(note.typeOrder, shiftAmount);
    }

    //shift preTypeOrder Array in current typeOrder Array
    private void pastePreTypeOrderInTypeOrder(int[] preTypeOrder, int shiftAmount)
    {
        for(int i = 0; i< preTypeOrder.Length; i++)
        {
            typeOrder[i] = preTypeOrder[(i+shiftAmount) % 6];
        }
    }

    //convert direction string into direction int for typeOrder Array
    private int getOrderPositionFromDirection(string direction)
    {
        switch(direction)
        {
            case "Top":
                return 0;
            case "TopRight": 
                return 1;
            case "Right":
                return 2;
            case "Bottom":
                return 3;
            case "BottomLeft":
                return 4;
            case "Left":
                return 5;
            default:
                Debug.LogError("Wrong direction");
                return 0;
        }
    }
    */
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
        if (order.isPosOrNeg(correctDirection(preDirection)))
        {
            noteNumber = (preNoteNumber + spaceNumber) % 12;
        }
        else
        {
            noteNumber = (preNoteNumber - spaceNumber) % 12;
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

    private void updateSound()
    {
        soundManager.changeSound(noteNumber.ToString());
    }

    public void playSound()
    {
        soundManager.playSound();
    }


}
