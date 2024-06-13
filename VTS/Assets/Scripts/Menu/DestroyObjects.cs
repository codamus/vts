using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DestroyObjects : MonoBehaviour
{
    //Collider to destroy objects
    private BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
