using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOrbController : OrbController
{
    //Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        //rb = this.GetComponent<Rigidbody2D>();

        jumpSpeed = 5;

        gravity = 5f;
        moveAcceleration = 5f;
        stopDeceleration = 6f;
        maxMoveSpeed = 5f;
        maxFallSpeed = 5f;

        xVelocity = 0;
        yVelocity = 0;
}

    // Update is called once per frame
    void Update()
    {
        if (isControlsActive)
        {
            if (!InOppositeBackgroundCheck())
            {
                LightOrbControls();
            }
            else
            {
                LightOrbControlsNerfed();
            }
        }
        //RoundColliderLeftWallDetection();
        //RoundColliderRightWallDetection();

        //RoundColliderFloorDetection();

    }

    private void FixedUpdate()
    {
        PhysicsStuff();
    }

    void LightOrbControls()
    {


        if (Input.GetKey(KeyCode.A))
        {
            if (xVelocity > -maxMoveSpeed)
            {
                xVelocity -= moveAcceleration * Time.deltaTime;
                //print("Moving Left");
            }

        }
        if (Input.GetKey(KeyCode.D))
        {
            if (xVelocity < maxMoveSpeed)
            {
                xVelocity += moveAcceleration * Time.deltaTime;
                //print("Moving right");
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                yVelocity = jumpSpeed;
                print("Jump!");
            }
        }
        
    }

    void LightOrbControlsNerfed()
    {


        if (Input.GetKey(KeyCode.A))
        {
            if (xVelocity > -maxMoveSpeed * slowPenalty)
            {
                xVelocity -= moveAcceleration * slowPenalty * Time.deltaTime;
                //print("Moving Left");
            }
            else
            {
                xVelocity += moveAcceleration * Time.deltaTime;
            }

        }
        if (Input.GetKey(KeyCode.D))
        {
            if (xVelocity < maxMoveSpeed * slowPenalty)
            {
                xVelocity += moveAcceleration * slowPenalty * Time.deltaTime;
                //print("Moving right");
            }
            else
            {
                xVelocity -= moveAcceleration * Time.deltaTime;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                yVelocity = jumpSpeed * 3/4f;
                print("Jump!");
            }
        }

    }


    void PhysicsStuff()
    {
        bool isTouchingFloor = RoundColliderFloorDetection();
        //bool isTouchingFloor = BoxColliderFloorDetection();

        bool isTouchingRoof = RoundColliderRoofDetection();
        bool isTouchingRightWall = RoundColliderRightWallDetection();
        bool isTouchingLeftWall = RoundColliderLeftWallDetection();

        isGrounded = isTouchingFloor;

        if (xVelocity > Mathf.Epsilon)//Moving right
        {
            if (isTouchingRightWall)
            {
                xVelocity = 0;
            }
        }
        else if (xVelocity < Mathf.Epsilon)//Moving left
        {
            if (isTouchingLeftWall)
            {
                xVelocity = 0;
            }
        }

        if (yVelocity > Mathf.Epsilon)//Moving up
        {
            if (isTouchingRoof)
            {
                yVelocity = 0;
            }
        }
        else if (yVelocity < Mathf.Epsilon)//Moving down
        {
            if (isTouchingFloor)
            {
                yVelocity = 0;
            }
        }

        this.transform.position = this.transform.position + new Vector3(xVelocity * Time.deltaTime, yVelocity * Time.deltaTime, 0);
        yVelocity = yVelocity - gravity * Time.fixedDeltaTime;
    }

    bool RoundColliderFloorDetection()
    {
        float startAngle = Mathf.Deg2Rad*(200);
        float endAngle = Mathf.Deg2Rad*(340);
        int numOfRays = 10;
        bool returnAnswer = false;

        RaycastHit2D hit;
        for (int i = 0; i < numOfRays + 1; i++)
        {
            Vector2 rayStartPos = new Vector2 (this.transform.position.x, this.transform.position.y) + new Vector2(0.5f * Mathf.Cos(startAngle + (endAngle - startAngle) * i/numOfRays ), 0.5f * Mathf.Sin(startAngle + (endAngle - startAngle) * i / numOfRays));
            //rayStartPos = rayStartPos.normalized * this.transform.lossyScale.magnitude/2;
            //print(rayStartPos);
            hit = Physics2D.Raycast(rayStartPos, Vector2.down, 0.05f, platformLayerMask);
            Debug.DrawRay(rayStartPos, Vector3.down, Color.red, 0.05f);
            
            if (hit.collider != null && hit.collider.tag != "LightWall") //Touching Ground
            {
                this.transform.position = new Vector3(this.transform.position.x, hit.point.y + 0.5f, this.transform.position.z);
                //if ()
                //{

                //}
                //print(hit.collider.name);
                //print(hit.point);
                returnAnswer = true;
                return returnAnswer;
                //yVelocity = 0;
                //break;
            }
        }
        return returnAnswer;
    }

    bool RoundColliderRoofDetection()
    {
        float startAngle = Mathf.Deg2Rad * (20);
        float endAngle = Mathf.Deg2Rad * (160);
        int numOfRays = 10;
        bool returnAnswer = false;

        RaycastHit2D hit;
        for (int i = 0; i < numOfRays + 1; i++)
        {
            Vector2 rayStartPos = new Vector2(this.transform.position.x, this.transform.position.y) + new Vector2(0.5f * Mathf.Cos(startAngle + (endAngle - startAngle) * i / numOfRays), 0.5f * Mathf.Sin(startAngle + (endAngle - startAngle) * i / numOfRays));
            //rayStartPos = rayStartPos.normalized * this.transform.lossyScale.magnitude/2;
            //print(rayStartPos);
            hit = Physics2D.Raycast(rayStartPos, Vector2.up, 0.05f, platformLayerMask);
            Debug.DrawRay(rayStartPos, Vector3.up, Color.magenta, 0.05f);

            if (hit.collider != null && hit.collider.tag != "LightWall")
            {
                //print(hit.collider.name);
                returnAnswer = true;
                return returnAnswer;
                //yVelocity = 0;
                //break;
            }
        }
        return returnAnswer;
    }

    bool RoundColliderLeftWallDetection()
    {
        float startAngle = Mathf.Deg2Rad * (110);
        float endAngle = Mathf.Deg2Rad * (250);
        int numOfRays = 10;
        bool returnAnswer = false;

        RaycastHit2D hit;
        for (int i = 0; i < numOfRays + 1; i++)
        {
            Vector2 rayStartPos = new Vector2(this.transform.position.x, this.transform.position.y) + new Vector2(0.5f * Mathf.Cos(startAngle + (endAngle - startAngle) * i / numOfRays), 0.5f * Mathf.Sin(startAngle + (endAngle - startAngle) * i / numOfRays));
            //rayStartPos = rayStartPos.normalized * this.transform.lossyScale.magnitude/2;
            //print(rayStartPos);
            hit = Physics2D.Raycast(rayStartPos, Vector2.left, 0.05f, platformLayerMask);
            Debug.DrawRay(rayStartPos, Vector3.left, Color.green, 0.05f);

            if (hit.collider != null && hit.collider.tag != "LightWall")
            {
                //print(hit.collider.name);
                returnAnswer = true;
                return returnAnswer;
                //yVelocity = 0;
                //break;
            }
        }
        return returnAnswer;
    }

    bool RoundColliderRightWallDetection()
    {
        float startAngle = Mathf.Deg2Rad * (290);
        float endAngle = Mathf.Deg2Rad * (70 + 360);
        int numOfRays = 10;
        bool returnAnswer = false;

        RaycastHit2D hit;
        for (int i = 0; i < numOfRays + 1; i++)
        {
            Vector2 rayStartPos = new Vector2(this.transform.position.x, this.transform.position.y) + new Vector2(0.5f * Mathf.Cos(startAngle + (endAngle - startAngle) * i / numOfRays), 0.5f * Mathf.Sin(startAngle + (endAngle - startAngle) * i / numOfRays));
            //rayStartPos = rayStartPos.normalized * this.transform.lossyScale.magnitude/2;
            //print(rayStartPos);
            hit = Physics2D.Raycast(rayStartPos, Vector2.right, 0.05f, platformLayerMask);
            Debug.DrawRay(rayStartPos, Vector3.right, Color.yellow, 0.05f);

            if (hit.collider != null && hit.collider.tag != "LightWall")
            {
                //print(hit.collider.name);
                returnAnswer = true;
                return returnAnswer;
                //yVelocity = 0;
                //break;
            }
        }
        return returnAnswer;
    }

    bool BoxColliderFloorDetection()
    {
        float startValue = -0.5f;
        float endValue = 0.5f;
        int numOfRays = 10;
        bool returnAnswer = false;

        RaycastHit2D hit;
        for (int i = 0; i < numOfRays + 1; i++)
        {
            Vector2 rayStartPos = new Vector2(this.transform.position.x, this.transform.position.y) + new Vector2(startValue + (endValue - startValue) * i / numOfRays, -0.5f);
            //rayStartPos = rayStartPos.normalized * this.transform.lossyScale.magnitude/2;
            //print(rayStartPos);
            hit = Physics2D.Raycast(rayStartPos, Vector2.down, 0.05f, platformLayerMask);
            Debug.DrawRay(rayStartPos, Vector3.down, Color.red, 0.05f);

            if (hit.collider != null && hit.collider.tag != "LightWall") //If it hits something that isn't a light wall
            {
                print(hit.collider.name);
                returnAnswer = true;
                return returnAnswer;
                //yVelocity = 0;
                //break;
            }
        }
        return returnAnswer;
    }

    bool InOppositeBackgroundCheck()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(this.transform.position, Vector2.down, 0.5f, backGroundLayerMask);
        Debug.DrawRay(this.transform.position, Vector3.down, Color.black);

        if (hit.collider != null && hit.collider.tag != "LightBackground")
        {
            return true;
        }
        return false;
    }

    public void SetControlsActive (bool state)
    {
        isControlsActive = state;
    }

    public void SwapControlsActive()
    {
        isControlsActive = !isControlsActive;
    }
}
