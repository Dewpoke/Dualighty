using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool canSwap;
    public bool startOnLightOrb;
    bool lightOrbActive = false;
    bool darkOrbActive = false;

    public GameObject lightPlayerOrb;
    public GameObject darkPlayerOrb;

    void Start()
    {
        lightOrbActive = startOnLightOrb;
        darkOrbActive = !startOnLightOrb;
        lightPlayerOrb.GetComponent<LightOrbController>().SetControlsActive(startOnLightOrb);
        darkPlayerOrb.GetComponent<DarkOrbController>().SetControlsActive(!startOnLightOrb);

        RespawnPlayers();
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
        lightOrbActive = !lightOrbActive;
        lightPlayerOrb.GetComponent<LightOrbController>().SetControlsActive(lightOrbActive);
        darkOrbActive = !darkOrbActive;
        darkPlayerOrb.GetComponent<DarkOrbController>().SetControlsActive(darkOrbActive);
    }

    public void RespawnPlayers()
    {
        //move players to spawn point, then make them stop moving
        lightPlayerOrb.transform.position = GameObject.Find("CheckpointManager").GetComponent<CheckpointManagerScript>().GetActiveCheckpoint().transform.GetChild(0).transform.position + Vector3.back * 10;
        darkPlayerOrb.transform.position = GameObject.Find("CheckpointManager").GetComponent<CheckpointManagerScript>().GetActiveCheckpoint().transform.GetChild(1).transform.position + Vector3.back * 10;

        lightPlayerOrb.GetComponent<LightOrbController>().StopMomentum();
        darkPlayerOrb.GetComponent<DarkOrbController>().StopMomentum();
    }
}
