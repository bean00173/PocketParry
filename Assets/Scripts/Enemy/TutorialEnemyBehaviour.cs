using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemyBehaviour : EnemyBehaviour
{
    // Start is called before the first frame update
     public override void Start()
    {
        this.enemyStats.name = SetName();
        ac = this.GetComponent<Animator>();
        stanceIndicator.SetupBar(this.gameObject.name, this.enemyStats.health, this.enemyStats.name, headBone);
        soundHandler = this.GetComponent<SoundHandler>();
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    public void SignalParryTiming()
    {
        TutorialManager.instance.ReadyForInput(currentClipInfo.parryDirection);
    }

    public void SuccessfullyParried()
    {
        int y = CalculateScore(Time.time - PlayerInput.Instance.InputTime);

        if (!TutorialManager.instance.teachingParry) CombatManager.instance.UpdateScore(y);

        composurePercentage = 1f - ((float)CombatManager.instance.score / (float)this.enemyStats.health);
        ac.SetFloat("Composure", composurePercentage);

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
}
