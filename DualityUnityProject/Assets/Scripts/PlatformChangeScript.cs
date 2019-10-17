using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformChangeScript : MonoBehaviour
{
    public GameObject[] inputButtonsArr; //The button inputs (if they exist)
    public bool usesAND; //If the inputs are AND or OR
    [SerializeField]
    bool isActive; //If the overall input is 'ON'

    public Sprite lightWallSprite;
    public Sprite darkWallSprite;

    public int startColour; //0 for neutral, 1 for light, 2 for dark
    public int endColour;

    private void Start()
    {
        StartCoroutine(DrawLineToConnectedButtons());
    }

    // Update is called once per frame
    void Update()
    {
        isActive = CheckIsActive();
        if (isActive)//If the switch is ON
        {
            switch (endColour)//Change to end colour
            {
                case 0:
                    ChangeToNeutral();
                    break;
                case 1:
                    ChangeToLight();
                    break;
                case 2:
                    ChangeToDark();
                    break;
            }
        }
        else//If the switch is OFF
        {
            switch (startColour)//change to start colour
            {
                case 0:
                    ChangeToNeutral();
                    break;
                case 1:
                    ChangeToLight();
                    break;
                case 2:
                    ChangeToDark();
                    break;

            }
        }
    }

    void ChangeToLight()
    {
        this.tag = "LightWall";
        this.GetComponent<SpriteRenderer>().sprite = lightWallSprite;
    }

    void ChangeToDark()
    {
        this.tag = "DarkWall";
        this.GetComponent<SpriteRenderer>().sprite = darkWallSprite;
    }

    void ChangeToNeutral()
    {
        this.tag = "Untagged";

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

    public bool getIsActive()
    {
        return isActive;
    }

    IEnumerator DrawLineToConnectedButtons()
    {
        while (true)
        {
            for (int i = 0; i < inputButtonsArr.Length; i++)
            {
                Debug.DrawLine(this.transform.position, inputButtonsArr[i].transform.position, Color.blue, 5);
            }
            yield return new WaitForSeconds(5);
        }
    }
}


