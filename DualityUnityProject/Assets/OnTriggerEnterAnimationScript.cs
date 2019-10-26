using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnterAnimationScript : MonoBehaviour
{
    Animation animToPlay;

    // Start is called before the first frame update
    void Start()
    {
        animToPlay = this.GetComponent<Animation>();
        animToPlay.Stop();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animToPlay.Play();
    }
}
