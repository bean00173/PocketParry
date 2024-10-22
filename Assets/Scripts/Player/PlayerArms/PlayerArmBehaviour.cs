using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerArmBehaviour : MonoBehaviour
{
    public UnityEvent onFinisherComplete = new UnityEvent();

    Animator ac;
    SoundHandler soundHandler;
    float x, y;

    public bool attacking;

    // Start is called before the first frame update
    void Start()
    {
        ac = this.GetComponent<Animator>();
        soundHandler = this.GetComponent<SoundHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        ac.SetFloat("x", x);
        ac.SetFloat("y", y);
        ac.SetBool("Attacking", attacking);
    }

    public void Parry(ParryDirection dir)
    {
        switch (dir)
        {
            case ParryDirection.Up: x = 0; y = 1; break; //
            case ParryDirection.Down: x = 0; y = -1; break; // 
            case ParryDirection.Left: x = -1; y = 0; break; // 
            case ParryDirection.Right: x = 1; y = 0; break; //  
            case ParryDirection.LeftUp: x = -1; y = 1; break; //  
            case ParryDirection.LeftDown: x = -1; y = -1; break; //  
            case ParryDirection.RightUp: x = 1; y = 1; break; //  
            case ParryDirection.RightDown: x = 1; y = -1; break; //  
        }

        ac.SetTrigger("Parry");
        soundHandler.PlayRandomSound("player_swing");
    }

    //public void ResetFinish()
    //{
    //    finish = false;
    //}

    public void InvokeFinisherComplete()
    {
        onFinisherComplete.Invoke();
    }

}
