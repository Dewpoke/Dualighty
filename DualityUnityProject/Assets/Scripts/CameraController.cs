using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject lightPlayerOrb;
    public GameObject darkPlayerOrb;

    float startSize = 10;
    float zoomModifier = 0.05f;

    bool isFollowingLight = true; //if the camera is following the Light orb or not

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
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
            this.transform.position = Vector3.Lerp(this.transform.position, lightPlayerOrb.transform.position + Vector3.back * 50, 0.2f);
        else
            this.transform.position = Vector3.Lerp(this.transform.position,  darkPlayerOrb.transform.position + Vector3.back * 50, 0.2f);
    }
}
