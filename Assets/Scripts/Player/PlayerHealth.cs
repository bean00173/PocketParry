using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [HideInInspector] public UnityEvent onPlayerDefeat;

    public GameObject heartsParent;
    public DamageVisualiser damageVisualiser;

    EnemyBehaviour currentEnemy;
    private int baseHealth = 3;
    private int currentHealth;

    public int CurrentHealth
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = value;
            damageVisualiser.UpdateVignette(currentHealth);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = baseHealth;
        if(CombatManager.instance.levelInfo.levelId == LevelID.tutorial)
        {
            heartsParent.SetActive(false);
        }
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
        Image heart = heartsParent.transform.GetChild(baseHealth - (baseHealth - CurrentHealth) - 1).GetComponent<Image>();
        heart.color = new Color(.2f, .2f, .2f, .2f);

        damageVisualiser.TakeHit(dir);

        CurrentHealth = insta ? 0 : CurrentHealth - 1;

        if (CurrentHealth == 0 && CombatManager.instance.levelInfo.levelId != LevelID.tutorial)
        {
            onPlayerDefeat.Invoke();
        }
        else if(CombatManager.instance.levelInfo.levelId == LevelID.tutorial)
        {
            Invoke(nameof(TutorialReturnHealth), 1f);
        }
        else
        {
            Debug.Log(CurrentHealth);
        }
    }

    private void TutorialReturnHealth()
    {
        CurrentHealth += 1;
    }

    public void EnemyBeaten()
    {
        if(CurrentHealth < 3)
        {
            CurrentHealth += 1;

            Image heart = heartsParent.transform.GetChild(baseHealth - (baseHealth - CurrentHealth) - 1).GetComponent<Image>();
            heart.color = new Color(1, 1, 1, 1);
        }
    }
}
