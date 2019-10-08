using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{

    public static SoundEffects Instance = null;
    //public AudioClip openDoor;
   // public AudioClip flapWings;
    public AudioClip passageDoor;
    
<<<<<<< HEAD:DualityUnityProject/Assets/Sound/SoundEffects.cs
   AudioSource source;
=======
     AudioSource source;
>>>>>>> 6ead6442d7218a16478a7ec3f00a10a4ffea7852:DualityUnityProject/Assets/Sound/SoundEffects.cs
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
<<<<<<< HEAD:DualityUnityProject/Assets/Sound/SoundEffects.cs
       // source.PlayOneShot(openDoor);
=======
        //source.PlayOneShot(openDoor);
>>>>>>> 6ead6442d7218a16478a7ec3f00a10a4ffea7852:DualityUnityProject/Assets/Sound/SoundEffects.cs
        //source.PlayOneShot(flapWings);
        source.PlayOneShot(passageDoor);
    }
}
