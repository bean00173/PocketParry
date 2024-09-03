using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimingSlider : MonoBehaviour
{
    public EnemyBehaviour enemyBehaviour; // REPLACE WITH COMBAT MANAGER DOWN LINE - will manage spawning of enemies etc.
    Slider timingSlider;

    private float vulnStart;
    private float vulnEnd;
    private float inputTime;

    // Start is called before the first frame update
    void Start()
    {
        timingSlider = this.GetComponent<Slider>();

        enemyBehaviour.onHitTaken.AddListener(hitTaken);
        enemyBehaviour.onParrySuccessful.AddListener(parrySuccessful);
        enemyBehaviour.onVulnerable.AddListener(vulnerableStart);
        enemyBehaviour.onInVulnerable.AddListener(vulnerableEnd);
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

    private void vulnerableStart(float time)
    {
        vulnStart = time;
    }

    private void vulnerableEnd(float time)
    {
        vulnEnd = time;
        UpdateSlider();
    }

    private void parrySuccessful(float time)
    {
        inputTime = time;
    }

    private void hitTaken(float time)
    {
        vulnEnd = time;
        UpdateSlider(true);
    }
}
