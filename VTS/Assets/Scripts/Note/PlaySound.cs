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

    public void playSound()
    {
        if (audioSource.clip != null)
        {
            audioSource.Play();

            //for debugging
            //Debug.Log("PlaySound");
        }
    }

    //change the audioclip by load the sound with the given name out of the folder vts_sounds 
    public void changeSound(string audioName)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sounds/vts_sounds/" + audioName);
        audioSource.clip = clip;
    }
}
