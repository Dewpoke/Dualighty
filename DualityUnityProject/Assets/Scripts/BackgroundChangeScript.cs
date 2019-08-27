﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChangeScript : MonoBehaviour
{
    public GameObject[] inputButtonsArr; //The button inputs (if they exist)
    public bool usesAND; //If the inputs are AND or OR
    [SerializeField]
    bool isActive; //If the overall input is 'ON'

    public Sprite lightBackgroundSprite;
    public Sprite darkBackgroundSprite;

    public bool startLight;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isActive = CheckIsActive();
        if (isActive)//If the switch is ON
        {
            if (startLight)//If the background starts as Light
            {
                ChangeToDark();
            }
            else//If the background starts as Dark
            {
                ChangeToLight();
            }
        }
        else//If the switch is OFF
        {
            if (startLight)//If the background starts as Light
            {
                ChangeToLight();
            }
            else//If the background starts as Dark
            {
                ChangeToDark();
            }
        }
    }

    void ChangeToLight()
    {
        this.tag = "LightBackground";
        this.GetComponent<SpriteRenderer>().sprite = lightBackgroundSprite;
    }

    void ChangeToDark()
    {
        this.tag = "DarkBackground";
        this.GetComponent<SpriteRenderer>().sprite = darkBackgroundSprite;
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
