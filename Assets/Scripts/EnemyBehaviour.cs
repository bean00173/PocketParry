using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    Animator ac;
    bool vulnerable;
    bool parry;

    float elapsedTime;

    float parryStart, parryEnd;

    public Slider slider;
    public Button parryButton;

    AttackInfo currentClipInfo;

    public AttackInformation attackInformation;

    // Start is called before the first frame update
    void Start()
    {
        ac = this.GetComponent<Animator>();
        PlayerInput.Instance.inputHandled.AddListener(Parry);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAttack(int num)
    {
        ac.SetFloat("AttackNumber", num);
        ac.SetTrigger("Attack");
    }

    public void Vulnerable()
    {
        vulnerable = true;
        parryStart = GetCurrentAnimatorTime();
        
        foreach(AttackInfo info in attackInformation.attackInfo)
        {
            AnimatorClipInfo[] clipInfo = ac.GetCurrentAnimatorClipInfo(0);
            if (clipInfo[0].clip.name == info.clip.name)
            {
                Debug.Log("huh");
                currentClipInfo = info;
            }
            
        }

    }

    public void InVulnerable()
    {
        vulnerable = false;
        parryEnd = GetCurrentAnimatorTime();

        slider.minValue = parryStart;
        slider.maxValue = parryEnd;
        slider.value = elapsedTime;
    }

    public float GetCurrentAnimatorTime()
    {
        return ac.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public void Parry(ParryDirection dir)
    {
        if (vulnerable && CheckInputMatch(dir))
        {
            Debug.Log("Parried");
            elapsedTime = GetCurrentAnimatorTime();
            vulnerable = false;
        }
        else
        {
            elapsedTime = 0;
        }
    }

    private bool CheckInputMatch(ParryDirection dir)
    {
        return dir == currentClipInfo.parryDirection;

    }
}
