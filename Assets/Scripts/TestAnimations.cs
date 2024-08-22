using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAnimations : MonoBehaviour
{
    Animator ac;
    public Toggle comboToggle;
    bool combo;

    int comboId;
    Attack currentAttack;
    
    // Start is called before the first frame update
    void Start()
    {
        ac = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        combo = comboToggle.isOn;
        ac.SetBool("Combo", combo);
    }

    public void PlayAttackAnimation(int num)
    {
        AttackType type = (AttackType)num;
        currentAttack = TestAttackData.instance.GetAttackData(type);
        ac.SetFloat("AttackNumber", currentAttack.id);
        if (combo)
        {
            comboId = currentAttack.comboId;
        }
        ac.SetTrigger("Attack");
    }

    public void ComboTransition()
    {
        ac.SetFloat("AttackNumber", comboId);
    }
}
