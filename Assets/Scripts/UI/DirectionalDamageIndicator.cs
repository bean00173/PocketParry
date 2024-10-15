using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionalDamageIndicator : MonoBehaviour
{
    Image img;
    public float dmgFadeDelay = 2f;
    public float defaultFadeDuration = 10f;
    // Start is called before the first frame update
    void Start()
    {
        img = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoHit()
    {
        StopAllCoroutines();
        StartCoroutine(DoImageFadeIn(.25f));
    }

    private void DoFadeOut()
    {
        StartCoroutine(DoImageFadeOut(defaultFadeDuration));
    }

    private IEnumerator DoImageFadeIn(float duration)
    {
        float time = 0;
        while(time < duration)
        {
            img.color = new Color(1, 1, 1, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        img.color = new Color(1, 1, 1, 1);

        Invoke(nameof(DoFadeOut), dmgFadeDelay);
    }

    private IEnumerator DoImageFadeOut(float duration)
    {
        float time = 0;
        while (time < duration)
        {
            img.color = new Color(1, 1, 1, 1 - (time / duration));
            time += Time.deltaTime;
            yield return null;
        }

        img.color = new Color(1, 1, 1, 0);
    }
}
