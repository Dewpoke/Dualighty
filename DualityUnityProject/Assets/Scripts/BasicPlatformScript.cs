using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlatformScript : MonoBehaviour
{
    //Move back and forth between the points
    public GameObject[] inputButtonsArr; //The button inputs (if they exist)
    bool usesInputs; //If the platform has inputs
    public bool usesAND; //If the inputs are AND or OR
    [SerializeField]
    bool isActive; //If the overall input is 'ON'

    public int operatingMode; //0 is move to the end. 1 is move back and forth. 2 is move from last node to first node (loop)
    public bool isOffReverse; //If the platform must act in reverse if no signal is given, or if it must stop moving

    bool isAtEnd = false;

    bool isMovingForward = true;

    GameObject[] nodePositionsArr;//The positions the platform travels along to.
    int currNode = 0;//The current node the platform is traveling from (forwards)

    public float moveSpeed = 3; //How fast the platform moves
    public float pauseTime = 0; // How long the platform must wait at each node

    Vector3 moveSpeedAndDir;

    void Start()
    {
        if (inputButtonsArr.Length > 0)//Set if there are inputs
        {
            usesInputs = true;
            isActive = false;
            StartCoroutine(DrawLineToConnectedButtons());
        }
        else
        {
            usesInputs = false;
            isActive = true;
        }

        nodePositionsArr = new GameObject[this.transform.childCount - 1];//Set the node array size
        for (int i = 1; i < this.transform.childCount; i++) //i starts at 1, as 0 is reserved for the moving platform
        {
            nodePositionsArr[i-1] = this.transform.GetChild(i).gameObject;
            //print(nodePositionsArr[i - 1].name);
        }

        StartCoroutine(DrawLineToPlatform());
    }

    // Update is called once per frame
    void Update()
    {
        //print(isMovingForward);
    }

    private void FixedUpdate()
    {
        if (usesInputs)//Update to work with multiple
        {
           isActive = CheckIsActive();
        }
        switch (operatingMode)
        {
            case 0: //MoveToEnd mode
                if (isActive)//If ON
                {
                    if (currNode < 0)
                    {
                        currNode++;
                    }
                    MoveToEnd();

                }
                else
                {//If OFF
                    if (isOffReverse) //If in Reverse-active mode
                    {
                        if (currNode >= nodePositionsArr.Length - 1)
                        {
                            currNode--;
                        }
                        MoveToStart();
                    }
                    else
                    {
                        moveSpeedAndDir = Vector3.zero;
                    }
                }
                break;
            case 1: //MoveToEnd then MoveToStart
                if (isActive)
                {
                    if (isMovingForward)
                    {
                        MoveToEnd();
                        if (isAtEnd)
                        {
                            isMovingForward = !isMovingForward;
                            currNode--;
                        }
                    }
                    else
                    {
                        MoveToStart();
                        if (isAtEnd)
                        {
                            isMovingForward = !isMovingForward;
                            currNode++;
                        }
                    }
                }
                else
                {//If off
                    if (isOffReverse)
                    {
                        if (!isMovingForward)
                        {
                            MoveToEnd();
                            if (isAtEnd)
                            {
                                isMovingForward = !isMovingForward;
                                currNode--;
                            }
                        }
                        else
                        {
                            MoveToStart();
                            if (isAtEnd)
                            {
                                isMovingForward = !isMovingForward;
                                currNode++;
                            }
                        }
                    }
                    else
                    {
                        moveSpeedAndDir = Vector3.zero;
                    }
                }
                break;
            case 2: //Loop to beginning
                if (isActive)
                {
                    if (currNode < 0)
                    {
                        currNode = nodePositionsArr.Length - 1;
                    }
                    MoveToEndLoop();
                }
                else
                {
                    if (isOffReverse)
                    {
                        if (currNode >= nodePositionsArr.Length - 1){
                            currNode = -1;
                        }
                        MoveToStartLoop();
                    }
                    else
                    {
                        moveSpeedAndDir = Vector3.zero;
                    }
                }
                break;
        }

        //print(moveSpeedAndDir);
        //print(isAtEnd);
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

    void MoveToEnd()
    {
        if (currNode + 1 < nodePositionsArr.Length) //If the current node is not the last node (ie. if not at the end)
        {
            isAtEnd = false;
            float distanceToNextPoint = (this.transform.GetChild(0).transform.position - nodePositionsArr[currNode + 1].transform.position).magnitude;//The distance from the platform to the next node
            distanceToNextPoint -= moveSpeed * Time.fixedDeltaTime; //The distance left after moving a bit
            float perc = distanceToNextPoint / (nodePositionsArr[currNode].transform.position - nodePositionsArr[currNode + 1].transform.position).magnitude; //The distance left over the distance from point A to point B ie. The new distance as a percentage between the two points
            perc = 1 - perc; //As we're using the distance left, we need to invert 
            this.transform.GetChild(0).transform.position = Vector3.Lerp(nodePositionsArr[currNode].transform.position, nodePositionsArr[currNode + 1].transform.position, perc); //Move the platform

            moveSpeedAndDir = (nodePositionsArr[currNode + 1].transform.position - nodePositionsArr[currNode].transform.position).normalized * moveSpeed;
            if (distanceToNextPoint < 0) //if at the next node
            {
                currNode++;  
            }
        }
        else
        {
            isAtEnd = true;
            moveSpeedAndDir = Vector3.zero;
        }

    }

    void MoveToStart()
    {
        //print(currNode);
        isAtEnd = false;
        if (currNode + 1 > 0) //If the current node is not the last node (ie. if not at the end)
        {
            float distanceToNextPoint = (this.transform.GetChild(0).transform.position - nodePositionsArr[currNode].transform.position).magnitude;//The distance from the platform to the current node
            distanceToNextPoint -= moveSpeed * Time.fixedDeltaTime; //The distance left after moving a bit
            float perc = distanceToNextPoint / (nodePositionsArr[currNode].transform.position - nodePositionsArr[currNode + 1].transform.position).magnitude; //The distance left over the distance from point A to point B ie. The new distance as a percentage between the two points
            perc = 1 - perc; //As we're using the distance left, we need to invert 
            this.transform.GetChild(0).transform.position = Vector3.Lerp(nodePositionsArr[currNode + 1].transform.position, nodePositionsArr[currNode].transform.position, perc); //Move the platform

            moveSpeedAndDir = (nodePositionsArr[currNode].transform.position - nodePositionsArr[currNode + 1].transform.position).normalized * moveSpeed;
            if (distanceToNextPoint < 0)//If at the prev node
            {
                currNode--;
            }
        }
        else
        {
            isAtEnd = true;
            moveSpeedAndDir = Vector3.zero;
        }
    }

    void MoveToEndLoop()
    {
        if (currNode + 1 < nodePositionsArr.Length) //If the current node is not the last node (ie. if not at the end)
        {
            float distanceToNextPoint = (this.transform.GetChild(0).transform.position - nodePositionsArr[currNode + 1].transform.position).magnitude;//The distance from the platform to the next node
            distanceToNextPoint -= moveSpeed * Time.fixedDeltaTime; //The distance left after moving a bit
            float perc = distanceToNextPoint / (nodePositionsArr[currNode].transform.position - nodePositionsArr[currNode + 1].transform.position).magnitude; //The distance left over the distance from point A to point B ie. The new distance as a percentage between the two points
            perc = 1 - perc; //As we're using the distance left, we need to invert 
            this.transform.GetChild(0).transform.position = Vector3.Lerp(nodePositionsArr[currNode].transform.position, nodePositionsArr[currNode + 1].transform.position, perc); //Move the platform

            moveSpeedAndDir = (nodePositionsArr[currNode + 1].transform.position - nodePositionsArr[currNode].transform.position).normalized * moveSpeed;
            if (distanceToNextPoint < 0) //if at the next node
            {
                currNode++;
            }
        }
        else
        {//force it to go from the last node to the first
            float distanceToNextPoint = (this.transform.GetChild(0).transform.position - nodePositionsArr[0].transform.position).magnitude;//The distance from the platform to the next node
            distanceToNextPoint -= moveSpeed * Time.fixedDeltaTime; //The distance left after moving a bit
            float perc = distanceToNextPoint / (nodePositionsArr[currNode].transform.position - nodePositionsArr[0].transform.position).magnitude; //The distance left over the distance from point A to point B ie. The new distance as a percentage between the two points
            perc = 1 - perc; //As we're using the distance left, we need to invert 
            this.transform.GetChild(0).transform.position = Vector3.Lerp(nodePositionsArr[currNode].transform.position, nodePositionsArr[0].transform.position, perc); //Move the platform

            moveSpeedAndDir = (nodePositionsArr[0].transform.position - nodePositionsArr[currNode].transform.position).normalized * moveSpeed;
            if (distanceToNextPoint < 0) //if at the next node
            {
                currNode = 0;
            }
        }

    }

    void MoveToStartLoop()
    {
        //print(currNode);
        if (currNode + 1 > 0) //If the current node is not the last node (ie. if not at the end)
        {
            float distanceToNextPoint = (this.transform.GetChild(0).transform.position - nodePositionsArr[currNode].transform.position).magnitude;//The distance from the platform to the current node
            distanceToNextPoint -= moveSpeed * Time.fixedDeltaTime; //The distance left after moving a bit
            float perc = distanceToNextPoint / (nodePositionsArr[currNode].transform.position - nodePositionsArr[currNode + 1].transform.position).magnitude; //The distance left over the distance from point A to point B ie. The new distance as a percentage between the two points
            perc = 1 - perc; //As we're using the distance left, we need to invert 
            this.transform.GetChild(0).transform.position = Vector3.Lerp(nodePositionsArr[currNode + 1].transform.position, nodePositionsArr[currNode].transform.position, perc); //Move the platform

            moveSpeedAndDir = (nodePositionsArr[currNode].transform.position - nodePositionsArr[currNode + 1].transform.position).normalized * moveSpeed;
            if (distanceToNextPoint < 0)//If at the prev node
            {
                currNode--;
            }
        }
        else
        {
            float distanceToNextPoint = (this.transform.GetChild(0).transform.position - nodePositionsArr[nodePositionsArr.Length - 1].transform.position).magnitude;//The distance from the platform to the current node
            distanceToNextPoint -= moveSpeed * Time.fixedDeltaTime; //The distance left after moving a bit
            float perc = distanceToNextPoint / (nodePositionsArr[0].transform.position - nodePositionsArr[nodePositionsArr.Length - 1].transform.position).magnitude; //The distance left over the distance from point A to point B ie. The new distance as a percentage between the two points
            perc = 1 - perc; //As we're using the distance left, we need to invert 
            this.transform.GetChild(0).transform.position = Vector3.Lerp(nodePositionsArr[0].transform.position, nodePositionsArr[nodePositionsArr.Length - 1].transform.position, perc); //Move the platform

            moveSpeedAndDir = (nodePositionsArr[nodePositionsArr.Length - 1].transform.position - nodePositionsArr[0].transform.position).normalized * moveSpeed;
            if (distanceToNextPoint < 0)//If at the prev node
            {
                currNode = nodePositionsArr.Length-2;
            }
        }
    }

    public Vector3 GetMoveSpeedAndDir()
    {
        return moveSpeedAndDir;
    }

    IEnumerator DrawLineToConnectedButtons()
    {
        while (true)
        {
            for (int i = 0; i < inputButtonsArr.Length; i++)
            {
                Debug.DrawLine(this.transform.position, inputButtonsArr[i].transform.position, Color.red, 5);
            }
            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator DrawLineToPlatform()
    {
        while (true)
        { 
            Debug.DrawLine(this.transform.position, this.transform.GetChild(0).position, Color.magenta, 0.2f);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
