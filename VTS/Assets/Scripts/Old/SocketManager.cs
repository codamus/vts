using System.Net.Sockets;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketManager : MonoBehaviour
{
    XRSocketInteractor[] sockets;
    public int priority;
    //public ManageRotationCollider rotationCollider;

    void Start()
    {
        sockets = GetComponentsInChildren<XRSocketInteractor>();
        foreach (XRSocketInteractor socket in sockets)
        {
            socket.selectEntered.AddListener(OnObjectAttached);
            socket.selectExited.AddListener(OnObjectDetached);
        }
    }

    void OnObjectAttached(SelectEnterEventArgs args)
    {
        //change priority for right attachment
        priority++;
        args.interactable.gameObject.GetComponent<SocketManager>().priority++;
        //disable grab for inner objects
        if(priority == 2)
        {
            GetComponent<XRGrabInteractable>().enabled = false;
        }
        Debug.Log("Attached");
        //rotationCollider.increaseColliderSize();
    }

    void OnObjectDetached(SelectExitEventArgs args)
    {
        //change priority for right attachment
        priority--;
        args.interactable.gameObject.GetComponent<SocketManager>().priority--;
        //enable grab for outer objects
        if (priority < 2)
        {
            GetComponent<XRGrabInteractable>().enabled = true;
        }else if(priority == 0) { 
            foreach(SocketColliderManager socketColliderManager in GetComponentsInChildren<SocketColliderManager>())
            {
                socketColliderManager.isEnabled = true;
            }
        }
        //rotationCollider.decreaseColliderSize();
    }

    void disableAllSockets()
    {
        foreach(XRSocketInteractor socket in sockets)
        {
                socket.enabled = false;
        }
    }

   void OnEnableAllSockets()
    {
        SocketColliderManager[] socketColliderManager = GetComponentsInChildren<SocketColliderManager>();
        foreach(SocketColliderManager socketCM in socketColliderManager) 
        {
            if (socketCM.isEnabled)
            {
                socketCM.enableSocket();
            }
        }
    }
}