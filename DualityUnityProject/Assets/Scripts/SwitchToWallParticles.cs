using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToWallParticles : MonoBehaviour
{
    public float travelTime = 5;
    float pulseDelay = 3;

    public GameObject redParticles;
    public GameObject yellowParticles;

    GameObject[] inputSwitches;

    PlatformChangeScript script;

    // Start is called before the first frame update
    void Start()
    {
        script = this.GetComponent<PlatformChangeScript>();
        inputSwitches = script.inputButtonsArr;

        StartCoroutine(SendPulse());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Spawn item, Send from switch to wall, when item reaches end despawn it, delay for seconds, repeat
    IEnumerator SendPulse()
    {
        yield return new WaitForSeconds(pulseDelay);

        float perc = 0;
        GameObject [] particlesArr = new GameObject [inputSwitches.Length];
        for (int i = 0; i < inputSwitches.Length; i++) //Instantiate the particle gameobjects
        {
            if (script.getIsActive())
            {
                if (script.startColour == 1)//If active and start colour is yellow
                    particlesArr[i] = Instantiate(redParticles, inputSwitches[i].transform.position, Quaternion.identity);
                else
                    particlesArr[i] = Instantiate(yellowParticles, inputSwitches[i].transform.position, Quaternion.identity);
            }
            else
            {
                if (script.startColour == 1)//If inactive and start colour is yellow
                    particlesArr[i] = Instantiate(yellowParticles, inputSwitches[i].transform.position, Quaternion.identity);
                else
                    particlesArr[i] = Instantiate(redParticles, inputSwitches[i].transform.position, Quaternion.identity);
            }
        }
               
        while (perc < 1)//move
        {
            for (int i = 0; i < inputSwitches.Length; i++)
            {
                particlesArr[i].transform.position = Vector3.Lerp(inputSwitches[i].transform.position, this.transform.position, perc);
            }
            perc += 1 / travelTime * Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < inputSwitches.Length; i++)
        {
            Destroy(particlesArr[i]);
        }

        StartCoroutine(SendPulse());//Start it again
    }

}
