using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkOrbAnimationController : MonoBehaviour
{
    DarkOrbController darkPlayerScript;

    public Animation[] animationArr;
    public Animator animator;

    bool direction; //false = left facing, true = right facing
    bool wasAirborne = false;

    // Start is called before the first frame update
    void Start()
    {
        darkPlayerScript = this.GetComponent<DarkOrbController>();

       // animator = this.transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>();
        animator = this.transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (darkPlayerScript.GetYSpeed() > 0.2f && !darkPlayerScript.GetIsGrounded())//if the player is moving up and is not grounded
        {
            JumpAnimation();
            wasAirborne = true;
        }
        else if (darkPlayerScript.GetYSpeed() < 0 && !darkPlayerScript.GetIsGrounded())
        {
            FallAnimation();
            wasAirborne = true;
        }
        else if (darkPlayerScript.GetIsGrounded() && wasAirborne)
        {
            LandAnimation();
            wasAirborne = false;
        }
        else if (Mathf.Abs(darkPlayerScript.GetXSpeed()) > 0.5f) //if moving
        {
            RunAnimation();
            wasAirborne = false;
        }
        else
        {
            IdleAnimation();
            wasAirborne = false;
        }

        ChangeDirectionCheck();
    }

    void ChangeDirectionCheck()
    {
        if (direction)//if right facing
        {
            if (darkPlayerScript.GetXSpeed() < -0.5f)
            {
                direction = false;
                this.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        else//if left facing
        {
            if (darkPlayerScript.GetXSpeed() > 0.5f)
            {
                direction = true;
                this.transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
    }

    void IdleAnimation()
    {
        animator.SetBool("Idle", true);
        animator.SetBool("Walking", false);
        animator.SetBool("Jumping", false);
        animator.SetBool("Falling", false);
        animator.SetBool("Landing", false);
    }

    void RunAnimation()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Walking", true);
        animator.SetBool("Jumping", false);
        animator.SetBool("Falling", false);
        animator.SetBool("Landing", false);
    }

    void JumpAnimation()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Walking", false);
        animator.SetBool("Jumping", true);
        animator.SetBool("Falling", false);
        animator.SetBool("Landing", false);
    }

    void FallAnimation()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Walking", false);
        animator.SetBool("Jumping", false);
        animator.SetBool("Falling", true);
        animator.SetBool("Landing", false);
    }

    void LandAnimation()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Walking", false);
        animator.SetBool("Jumping", false);
        animator.SetBool("Falling", false);
        animator.SetBool("Landing", true);
        //print("Ping");
    }
}
