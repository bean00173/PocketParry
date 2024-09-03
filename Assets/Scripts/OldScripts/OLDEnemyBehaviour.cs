//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class OLDEnemyBehaviour : MonoBehaviour
//{
//    [HideInInspector] public OLDCombatManager cm;
//    public Enemy enemyStats;

//    public float parryCount;
//    bool canAttack;

//    Animator ac;
//    private bool combo;
//    private int comboStatus;

//    // Start is called before the first frame update
//    void Start()
//    {
//        cm = GetComponentInParent<OLDCombatManager>();
//        ac = this.GetComponent<Animator>();

//        StartCoroutine(AtkTimer(5.0f / enemyStats.atkSpeed));
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (canAttack)
//        {
//            OLDCombatManager.AtkType attack = enemyStats.potentialAttacks[Random.Range(0, enemyStats.potentialAttacks.Length)];
//            ac.SetTrigger($"{attack}Attack");

//            if(combo == true && comboStatus < enemyStats.comboLength)
//            {
//                comboStatus++;
//                StartCoroutine(AtkTimer(1.0f));
//            }
//            else if(Random.value > 1 - enemyStats.comboChance)
//            {
//                combo = true;
//                comboStatus++;
//                StartCoroutine(AtkTimer(1.0f));
//            }
//            else
//            {
//                combo = false;
//                StartCoroutine(AtkTimer(5.0f / enemyStats.atkSpeed));


//                //AnimatorClipInfo[] clipInfo = ac.GetCurrentAnimatorClipInfo(0);
//                //AnimationClip clip = clipInfo[0].clip;
//                //float atkTime = clip.events[0].time;
//            }
//        }
//    }

//    public void ParryStart()
//    {
//        cm.ParryStart(OLDCombatManager.AtkType.Left);
//    }

//    public void ParryEnd()
//    {
//        cm.ParryEnd();
//    }

//    private IEnumerator AtkTimer(float time)
//    {
//        canAttack = false;
//        yield return new WaitForSeconds(time);
//        canAttack = true;
//    }

//    public void Parried()
//    {
//        ac.SetTrigger("Parried");

//        if(parryCount >= enemyStats.health)
//        {
//            Debug.Log("Enemy Defeated!");
//            StopAllCoroutines();
//            canAttack = false;
//            ParryEnd();
//        }
//        else if(combo == true && comboStatus < enemyStats.comboLength)
//        {
//            canAttack = true;
//        }
//        else
//        {
//            StopAllCoroutines();
//            StartCoroutine(AtkTimer(5.0f / enemyStats.atkSpeed));
//        }
//    }

//}
