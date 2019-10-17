using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAreaOffset : MonoBehaviour
{
    GameObject mainCamera;

    [SerializeField]
    Vector2 cameraOffset;
    [SerializeField]
    float zoomOffset;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AdjustCameraOffsets()
    {
        mainCamera.GetComponent<CameraController>().SetDestinationOffset(cameraOffset);
        mainCamera.GetComponent<CameraController>().SetZoomOffset(zoomOffset);
    }

    void ClearCameraOffsets()
    {
        mainCamera.GetComponent<CameraController>().SetDestinationOffset(Vector3.zero);
        mainCamera.GetComponent<CameraController>().SetZoomOffset(0);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.gameObject.layer == 11)//Angel layer
            {
                LightOrbController script = collision.GetComponent<LightOrbController>();
                if (script.getIsControlsActive())
                {
                    AdjustCameraOffsets();
                }
            }
            else
            {
                DarkOrbController script = collision.GetComponent<DarkOrbController>();
                if (script.getIsControlsActive())
                {
                    AdjustCameraOffsets();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.gameObject.layer == 11)//Angel layer
            {
                LightOrbController script = collision.GetComponent<LightOrbController>();
                if (script.getIsControlsActive())
                {
                    ClearCameraOffsets();
                }
            }
            else
            {
                DarkOrbController script = collision.GetComponent<DarkOrbController>();
                if (script.getIsControlsActive())
                {
                    ClearCameraOffsets();
                }
            }
        }
    }

}
