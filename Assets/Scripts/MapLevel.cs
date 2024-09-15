using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapLevel : MonoBehaviour
{
    public bool isUnlocked;
    //public UILevelButton campaignButton, endlessButton;
    private Animator ac;

    public UnityEvent onHoverEnter, onHoverExit = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        ac = this.GetComponent<Animator>();
        //onHoverExit.AddListener(campaignButton.ToggleActive);
        //onHoverExit.AddListener(endlessButton.ToggleActive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CenteredOnScreen(bool a)
    {
        if(!a && ac.GetCurrentAnimatorStateInfo(0).IsName("Hover"))
        {
            ac.SetBool("Hover", a);
            onHoverExit.Invoke();
        }
        else if (a)
        {
            ac.SetBool("Hover", a);
            onHoverEnter.Invoke();
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
