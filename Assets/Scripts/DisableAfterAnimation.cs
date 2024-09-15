using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterAnimation : MonoBehaviour
{
    public bool playDisableAnim;
    Animator ac;
    // Start is called before the first frame update
    void Start()
    {
        ac = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playDisableAnim)
        {
            ac.SetTrigger("Disable");
            playDisableAnim = false;
        }
    }

    public void PlayDisableAnimation()
    {
        playDisableAnim = true;
        ac.StopPlayback();
    }

    public void DisableObject()
    {
        this.gameObject.SetActive(false);
    }
}
