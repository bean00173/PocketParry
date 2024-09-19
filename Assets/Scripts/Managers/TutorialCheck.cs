using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCheck : MonoBehaviour
{
    public GameObject tutorialCheckUI;
    // Start is called before the first frame update
    void Start()
    {
        BeginGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginGame()
    {
        if (!GameManager.Instance.tutorialPlayed)
        {
            tutorialCheckUI.SetActive(true);
        }
        else
        {
            GameManager.Instance.LoadScene("MainMenu");
        }
    }
}
