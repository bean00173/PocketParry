using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapLevel : MonoBehaviour
{
    public bool isUnlocked;
    private Animator ac;

    public UnityEvent onHoverEnter, onHoverExit = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        ac = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CenteredOnScreen(bool a)
    {
        ac.SetBool("Hover", a);

        if (a)
        {
            onHoverEnter.Invoke();
        }
        else
        {
            onHoverExit.Invoke();
        }
    }

}
