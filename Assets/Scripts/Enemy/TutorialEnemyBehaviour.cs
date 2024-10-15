using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemyBehaviour : EnemyBehaviour
{
    bool freeplay;
    // Start is called before the first frame update
    public override void Start()
    {
        this.enemyStats.name = SetName();
        ac = this.GetComponent<Animator>();
        stanceIndicator.SetupBar(this.gameObject.name, this.enemyStats.health, this.enemyStats.name, headBone);
        soundHandler = this.GetComponent<SoundHandler>();
    }

    public override void Update()
    {
        if (freeplay)
        {
            base.Update();
        }
    }

    public void SignalParryTiming()
    {
        foreach (AttackInfo info in attackInformation.attackInfo) // for each potential attack, cross reference to check with current attack to retrieve attack data
        {
            AnimatorClipInfo[] clipInfo = ac.GetCurrentAnimatorClipInfo(0);
            if (clipInfo[0].clip.name == info.clip.name)
            {
                currentClipInfo = info;
            }

        }

        TutorialManager.instance.ReadyForInput(currentClipInfo.parryDirection);
        TutorialManager.instance.DisplayParryReady();
    }

    public void SignalInstaParryTiming()
    {
        foreach (AttackInfo info in attackInformation.attackInfo) // for each potential attack, cross reference to check with current attack to retrieve attack data
        {
            AnimatorClipInfo[] clipInfo = ac.GetCurrentAnimatorClipInfo(0);
            if (clipInfo[0].clip.name == info.clip.name)
            {
                currentClipInfo = info;
            }

        }

        TutorialManager.instance.ReadyForInput(currentClipInfo.parryDirection);
        TutorialManager.instance.DisplayInstaKill();
    }

    public void SuccessfullyParried()
    {
        int y = CalculateScore(Time.time - PlayerInput.Instance.InputTime);

        if (!TutorialManager.instance.teachingParry && !TutorialManager.instance.teachingInsta)
        {
            CombatManager.instance.UpdateScore(y);
            composurePercentage = 1f - ((float)CombatManager.instance.score / (float)this.enemyStats.health);
            ac.SetFloat("Composure", composurePercentage);
        }

        onParrySuccessful.Invoke();

        if (lastFx != null)
        {
            lastFx.GetComponent<SoundHandler>().FadeOut();
        }

        GameObject fxPrefab = y == 1 ? parryVfx : blockVfx;
        Transform fx = Instantiate(fxPrefab, slashVfx.transform.parent).transform;
        lastFx = fx.gameObject;
        fx.SetParent(null);
    }

    public void IncorrectInput()
    {
        onHitTaken.Invoke(currentClipInfo.parryDirection, currentClipInfo.isInstaKill);
        soundHandler.PlayRandomSound("hit");
    }

    public void FreePlay()
    {
        StartCoroutine(CooldownTimer(2.0f));
        freeplay = true;
    }
}
