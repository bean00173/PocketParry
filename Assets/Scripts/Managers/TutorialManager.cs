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
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.selectedLevelId != LevelID.tutorial)
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
        tutorialEnemy.PlayAttack(0);
    }

    public void DoInstaAtk()
    {
        tutorialEnemy.PlayAttack(2);
        teachingInsta = true;
    }

    public void SuccessfulParry()
    {
        if (teachingParry)
        {
            onSuccessfulParry.Invoke();
            teachingParry = false;
        }
    }

    public void SuccessfulInstaParry()
    {
        if(teachingInsta)
        {
            onInstaSuccess.Invoke();
            teachingInsta = false;
        }
    }

    public void DisplayInstaKill()
    {
        if (teachingInsta)
        {
            Time.timeScale = 0;

            onInstantKillAtk.Invoke();
        }

    }

    public void DisplayParryReady()
    {
        if (teachingParry)
        {
            Time.timeScale = 0;

            onParryReady.Invoke();
        }
    }

    public void ReadyForFreePlay()
    {
        tutorialEnemy.FreePlay();
    }

    public void ReadyForInput(ParryDirection dir)
    {
        attackDirection = dir;
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
                SuccessfulInstaParry();
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
        GameManager.Instance.levelBeaten = true;
    }
}
