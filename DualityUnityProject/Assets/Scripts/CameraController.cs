using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject lightPlayerOrb;
    public GameObject darkPlayerOrb;

    float startSize = 10;
    float zoomModifier = 0.05f;

    Vector3 destinationPoint;
    Vector3 destinationPointOffset;

    float zoom;
    float zoomOffset;

    bool isFollowingLight = true; //if the camera is following the Light orb or not

    // Start is called before the first frame update
    void Start()
    {
        zoom = this.GetComponent<Camera>().orthographicSize;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        FollowPlayer(); 
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isFollowingLight = !isFollowingLight;
            destinationPointOffset = Vector3.zero;
            zoomOffset = 0;
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
        {
            destinationPoint = lightPlayerOrb.transform.position + new Vector3(0, 2.5f, -50);
            destinationPoint += destinationPointOffset;
            this.transform.position = Vector3.Lerp(this.transform.position, destinationPoint, 0.2f);
            //this.GetComponent<Camera>().orthographicSize = Mathf.Lerp(zoom, zoom + zoomOffset, 0.2f);
            this.GetComponent<Camera>().orthographicSize = zoom + zoomOffset;
        }
        else
        {
            destinationPoint = darkPlayerOrb.transform.position + new Vector3(0, 2.5f, -50);
            destinationPoint += destinationPointOffset;
            this.transform.position = Vector3.Lerp(this.transform.position, destinationPoint, 0.2f);
            //this.GetComponent<Camera>().orthographicSize = Mathf.Lerp(zoom, zoom + zoomOffset, 0.2f);
            this.GetComponent<Camera>().orthographicSize = zoom + zoomOffset;
        }
    }

    public void SetDestinationOffset(Vector3 newOffset)
    {
        destinationPointOffset = newOffset;
    }

    public void SetZoomOffset(float newOffset)
    {
        zoomOffset = newOffset;
    }
}
