using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketColliderManager : MonoBehaviour
{
    //lower priority disable collider 
    public bool isCube;
    public bool isEnabled = true;
    public SocketManager socketManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       GetComponent<XRSocketInteractor>().enabled = isEnabled;
    }

    void OnTriggerEnter(Collider collider)
    {
      
        if(collider != null && collider.isTrigger) {
            SocketColliderManager otherColliderManager = collider.gameObject.GetComponent<SocketColliderManager>();
            if(otherColliderManager != null)
            {
                if (otherColliderManager.isCube && isCube || !otherColliderManager.isCube && !isCube)
                {
                    
                    isEnabled = false;
                }else if (otherColliderManager.socketManager.priority > socketManager.priority)
                {
                 
                    isEnabled = false;

                }
                else if(otherColliderManager.socketManager.priority == socketManager.priority)
                {
                    if(!isCube && otherColliderManager.isCube)
                    {
                        
                        isEnabled = false;
                    }
                }
                //Debug.Log("" + socketManager.priority + isCube + isEnabled);
            }
        }
    }

    public void enableSocket()
    {
        isEnabled = true;
    }

}
 