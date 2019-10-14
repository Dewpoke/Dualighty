using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{

    public static SoundEffects Instance = null;
    //public AudioClip openDoor;
   // public AudioClip flapWings;
    public AudioClip passageDoor;
    

   AudioSource source;

    

    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
        AudioSource theSource = GetComponent<AudioSource>();
        source = theSource;
    }

   public void PlayOneShot(AudioClip clip)
    {

        source.PlayOneShot(passageDoor);
    }
}
