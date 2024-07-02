using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class Scale : MonoBehaviour
{
    public GameObject scalingObject;

    private Slider slider;
    private Vector3 defaultScaling;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        defaultScaling = scalingObject.transform.localScale;
    }

    //scaling object. If slider is at 0.5 the scale factor is 1
    //e.g. if silder is at 0.8 the factor is 1.3 and mulitplied by the defaultScaling value
    public void scaleObject()
    { 
        Vector3 scalingVec = new Vector3(defaultScaling.x * (0.5f + slider.value), defaultScaling.y * (0.5f + slider.value), defaultScaling.z * (0.5f + slider.value));
        scalingObject.transform.localScale = scalingVec;
    }
}
