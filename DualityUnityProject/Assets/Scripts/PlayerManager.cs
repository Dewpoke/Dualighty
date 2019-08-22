using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool canSwap;
    public bool startOnLightOrb;
    bool lightOrbActive = false;

    public GameObject lightPlayerOrb;
    public GameObject darkPlayerOrb;

    void Start()
    {
        lightPlayerOrb.GetComponent<LightOrbController>().SetControlsActive(startOnLightOrb);
        darkPlayerOrb.GetComponent<DarkOrbController>().SetControlsActive(!startOnLightOrb);
    }

    // Update is called once per frame
    void Update()
    {
        if (canSwap)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SwapPlayer();
                print("swapping");
            }
        }
    }

    void SwapPlayer()
    {
        lightPlayerOrb.GetComponent<LightOrbController>().SwapControlsActive();
        darkPlayerOrb.GetComponent<DarkOrbController>().SwapControlsActive();
    }
}
