using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    EnemyBehaviour currentEnemy;
    Animator ac;

    float x, y;

    // Start is called before the first frame update
    void Start()
    {
        ac = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ac.SetFloat("x", x);
        ac.SetFloat("y", y);
    }

    public void UpdateCurrentEnemy(EnemyBehaviour enemy)
    {
        currentEnemy = enemy;
        enemy.onHitTaken.AddListener(TakeHit);
    }

    private void TakeHit(float time, ParryDirection dir)
    {
        switch (dir)
        {
            case ParryDirection.Up: x = 0; y = -1; break; // 
            case ParryDirection.Down: x = 0; y = 1; break; //
            case ParryDirection.Left: x = 1; y = 0; break; // 
            case ParryDirection.Right: x = -1; y = 0; break; // 
            case ParryDirection.LeftUp: x = 1; y = -1; break; // 
            case ParryDirection.LeftDown: x = 1; y = 1; break; //
            case ParryDirection.RightUp: x = -1; y = -1; break; //
            case ParryDirection.RightDown: x = -1; y = 1; break; //
        }

        ac.SetTrigger("Hit");
    }

}
