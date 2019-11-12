using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLandAnimation : MonoBehaviour
{
    Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animation>();
        anim.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayAnimation()
    {
        anim.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayAnimation();
            print ("Pls Work");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayAnimation();
            print("Pls Work");
        }
    }
}
