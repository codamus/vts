using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySound : MonoBehaviour
{

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound()
    {
        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
    }

    public void changeSound(string audioName)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sounds/vts_sounds/" + audioName);
        if (clip != null)
        {
            audioSource.clip = clip;
        }
    }
}
