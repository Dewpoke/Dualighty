using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelParticleScript : MonoBehaviour
{
    public GameObject runParticles;
    public GameObject jumpParticles;

    ParticleSystem runPartSys;
    ParticleSystem jumpPartSys;

    LightOrbController lightScript;
    // Start is called before the first frame update
    void Start()
    {
        lightScript = this.GetComponent<LightOrbController>();

        runPartSys = runParticles.GetComponent<ParticleSystem>();
        jumpPartSys = jumpParticles.GetComponent<ParticleSystem>();

        runPartSys.Stop();
        jumpPartSys.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (lightScript.GetIsGrounded() && Mathf.Abs(lightScript.GetXSpeed()) > 0.2f)//is walking
        {
            runPartSys.Play();
            jumpPartSys.Stop();
        }
        else if (lightScript.GetIsGrounded() && Input.GetKeyDown(KeyCode.Space) && lightScript.getIsControlsActive())//jumping
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
