using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

[System.Serializable]
public class Enemy
{
    [Range(0f, 50f)]
    public int health;
    public string name;
    [Range(0, 1f)]
    public float comboChance;
    public int comboLengthMin;
    public int comboLengthMax;
    public float feintChance;
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

    public AttackInformation attackInformation;
    public Enemy enemyStats;

    AttackInfo currentClipInfo;
    Animator ac;

    int comboProgress;
    int comboLength;
    bool canAttack;
    bool doCombo;
    int score;


    [HideInInspector]
    public UnityEvent<float> onParrySuccessful, onVulnerable, onInVulnerable, onHitTaken; // slider events

    public EnemyState currentState { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        ac = this.GetComponent<Animator>();
        PlayerInput.Instance.inputHandled.AddListener(Parry); // add listener to input event

        StartCoroutine(CooldownTimer(2.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (canAttack) // if the enemy can attack
        {
            canAttack = false; // prevent from doing so again

            doCombo = DoChanceCalculation(this.enemyStats.comboChance); // check if the attack can combo

            //ac.speed = 1 + Random.value; // potential for random changes in how fast the attacks play

            ac.SetBool("Combo", doCombo); // update animator 
            PlayAttack(SelectAttack()); // select attack

            if (!doCombo) // if not comboing, automatically begin the cooldown timer
            {
                StartCoroutine(CooldownTimer(5.0f / enemyStats.atkSpeed));
            }
            else
            {
                comboLength = Random.Range(enemyStats.comboLengthMin, enemyStats.comboLengthMax); // choose a random combo length between variables min and max
            }

            currentState = EnemyState.Attacking; // update state
        }

        if (currentState == EnemyState.Attacking && comboProgress == comboLength && comboProgress > 0) // if the enemy is attacking and the combo has reached its end
        {
            ac.SetBool("Combo", false); // update animator
            StartCoroutine(CooldownTimer(5.0f / enemyStats.atkSpeed)); // do cooldown
        }

        CombatManager.instance.scoreText.text = score.ToString(); // updates test ui score text
    }

    public void PlayAttack(int num) // plays an attack with the designated number
    {
        ac.SetFloat("AttackNumber", num); // set relevant ac params using designated atk number
        ac.SetFloat("TransitionNumber", num);
        ac.SetTrigger("Attack");
    }

    public void Vulnerable() // animation event driven method for start of parry period
    {
        vulnerable = true; // set vulnerable bool
        parryStart = GetCurrentAnimatorTime(); // set start time var

        CombatManager.instance.tempDetector.color = Color.green;

        onVulnerable.Invoke(parryStart);
        
        foreach(AttackInfo info in attackInformation.attackInfo) // for each potential attack, cross reference to check with current attack to retrieve attack data
        {
            AnimatorClipInfo[] clipInfo = ac.GetCurrentAnimatorClipInfo(0);
            if (clipInfo[0].clip.name == info.clip.name)
            {
                currentClipInfo = info;
            }
            
        }

    }

    public void InVulnerable() // animation event driven method for end of parry period
    {
        if (!vulnerable)
        {
            parryEnd = GetCurrentAnimatorTime(); // set end time var
            score += DetermineScoreAmount(); // increment score based on timing performance

            onInVulnerable.Invoke(parryEnd); // corresponding event trigger

        }
        else
        {
            parryEnd = GetCurrentAnimatorTime();
            vulnerable = false; // set vulnerable bool
            score--;

            onHitTaken.Invoke(parryEnd); // corresponding event trigger
        }

        CombatManager.instance.tempDetector.color = Color.red;

    }

    public float GetCurrentAnimatorTime() // utility method for returning the current time in the animator
    {
        return ac.GetCurrentAnimatorStateInfo(0).normalizedTime; 
    }

    public void Parry(ParryDirection dir, float time) // method that listens for player inputs
    {
        if (vulnerable && CheckInputMatch(dir)) // checks for parry conditionals
        {
            Debug.Log($"Elapsed Animator Time : {GetCurrentAnimatorTime()}, Actual Parry Time : {GetCurrentAnimatorTime() - time}");
            elapsedTime = GetCurrentAnimatorTime() - time; // sets parry time
            //score += DetermineScoreAmount(); // increment score based on timing performance
            vulnerable = false; // makes invulnerable

            onParrySuccessful.Invoke(elapsedTime);
        }
        else
        {
            elapsedTime = 0; // if not a parry make sure slider doesnt update
        }
    }

    private bool CheckInputMatch(ParryDirection dir) // utility method to check if input direction matches the required direction in current attack info
    {
        return dir == currentClipInfo.parryDirection; 
    }

    private int DetermineScoreAmount() // PROVISIONAL METHOD FOR SCALING SCORE BASED ON TIMING PERFORMANCE
    {
        float parryTime = elapsedTime - parryStart;
        float vulnerableTime = (parryEnd - parryStart) / 2;
        float multiplier = parryTime / vulnerableTime;

        if (multiplier > 1)
        {
            multiplier = 1 - (multiplier - 1);
        }

        if (multiplier >= .9f) return 10;
        else if (multiplier >= .75f) return 5;
        else if (multiplier >= .5f) return 2;
        else return 1;
    }

    private IEnumerator CooldownTimer(float time) // cooldown timer for after an attack has been executed, preventing attacks too soon after
    {
        currentState = EnemyState.Idle;
        yield return new WaitForSeconds(time); // LOOK AT LATER - Will probably add randomisation
        AttackReady();
    }

    private void AttackReady() // calls when cooldown is complete
    {
        //ac.speed = 1;
        canAttack = true; // resets a bunch of variables to prepare for new attacks
        doCombo = false;
        comboLength = 0;
        comboProgress = 0;
    }

    private bool DoChanceCalculation(float chance) // utility method for calculating random chance
    {
        return Random.value <= chance;
    }

    private int SelectAttack() // method that selects which attack to play
    {
        List<AttackInfo> potentialAttacks = new List<AttackInfo>(); // create list var

        foreach(AttackInfo attack in attackInformation.attackInfo) // cycle through each attack in the current attack info list
        {
            if (doCombo) // if the attack is to start a combo, it must be an attack that is able to combo (bool available in attackinfo)
            {
                if (attack.canCombo) potentialAttacks.Add(attack);
            }
            else
            {
                potentialAttacks.Add(attack); // if not comboing just add the attack to the list (will add all)
            }
        }

        return Random.Range(0, potentialAttacks.Count); // out of the new list select a random value

    }

    public void UpdateTransitionNumber() // animation event driven method for updating which transition will need to play after the current attack
    {
        int acNum = (int)ac.GetFloat("TransitionNumber"); // return current values
        int acAtkNum = (int)ac.GetFloat("AttackNumber");
        if (comboProgress > 0) acNum = acAtkNum; ac.SetFloat("TransitionNumber", acNum); // set new values
        comboProgress++; // update combo progress
    }

    public void UpdateAttackNumber() // animation event drive method for updating which attack will need to play after the current transition
    {
        int acNum = (int)ac.GetFloat("AttackNumber"); // return current attack value
        if (acNum == 0 || acNum % 2 == 0) // since combos are in pairs in the attack data, check if divisible by 2 (exc. 0) 
        {
            acNum++;
            ac.SetFloat("AttackNumber", acNum); // if conditions met move to the next attack
        }
        else
        {
            acNum--;
            ac.SetFloat("AttackNumber", acNum); // if not move back to the previous attack 
        }
    }

}
