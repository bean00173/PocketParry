using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelButton : MonoBehaviour
{
    Animator ac;
    public bool buttonEnabled;
    public Animator highlight;
    public LevelInformation levelInformation;
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

    //public void GreyedOut(string animation)
    //{
    //    if (!buttonEnabled)
    //    {
    //        greyedOut.Play(animation);
    //    }
    //}

    public void ToggleActive()
    {
        if (buttonEnabled)
        {
            if (highlight.GetCurrentAnimatorStateInfo(0).IsName("FadeIn"))
            {
                UIManager.Instance.playBtn.gameObject.SetActive(false);
                highlight.Play("FadeOut");
            }
            else
            {
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

    //public void OtherActive()
    //{
    //    ac.Play("Disable");
    //}
}
