using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkipScript1 : MonoBehaviour
{
    public GameObject Skip;

    public void Play()
    {
        SceneManager.LoadScene("LevelTwo");
    }
}

