using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour
{
    public GameObject textInstructions;

    // Start is called before the first frame update
    void Start()
    {
        textInstructions.SetActive(false);
    }

    void OnTriggerEnter2D (Collider2D col)
    {
  if (col.gameObject.tag == "Player")
        {
            textInstructions.SetActive(true);
            StartCoroutine("WaitforSecondsToDisplay");
        }
    }
    IEnumerator WaitforSecondsToDisplay()
    {
        yield return new WaitForSeconds(3);
        textInstructions.SetActive(false);
    }
}
