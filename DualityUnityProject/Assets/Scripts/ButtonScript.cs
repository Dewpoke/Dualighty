using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public int triggerMode; //0 is stay-on-button, 1 is once-off

    public bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
        if (CheckForPlayer()) //If the player is on the button
        {
            switch (triggerMode)
            {
                case 0:
                    isActive = true;
                    break;
                case 1:
                    isActive = true;
                    break;
            }
        }
        else //if the player is not on the button
        {
            switch (triggerMode)
            {
                case 0:
                    isActive = false;
                    break;
                case 1:
                    break;
            }
        }
    }

    void ButtonPressed()
    {
        switch (triggerMode)
        {
            case 0:
                isActive = true;
                break;
            case 1:
                break;
        }
    }

    bool CheckForPlayer()
    {
        RaycastHit2D hit;
        Vector2 rayStartPos = this.transform.position + new Vector3(-0.5f, 0.4f);
        //rayStartPos = rayStartPos.normalized * this.transform.lossyScale.magnitude/2;

        hit = Physics2D.Raycast(rayStartPos, Vector2.right, 1f);
        
        Debug.DrawRay(rayStartPos, Vector3.right, Color.magenta, 1f);

        if (hit.collider != null && hit.collider.tag == "Player")
        {
            //print(hit.collider.gameObject);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GetIsActive()
    {
        return isActive;
    }
}
