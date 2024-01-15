using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy
{
    [Range(0f, 10f)]
    public int health;
    public string name;
    public float comboChance;
    public float comboLength;
    public float damage;
    public float atkSpeed;
}

public class EnemyBehaviour : MonoBehaviour
{
    [HideInInspector] public CombatManager cm;
    public Enemy enemyStats;

    public float parryCount;
    bool canAttack;

    Animator ac;

    // Start is called before the first frame update
    void Start()
    {
        cm = GetComponentInParent<CombatManager>();
        ac = this.GetComponent<Animator>();

        StartCoroutine(AtkTimer(5.0f / enemyStats.atkSpeed));
    }

    // Update is called once per frame
    void Update()
    {
        if (canAttack)
        {
            ac.SetTrigger("DoAttack");
            StartCoroutine(AtkTimer(5.0f / enemyStats.atkSpeed));
        }
    }

    public void ParryStart()
    {
        cm.ParryStart(CombatManager.AtkType.left);
    }

    public void ParryEnd()
    {
        cm.ParryEnd();
    }

    private IEnumerator AtkTimer(float time)
    {
        canAttack = false;
        yield return new WaitForSeconds(time);
        canAttack = true;
    }

    public void Parried()
    {
        ac.SetTrigger("Parried");
        StopAllCoroutines();
        StartCoroutine(AtkTimer(5.0f / enemyStats.atkSpeed));
    }

}
