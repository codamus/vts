using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class ManageRotation: MonoBehaviour
{

    private SphereCollider sphereCollider;
    private MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    /**
     * Toggle Spherecollider on own gameobject
     */
    public void toggleSphereCollider()
    {
            if(!sphereCollider.enabled)
            {
                meshRenderer.enabled = true;
                sphereCollider.enabled = true;
            }
            else
            {
                meshRenderer.enabled = false;
                sphereCollider.enabled = false;
            }
    }

}
