using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafDoorAnimationScript : MonoBehaviour
{
    Animation leafDoor;

    public GameObject[] inputButtonsArr; //The button inputs (if they exist)

    public bool usesAND; //If the inputs are AND or OR
    [SerializeField]
    bool isActive; //If the overall input is 'ON'
    bool hasBeenTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        leafDoor = this.GetComponent<Animation>();
        leafDoor.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        isActive = CheckIsActive();
        if (!hasBeenTriggered && isActive)
        {
            print("ping");
            hasBeenTriggered = true;
            leafDoor.Play();
        }
    }

    bool CheckIsActive()
    {
        if (usesAND)
        {
            for (int i = 0; i < inputButtonsArr.Length; i++)
            {
                if (!inputButtonsArr[i].GetComponent<ButtonScript>().GetIsActive())//if one is OFF
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            for (int i = 0; i < inputButtonsArr.Length; i++)
            {
                if (inputButtonsArr[i].GetComponent<ButtonScript>().GetIsActive())//if one is ON
                {
                    return true;
                }
            }
            return false;
        }
    }
}
