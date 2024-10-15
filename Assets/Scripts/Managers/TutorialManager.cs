using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    private TutorialEnemyBehaviour tutorialEnemy;

    public bool teachingParry, teachingInsta;

    private int successfulParries;

    public UnityEvent onIntroductionEvent = new UnityEvent();
    public UnityEvent onInstantKillAtk = new UnityEvent();
    public UnityEvent onInstaFail = new UnityEvent();
    public UnityEvent onInstaSuccess = new UnityEvent();
    public UnityEvent onParryReady = new UnityEvent();
    public UnityEvent onSuccessfulParry = new UnityEvent();
    public UnityEvent onUnsuccessfulParry = new UnityEvent();
    public UnityEvent onFreePlay = new UnityEvent();
    public UnityEvent onTutorialFinished = new UnityEvent();

    ParryDirection attackDirection;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance.selectedLevelId != LevelID.tutorial)
        {
            Destroy(this.gameObject);
        }
        else
        {
            onIntroductionEvent.Invoke();
            teachingParry = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEnemy(TutorialEnemyBehaviour enemy)
    {
        tutorialEnemy = enemy;
    }

    public void BeginEnemyAttacks()
    {
        tutorialEnemy.PlayAttack(Random.Range(0, 1));
    }

    public void DoInstaAtk()
    {
        tutorialEnemy.PlayAttack(2);
        teachingInsta = true;
    }

    public void SuccessfulParry()
    {
        onSuccessfulParry.Invoke();
        teachingParry = false;
    }

    public void DisplayInstaKill()
    {
        Time.timeScale = 0;

        onInstantKillAtk.Invoke();
    }

    public void ReadyForInput(ParryDirection dir)
    {
        Time.timeScale = 0;
        attackDirection = dir;

        onParryReady.Invoke();
    }

    public void ReceivePlayerInput(ParryDirection input)
    {
        Time.timeScale = 1;

        if (teachingParry)
        {
            if (input == attackDirection)
            {
                tutorialEnemy.SuccessfullyParried();
                SuccessfulParry();
            }
            else
            {
                tutorialEnemy.IncorrectInput();
                onUnsuccessfulParry.Invoke();
            }
        }
        else if (teachingInsta)
        {
            if (input == attackDirection)
            {
                tutorialEnemy.SuccessfullyParried();
                onInstaSuccess.Invoke();
            }
            else
            {
                tutorialEnemy.IncorrectInput();
                onInstaFail.Invoke();
            }
        }
        
    }

    public void TutorialFinished()
    {
        onTutorialFinished.Invoke();
    }
}
