using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILevelButton : MonoBehaviour
{
    Animator ac;
    // Start is called before the first frame update
    void Start()
    {
        ac = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PointerEnter()
    {
        ac.SetBool("Hover", true);
    }

    public void PointerExit()
    {
        ac.SetBool("Hover", false);
    }
}
