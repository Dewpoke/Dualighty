using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject lightPlayerOrb;
    public GameObject darkPlayerOrb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StayBetweenTheTwo(); 
    }

    void StayBetweenTheTwo()
    {
        this.transform.position = (lightPlayerOrb.transform.position + darkPlayerOrb.transform.position) / 2 + Vector3.back * 10; ;
    }
}
