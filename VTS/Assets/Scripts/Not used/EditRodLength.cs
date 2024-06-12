using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditRodLength : MonoBehaviour
{
    public Transform rod;
    public float maxY;

    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void changeLength()
    {
        Vector3 newScale = rod.localScale;
        newScale.y = maxY * slider.value;
        rod.localScale = newScale;
    }
}
