using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilParticleScript : MonoBehaviour
{
    public GameObject runParticles;
    public GameObject jumpParticles;

    ParticleSystem runPartSys;
    ParticleSystem jumpPartSys;

    DarkOrbController darkScript;
    // Start is called before the first frame update
    void Start()
    {
        darkScript = this.GetComponent<DarkOrbController>();

        runPartSys = runParticles.GetComponent<ParticleSystem>();
        jumpPartSys = jumpParticles.GetComponent<ParticleSystem>();

        runPartSys.Stop();
        jumpPartSys.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (darkScript.GetIsGrounded() && Mathf.Abs(darkScript.GetXSpeed()) > 0.2f)//is walking
        {
            runPartSys.Play();
            jumpPartSys.Stop();
        }
        else if (darkScript.GetIsGrounded() && Input.GetKeyDown(KeyCode.Space) && darkScript.getIsControlsActive())//jumping
        {
            runPartSys.Stop();
            jumpPartSys.Play();

        }
        else
        {
            runPartSys.Stop();
            jumpPartSys.Stop();
        }
    }
}