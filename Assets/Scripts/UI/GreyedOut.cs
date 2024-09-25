using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreyedOut : MonoBehaviour
{
    Animator ac;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeIn()
    {
        if(ac == null)
        {
            ac = this.GetComponent<Animator>();
        }

        if (!this.GetComponentInParent<UILevelButton>().buttonEnabled)
        {
            ac.Play("FadeIn");
        }
    }

    public void FadeOut()
    {
        if (!this.GetComponentInParent<UILevelButton>().buttonEnabled)
        {
            ac.Play("FadeOut");
        }
    }
}
