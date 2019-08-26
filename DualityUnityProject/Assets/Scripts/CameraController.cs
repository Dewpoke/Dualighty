using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject lightPlayerOrb;
    public GameObject darkPlayerOrb;

    float startSize = 10;
    float zoomModifier = 0.05f;

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
        this.transform.position = (lightPlayerOrb.transform.position + darkPlayerOrb.transform.position) / 2 + Vector3.back * 10;
        this.GetComponent<Camera>().orthographicSize = startSize * (lightPlayerOrb.transform.position - darkPlayerOrb.transform.position).magnitude * zoomModifier;
    }
}
