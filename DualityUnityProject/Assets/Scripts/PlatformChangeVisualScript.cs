using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformChangeVisualScript : MonoBehaviour
{
    public Animation anim;

    public int operatingState; //0 is once-off, 1 is on and off.
    bool isActive;
    bool isReadyToSwap = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive != this.GetComponent<PlatformChangeScript>().getIsActive())
        {
            isActive = !isActive;
            if (isActive)
            {
                StartAnimation();
            }
            else
            {
                StopAnimation();
            }
        }
    }

    void StopAnimation()
    {
        anim.Stop();
    }

    void StartAnimation()
    {
        anim.Play();
    }
}
