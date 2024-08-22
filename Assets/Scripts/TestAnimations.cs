using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAnimations : MonoBehaviour
{
    Animator ac;
    Toggle comboToggle;
    bool combo;
    
    // Start is called before the first frame update
    void Start()
    {
        ac = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        combo = comboToggle.isOn;    
    }

    public void PlayAttackAnimation(int number)
    {
        ac.SetFloat("AttackNumber", number);
        ac.SetTrigger("Attack");
    }
}
