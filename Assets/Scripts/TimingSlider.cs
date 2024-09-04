using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimingSlider : MonoBehaviour
{
    EnemyBehaviour enemyBehaviour; // REPLACE WITH COMBAT MANAGER DOWN LINE - will manage spawning of enemies etc.
    Slider timingSlider;

    private float vulnStart;
    private float vulnEnd;
    private float inputTime;

    // Start is called before the first frame update
    void Start()
    {
        timingSlider = this.GetComponent<Slider>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateSlider(bool reset = false)
    {
        if (reset)
        {
            timingSlider.minValue = 0;
            timingSlider.value = 0;
            timingSlider.maxValue = 1;
        }
        else
        {
            timingSlider.minValue = vulnStart;
            timingSlider.value = inputTime;
            timingSlider.maxValue = vulnEnd;
        }
    }

    private void VulnerableStart(float time)
    {
        vulnStart = time;
    }

    private void VulnerableEnd(float time)
    {
        vulnEnd = time;
        UpdateSlider();
    }

    private void ParrySuccessful(float time)
    {
        inputTime = time;
    }

    private void HitTaken(float time)
    {
        vulnEnd = time;
        UpdateSlider(true);
    }

    public void SetCurrentEnemy(EnemyBehaviour enemy)
    {
        enemyBehaviour = enemy;

        enemyBehaviour.onHitTaken.AddListener(HitTaken);
        enemyBehaviour.onParrySuccessful.AddListener(ParrySuccessful);
        enemyBehaviour.onVulnerable.AddListener(VulnerableStart);
        enemyBehaviour.onInVulnerable.AddListener(VulnerableEnd);
    }
}
