using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    EnemyBehaviour currentEnemy;

    public int playerMaxHealth;
    private int playerCurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
        //ac = this.GetComponent<Animator>();
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
        if (insta)
        {
            playerCurrentHealth = 0;
            Debug.Log("DEAD");
        }
        else
        {
            playerCurrentHealth--;

            if (playerCurrentHealth == 0)
            {
                Debug.Log("Dead");
            }
        }
    }
}
