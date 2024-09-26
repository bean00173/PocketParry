using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapLevel : MonoBehaviour
{
    public bool isUnlocked;
    //public UILevelButton campaignButton, endlessButton;
    private Animator ac;

    public LevelID levelId;
    public UnityEvent onHoverEnter, onHoverExit = new UnityEvent();
    public UILevelButton campaign, endless;

    // Start is called before the first frame update
    void Start()
    {
        //ac = this.GetComponent<Animator>();
        //onHoverExit.AddListener(campaignButton.ToggleActive);
        //onHoverExit.AddListener(endlessButton.ToggleActive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CampaignBeaten()
    {
        campaign.UpdateProgress(0);
        if(endless != null)
        {
            endless.ButtonEnabled = true;
        }
    }

    public void StoreEndlessScore(int score)
    {
        endless.UpdateProgress(score);
        // if endless score < score -> update
    }

    public void CenteredOnScreen(bool a)
    {
        if(ac == null)
        {
            ac = this.GetComponent<Animator>();
        }

        if(!a && ac.GetCurrentAnimatorStateInfo(0).IsName("Hover"))
        {
            ac.SetBool("Hover", a);
            onHoverExit.Invoke();

            if (isUnlocked)
            {
                campaign.canClick = false;
                if (endless != null)
                {
                    endless.canClick = false;
                }
            }  
        }
        else if (a)
        {
            ac.SetBool("Hover", a);
            onHoverEnter.Invoke();

            if (isUnlocked)
            {
                campaign.canClick = true;
                if (endless != null)
                {
                    endless.canClick = true;

                }
            }
        }

        //if (a)
        //{
        //    onHoverEnter.Invoke();
        //}
        //else
        //{
        //    onHoverExit.Invoke();
        //}
    }

}
