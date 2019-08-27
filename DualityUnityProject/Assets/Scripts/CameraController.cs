using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject lightPlayerOrb;
    public GameObject darkPlayerOrb;

    float startSize = 10;
    float zoomModifier = 0.05f;

    bool isFollowingLight = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer(); 
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isFollowingLight = !isFollowingLight;
        }
    }

    void StayBetweenTheTwo()
    {
        this.transform.position = (lightPlayerOrb.transform.position + darkPlayerOrb.transform.position) / 2 + Vector3.back * 10;
        this.GetComponent<Camera>().orthographicSize = startSize * (lightPlayerOrb.transform.position - darkPlayerOrb.transform.position).magnitude * zoomModifier;
    }

    void FollowPlayer()
    {
        if (isFollowingLight)
            this.transform.position = lightPlayerOrb.transform.position + Vector3.back * 10;
        else
            this.transform.position = darkPlayerOrb.transform.position + Vector3.back * 10;
    }
}
