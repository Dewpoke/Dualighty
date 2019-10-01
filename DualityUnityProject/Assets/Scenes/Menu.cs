using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject options;
   public void Play()
    {
        SceneManager.LoadScene("CutScene");
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void OptionsMenu()
    {
        mainMenu.SetActive(false);
        options.SetActive(true);

    }
    public void MainMenu()
    {
        mainMenu.SetActive(true);
        options.SetActive(false);
    }
}
