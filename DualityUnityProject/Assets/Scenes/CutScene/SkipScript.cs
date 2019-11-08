using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkipScript : MonoBehaviour
{
    public GameObject Skip;

    public void Play()
    {
        SceneManager.LoadScene("LevelOne");
    }
}

