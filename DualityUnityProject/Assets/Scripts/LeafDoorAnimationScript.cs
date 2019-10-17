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

    public float delayTime = 5;
    // Start is called before the first frame update
    void Start()
    {
        leafDoor = this.GetComponent<Animation>();
        leafDoor.Stop();

        //For level designing/debugging
        StartCoroutine(DrawLineToConnectedButtons());
    }

    // Update is called once per frame
    void Update()
    {
        isActive = CheckIsActive();
        if (!hasBeenTriggered && isActive)
        {
            StartCoroutine (DelayAndActivateAnimation());
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

    IEnumerator DelayAndActivateAnimation()
    {
        hasBeenTriggered = true;
        yield return new WaitForSeconds(delayTime);
        print("ping");
        leafDoor.Play();
    }

    IEnumerator DrawLineToConnectedButtons()
    {
        while (true)
        {
            for (int i = 0; i < inputButtonsArr.Length; i++)
            {
                Debug.DrawLine(this.transform.position, inputButtonsArr[i].transform.position, Color.green, 5);
            }
            yield return new WaitForSeconds(5);
        }
    }
}
