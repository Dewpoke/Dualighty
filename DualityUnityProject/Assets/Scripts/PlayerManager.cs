using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        //RespawnPlayers();
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

        if (Input.GetKeyDown(KeyCode.K))
        {
            RespawnPlayers();
        }

        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            SceneManager.LoadScene(0);

        }
        else if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            SceneManager.LoadScene(2);

        }
         else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            SceneManager.LoadScene(4);

        }


    }

    void SwapPlayer() //NB: If both are inactive and canSwap == true, then both will become active. Opposite is also true
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
