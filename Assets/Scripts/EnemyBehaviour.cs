using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Enemy
{
    [Range(0f, 10f)]
    public int health;
    public string name;
    [Range(0, 1f)]
    public float comboChance;
    public int comboLengthMin;
    public int comboLengthMax;
    public float damage;
    public float atkSpeed;
}

public enum EnemyState
{
    Idle,
    Attacking,
    Dead

}

public class EnemyBehaviour : MonoBehaviour
{
    bool vulnerable; 
    float elapsedTime;
    float parryStart, parryEnd;

    public Slider slider;
    public AttackInformation attackInformation;
    public Enemy enemyStats;

    AttackInfo currentClipInfo;
    Animator ac;

    int comboProgress;
    int comboLength;
    bool canAttack;
    bool doCombo;

    public EnemyState currentState { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        ac = this.GetComponent<Animator>();
        PlayerInput.Instance.inputHandled.AddListener(Parry);

        StartCoroutine(CooldownTimer(2.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (canAttack)
        {
            canAttack = false;

            doCombo = DoChanceCalculation(this.enemyStats.comboChance);
            ac.SetBool("Combo", doCombo);
            PlayAttack(0);

            if (!doCombo)
            {
                StartCoroutine(CooldownTimer(5.0f / enemyStats.atkSpeed));
            }
            else
            {
                comboLength = Random.Range(enemyStats.comboLengthMin, enemyStats.comboLengthMax);
            }

            currentState = EnemyState.Attacking;
        }

        if (currentState == EnemyState.Attacking && comboProgress == comboLength && comboProgress > 0)
        {
            Debug.Log($"COMBO DONE, Can Attack? {canAttack}");
            ac.SetBool("Combo", false);
            StartCoroutine(CooldownTimer(5.0f / enemyStats.atkSpeed));
        }

        Debug.Log($"Combo Progress : {comboProgress} / {comboLength}");
    }

    private void AttackReady()
    {
        canAttack = true;
        doCombo = false;
        comboLength = 0;
        comboProgress = 0;
    }

    public void PlayAttack(int num)
    {
        ac.SetFloat("AttackNumber", num);
        ac.SetFloat("TransitionNumber", num);
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

    private IEnumerator CooldownTimer(float time)
    {
        currentState = EnemyState.Idle;
        yield return new WaitForSeconds(time);
        AttackReady();
    }

    private bool DoChanceCalculation(float chance)
    {
        return Random.value <= chance;
    }

    private int SelectAttack()
    {
        List<AttackInfo> potentialAttacks = new List<AttackInfo>();

        foreach(AttackInfo attack in attackInformation.attackInfo)
        {
            if (doCombo)
            {
                if (attack.canCombo) potentialAttacks.Add(attack);
            }
            else
            {
                potentialAttacks.Add(attack);
            }
        }

        return Random.Range(0, potentialAttacks.Count);

    }

    public void UpdateTransitionNumber()
    {
        int acNum = (int)ac.GetFloat("TransitionNumber");
        int acAtkNum = (int)ac.GetFloat("AttackNumber");
        if (comboProgress > 0) acNum = acAtkNum; ac.SetFloat("TransitionNumber", acNum);
        comboProgress++;
    }

    public void UpdateAttackNumber()
    {
        int acNum = (int)ac.GetFloat("AttackNumber");
        if (acNum == 0 || acNum % 2 == 0)
        {
            acNum++;
            ac.SetFloat("AttackNumber", acNum);
        }
        else
        {
            acNum--;
            ac.SetFloat("AttackNumber", acNum);
        }
    }

}
