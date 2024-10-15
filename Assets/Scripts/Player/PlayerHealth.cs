using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    EnemyBehaviour currentEnemy;
    private int baseHealth = 3;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = baseHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEnemy(EnemyBehaviour enemy)
    {
        currentEnemy = enemy;
        enemy.onHitTaken.AddListener(TakeHit);
    }

    private void TakeHit(ParryDirection dir, bool insta)
    {
        currentHealth = insta ? 0 : currentHealth - 1;
        if(currentHealth == 0)
        {
            Debug.Log("DEAD");
        }
        else
        {
            Debug.Log(currentHealth);
        }
    }
}
