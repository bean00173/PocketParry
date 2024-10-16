using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class UILevelButton : MonoBehaviour
{
    Animator ac;
    public UnityEvent onActivated = new UnityEvent();
    public UnityEvent onDeactivated = new UnityEvent();

    public bool btnEnabled;
    public bool ButtonEnabled
    {
        get
        {
            return btnEnabled;
        }
        set
        {
            if (this.levelInformation.endless)
            {
                try
                {
                    this.GetComponentInChildren<GreyedOut>().FadeOut();
                }
                catch (System.Exception e)
                {
                    Debug.Log($"{e.GetType()} : No component of type <GreyedOut> Found");
                }
            }

            btnEnabled = value;
        }
    }
    public Animator highlight;
    public LevelInformation levelInformation;

    public GameObject progress;

    private int endlessScore;

    public bool canClick;

    //public Animator greyedOut;

    //public UILevelButton otherButton;
    // Start is called before the first frame update
    void Start()
    {
        ac = this.GetComponent<Animator>();
        highlight.speed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateProgress(int score)
    {
        if (this.levelInformation.endless)
        {
            //if(endlessScore < score)
            //{
                
            //    progress.GetComponent<TextMeshProUGUI>().text = $"Highscore : {score}";
            //}

            //LeaderboardManager.instance.UploadEntry(GameManager.Instance.playerName, score);
        }
        else
        {
            progress.SetActive(true);
        }
    }

    //public void GreyedOut(string animation)
    //{
    //    if (!buttonEnabled)
    //    {
    //        greyedOut.Play(animation);
    //    }
    //}

    public void ManualDisable()
    {
        if (highlight.GetCurrentAnimatorStateInfo(0).IsName("FadeIn"))
        {
            onDeactivated.Invoke();
            UIManager.Instance.playBtn.gameObject.SetActive(false);
            highlight.Play("FadeOut");
        }
            
    }

    public void ToggleActive()
    {
        if (ButtonEnabled && canClick)
        {
            if (highlight.GetCurrentAnimatorStateInfo(0).IsName("FadeIn"))
            {
                onDeactivated.Invoke();
                UIManager.Instance.playBtn.gameObject.SetActive(false);
                highlight.Play("FadeOut");
            }
            else
            {
                onActivated.Invoke();
                UIManager.Instance.playBtn.gameObject.SetActive(true);
                highlight.Play("FadeIn");
                GameManager.Instance.UpdateLevelInformation(levelInformation);
            }
        }

        //if (buttonEnabled)
        //{
        //    if (!ac.GetCurrentAnimatorStateInfo(0).IsName("OnClick"))
        //    {
        //        ac.Play("OnClick");

        //        if (otherButton.ac.GetCurrentAnimatorStateInfo(0).IsName("OnClick"))
        //        {
        //            otherButton.OtherActive();
        //        }
        //    }
        //    else
        //    {
        //        ac.Play("Disable");
        //    }

        //}
    }

    public void Deactivate()
    {
        highlight.Play("FadeOut");
    }

    //public void OtherActive()
    //{
    //    ac.Play("Disable");
    //}
}
