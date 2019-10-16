using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOrbAnimationController : MonoBehaviour
{
    LightOrbController lightPlayerScript;

    public Animation [] animationArr;
    public Animator animator;

    bool direction; //false = left facing, true = right facing

    // Start is called before the first frame update
    void Start()
    {
        lightPlayerScript = this.GetComponent<LightOrbController>();

        //animator = this.transform.GetChild(0).transform.GetChild(1).GetComponent<Animator>();
        animator = this.transform.GetChild(1).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lightPlayerScript.GetYSpeed() > 0.2f && !lightPlayerScript.GetIsGrounded())//if the player is moving up and is not grounded
        {
            JumpAnimation();
        }
        else if (Mathf.Abs(lightPlayerScript.GetXSpeed()) > 0.5f) //if moving
        {
            RunAnimation();
        }
        else
        {
            IdleAnimation();
        }

        ChangeDirectionCheck();
    }

    void ChangeDirectionCheck()
    {
        if (direction)//if right facing
        {
            if (lightPlayerScript.GetXSpeed() < -0.5f)
            {
                direction = false;
                this.transform.eulerAngles = new Vector3 (0, 0, 0);
            }
        }
        else//if left facing
        {
            if (lightPlayerScript.GetXSpeed() > 0.5f)
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
    }

    void RunAnimation()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Walking", true);
        animator.SetBool("Jumping", false);
    }

    void JumpAnimation()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Walking", false);
        animator.SetBool("Jumping", true);
    }

}
