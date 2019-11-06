using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkOrbController : OrbController
{
    //Rigidbody2D rb;
    public GameObject darkGlow;

    public bool isCharging = false;

    float standardMoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        //rb = this.GetComponent<Rigidbody2D>();

        /*jumpSpeed = 15;
        gravity = 5f;
        moveAcceleration = 5f;
        stopDeceleration = 6f;
        maxMoveSpeed = 5f;
        maxFallSpeed = 10f;*/

        xVelocity = 0;
        yVelocity = 0;

        standardMoveSpeed = maxMoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isControlsActive)
        {
            darkGlow.SetActive(true);
            if (!InOppositeBackgroundCheck())
            {
                DarkOrbControls();
            }
            else
            {
                DarkOrbControlsNerfed();
            }
        }
        else
        {//Temp fix for slowing down orb aaaand now also used for the glow
            darkGlow.SetActive(false);
        }

    }

    private void FixedUpdate()
    {
        PhysicsStuff();
    }

    void DarkOrbControls()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (xVelocity > -maxMoveSpeed)
            {
                xVelocity -= moveAcceleration * Time.deltaTime;
                //print("Moving Left");
            }

        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (xVelocity < maxMoveSpeed)
            {
                xVelocity += moveAcceleration * Time.deltaTime;
                //print("Moving right");
            }
        }
        else //Slow down if nothing is held
        {
            if (xVelocity > 0)//if moving right
            {
                xVelocity -= Mathf.Sign(xVelocity) * stopDeceleration * Time.deltaTime;
                xVelocity = Mathf.Clamp(xVelocity, 0, 1000);
            }
            else if (xVelocity < 0)//if moving left
            {
                xVelocity -= Mathf.Sign(xVelocity) * stopDeceleration * Time.deltaTime;
                xVelocity = Mathf.Clamp(xVelocity, -1000, 0);
            }
            else
            {
                xVelocity = 0;
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

        if (Input.GetKey(KeyCode.LeftShift))
        {
            maxMoveSpeed = standardMoveSpeed * 1.5f;
        }
        else
        {
            maxMoveSpeed = standardMoveSpeed;
        }

    }

    void DarkOrbControlsNerfed()
    {

        print("DARK NERFED");
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
        else if (Input.GetKey(KeyCode.D))
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
        //bool isTouchingRoof = RoundColliderRoofDetection();
        bool isTouchingRoof = BoxColliderRoofDetection();

        //bool isTouchingFloor = RoundColliderFloorDetection();
        bool isTouchingFloor = BoxColliderFloorDetection();

        //bool isTouchingRightWall = RoundColliderRightWallDetection();
        bool isTouchingRightWall = BoxColliderRightWallDetection();

        //bool isTouchingLeftWall = RoundColliderLeftWallDetection();
        bool isTouchingLeftWall = BoxColliderLeftWallDetection();

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

        if (!isControlsActive)//slow down
        {
            if (xVelocity > 0)//if moving right
            {
                xVelocity -= Mathf.Sign(xVelocity) * stopDeceleration * Time.fixedDeltaTime;
                xVelocity = Mathf.Clamp(xVelocity, 0, 1000);
            }
            else if (xVelocity < 0)//if moving left
            {
                xVelocity -= Mathf.Sign(xVelocity) * stopDeceleration * Time.fixedDeltaTime;
                xVelocity = Mathf.Clamp(xVelocity, -1000, 0);
            }
            else
            {
                xVelocity = 0;
            }
        }

        this.transform.position = this.transform.position + new Vector3(xVelocity * Time.deltaTime, yVelocity * Time.deltaTime, 0);
        if (yVelocity > -maxFallSpeed)
        {
            yVelocity = yVelocity - gravity * gravity * Time.fixedDeltaTime;
        }
        else//ensure the player cannot fall faster than allowed
        {
            yVelocity = -maxFallSpeed;
        }

        StepUpCheck();
    }

    bool RoundColliderFloorDetection()
    {
        float startAngle = Mathf.Deg2Rad * (200);
        float endAngle = Mathf.Deg2Rad * (340);
        int numOfRays = 10;
        bool returnAnswer = false;

        RaycastHit2D hit;
        for (int i = 0; i < numOfRays + 1; i++)
        {
            Vector2 rayStartPos = new Vector2(this.transform.position.x, this.transform.position.y) + new Vector2(0.5f * Mathf.Cos(startAngle + (endAngle - startAngle) * i / numOfRays), 0.5f * Mathf.Sin(startAngle + (endAngle - startAngle) * i / numOfRays));
            //rayStartPos = rayStartPos.normalized * this.transform.lossyScale.magnitude/2;
            //print(rayStartPos);
            hit = Physics2D.Raycast(rayStartPos, Vector2.down, 0.05f, platformLayerMask);
            Debug.DrawRay(rayStartPos, Vector3.down, Color.red, 0.05f);

            if (hit.collider != null && hit.collider.tag != "DarkWall")
            {
                this.transform.position = new Vector3(this.transform.position.x, hit.point.y + 0.5f, this.transform.position.z);
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

            if (hit.collider != null && hit.collider.tag != "DarkWall")
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

            if (hit.collider != null && hit.collider.tag != "DarkWall")
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

            if (hit.collider != null && hit.collider.tag != "DarkWall")
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
        float startValue = -0.45f;
        float endValue = 0.45f;
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

            if (hit.collider != null && hit.collider.tag != "DarkWall") //If it hits something that isn't a dark wall
            {
                BasicPlatformScript script = hit.collider.GetComponentInParent<BasicPlatformScript>();
                if (script != null) //If the floor is actually a moving platform, follow along
                {
                    print(script.GetMoveSpeedAndDir());
                    this.transform.position += script.GetMoveSpeedAndDir() * Time.fixedDeltaTime;
                    //xVthiselocity += script.GetMoveSpeedAndDir().x * Time.fixedDeltaTime;
                    //yVelocity += script.GetMoveSpeedAndDir().y * Time.fixedDeltaTime;
                }

                //print(hit.collider.name);
                hit = Physics2D.Raycast(rayStartPos + Vector2.up * 0.5f, Vector2.down, 0.6f, platformLayerMask); //Check the point at which the floor is hit
                if (hit.collider.tag != "DarkWall" && !BoxColliderRoofDetection())//If it hits the platform AND the character is not touching the roof
                    this.transform.position = new Vector3(this.transform.position.x, hit.point.y + 0.5f, this.transform.position.z);
                return true;
                //yVelocity = 0;
                //break;
            }
        }
        return returnAnswer;
    }

    bool BoxColliderRoofDetection()
    {
        float startValue = -0.45f;
        float endValue = 0.45f;
        int numOfRays = 10;
        bool returnAnswer = false;

        RaycastHit2D hit;
        for (int i = 0; i < numOfRays + 1; i++)
        {
            Vector2 rayStartPos = new Vector2(this.transform.position.x, this.transform.position.y) + new Vector2(startValue + (endValue - startValue) * i / numOfRays, 1.1f);
            //rayStartPos = rayStartPos.normalized * this.transform.lossyScale.magnitude/2;
            //print(rayStartPos);
            hit = Physics2D.Raycast(rayStartPos, Vector2.up, 0.05f, platformLayerMask);
            Debug.DrawRay(rayStartPos, Vector3.up, Color.magenta, 0.05f);

            if (hit.collider != null && hit.collider.tag != "DarkWall") //If it hits something that isn't a dark wall
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

    bool BoxColliderLeftWallDetection()
    {
        float startValue = -0.4f;
        float endValue = 1.1f;
        int numOfRays = 15;
        bool returnAnswer = false;

        RaycastHit2D hit;
        for (int i = 0; i < numOfRays + 1; i++)
        {
            Vector2 rayStartPos = new Vector2(this.transform.position.x, this.transform.position.y) + new Vector2(-0.5f, startValue + (endValue - startValue) * i / numOfRays);
            //rayStartPos = rayStartPos.normalized * this.transform.lossyScale.magnitude/2;
            //print(rayStartPos);
            hit = Physics2D.Raycast(rayStartPos, Vector2.left, 0.05f, platformLayerMask);
            Debug.DrawRay(rayStartPos, Vector3.left, Color.yellow, 0.05f);

            if (hit.collider != null && hit.collider.tag != "DarkWall") //If it hits something that isn't a dark wall
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

    bool BoxColliderRightWallDetection()
    {
        float startValue = -0.4f;
        float endValue = 1.1f;
        int numOfRays = 15;
        bool returnAnswer = false;

        RaycastHit2D hit;
        for (int i = 0; i < numOfRays + 1; i++)
        {
            Vector2 rayStartPos = new Vector2(this.transform.position.x, this.transform.position.y) + new Vector2(0.5f, startValue + (endValue - startValue) * i / numOfRays);
            //rayStartPos = rayStartPos.normalized * this.transform.lossyScale.magnitude/2;
            //print(rayStartPos);
            hit = Physics2D.Raycast(rayStartPos, Vector2.right, 0.05f, platformLayerMask);
            Debug.DrawRay(rayStartPos, Vector3.right, Color.blue, 0.05f);

            if (hit.collider != null && hit.collider.tag != "DarkWall") //If it hits something that isn't a dark wall
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

    void StepUpCheck()
    {
        if (xVelocity > 0.2f)//if moving right
        {
            Vector2 rayStartPos = new Vector2(this.transform.position.x, this.transform.position.y) + new Vector2(0.45f, 0f);
            RaycastHit2D hit = Physics2D.Raycast(rayStartPos, Vector2.down, 0.5f, platformLayerMask);
            Debug.DrawLine(rayStartPos, rayStartPos + Vector2.down * 0.5f, Color.white,0.01f);

            if (hit.collider != null && hit.collider.tag != "DarkWall") //If it hits something that isn't a dark wall
            {
                this.transform.position = new Vector3(this.transform.position.x, hit.point.y + 0.5f, this.transform.position.z);

            }
        }
        else if (xVelocity < -0.2f)//if moving left
        {
            Vector2 rayStartPos = new Vector2(this.transform.position.x, this.transform.position.y) + new Vector2(-0.45f, 0f);
            RaycastHit2D hit = Physics2D.Raycast(rayStartPos, Vector2.down, 0.5f, platformLayerMask);
            Debug.DrawLine(rayStartPos, rayStartPos + Vector2.down * 0.5f, Color.white, 0.01f);

            if (hit.collider != null && hit.collider.tag != "DarkWall") //If it hits something that isn't a dark wall
            {
                this.transform.position = new Vector3(this.transform.position.x, hit.point.y + 0.5f, this.transform.position.z);

            }
        }
        
    }

    bool InOppositeBackgroundCheck()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(this.transform.position, Vector2.down, 0.5f, backGroundLayerMask);
        Debug.DrawRay(this.transform.position, Vector3.down, Color.black);

        if (hit.collider != null && hit.collider.tag != "DarkBackground") //if the background is something that is not dark
        {
            //print("FOREIGN WALL!!");
            return true;
        }
        return false;
    }

    public void SetControlsActive(bool state)
    {
        isControlsActive = state;
    }

    public void StopMomentum()
    {
        xVelocity = 0;
        yVelocity = 0;
    }

    public bool getIsControlsActive()
    {
        return isControlsActive;
    }

    public float GetXSpeed()
    {
        return xVelocity;
    }

    public float GetYSpeed()
    {
        return yVelocity;
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Interactable")
        {
            if (isControlsActive)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    collision.gameObject.GetComponent<ButtonScript>().InteractWithButton();
                }
            }
        }
    }
}
