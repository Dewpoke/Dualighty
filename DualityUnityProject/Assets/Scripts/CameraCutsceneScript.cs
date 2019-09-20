using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCutsceneScript : MonoBehaviour
{
    GameObject[] travelPointsArr; //The points the camera must pan to
    public float[] travelTimeArr; //The time it takes to get from this point to the next
    public float[] travelDelayArr; //The time it must wait AFTER arriving at the point
    public float[] zoomLevelArr; //The zoom level of the camera
    int currNode = 0; //The current node the camera is at (or rather just left)
    float perc = 0; //Used to lerp position + zoom
    float currDelayTime = 0;
    bool isDelayOn = false;

    public GameObject[] inputButtonsArr;
    [SerializeField]
    bool usesAND = true; //If the inputs are AND or OR
    [SerializeField]
    bool isActive = false; //If the overall input is 'ON'
    bool isAtEnd = false; //If the platform is at the end AND the delay has completed


    Camera thisCam;
    Camera mainCam;



    private void Start()
    {
        mainCam = Camera.main;
        thisCam = this.transform.GetChild(0).GetComponent<Camera>();
        thisCam.enabled = false;
        travelPointsArr = new GameObject[this.transform.childCount - 1];//initialise the travel points
        for (int i = 0; i < this.transform.childCount - 1; i++)
        {
            print(i);
            travelPointsArr[i] = this.transform.GetChild(i + 1).gameObject;
        }
    }

    private void Update()
    {
        isActive = CheckIsActive();
        if (isActive)
        {
            if (isAtEnd)
            {
                thisCam.enabled = false;
                mainCam.enabled = true;
            }
            else
            {
                mainCam.enabled = false;
                thisCam.enabled = true;
                MoveToNextZoomAndDelay();
            }
        }
    }

    bool CheckIsActive() //Check if the inputs return an 'ON' or 'OFF'
    {
        if (usesAND) //Uses AND
        {
            for (int i = 0; i < inputButtonsArr.Length; i++)//Loop through the inputs
            {
                if (!inputButtonsArr[i].GetComponent<ButtonScript>().GetIsActive())//If even one input is false
                {
                    return false;
                }
            }//Else if all are true
            return true;
        }
        else //Uses OR
        {
            for (int i = 0; i < inputButtonsArr.Length; i++)//Loop through the inputs
            {
                if (inputButtonsArr[i].GetComponent<ButtonScript>().GetIsActive())//If even one input is true
                {
                    return true;
                }
            } //Else if all are false
            return false;
        }
    }


    void MoveToNextZoomAndDelay()
    {
        if (isDelayOn)//Basic wait function. Resets correct variables on reaching wait time.
        {
            currDelayTime += Time.deltaTime;
            if (currDelayTime > travelDelayArr[currNode])
            {
                print(currNode);
                currDelayTime = 0;
                isDelayOn = false;
                currNode++;

                if (currNode + 1 >= travelPointsArr.Length)//Notify if the end is reached (if the currNode tries to be above or equal to
                {
                    isAtEnd = true;
                }
            }
        }
        else //Not waiting, ie. moving forward.
        {
            if (currNode + 1 < travelPointsArr.Length) //If the current node is not the last node (ie. if not at the end) (Redundant check but okay)
            {
                perc += Time.deltaTime/travelTimeArr[currNode];
                this.transform.GetChild(0).transform.position = Vector3.Lerp(travelPointsArr[currNode].transform.position, travelPointsArr[currNode + 1].transform.position, perc); //Move the camera
                thisCam.orthographicSize = Mathf.Lerp(zoomLevelArr[currNode], zoomLevelArr[currNode + 1], perc);
                if (perc >= 1) //if at the next node
                {
                    currDelayTime = 0;
                    isDelayOn = true;
                    perc = 0;
                }
            }
        }

    }
}

