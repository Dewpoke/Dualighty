using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneExit2 : MonoBehaviour
{

    public float Countdown = 22f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Countdown -= Time.deltaTime;
        if (Countdown <= 0f)
        {
            SceneManager.LoadScene("Menu");
        }

    }
}
