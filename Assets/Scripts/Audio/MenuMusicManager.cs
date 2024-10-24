using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicManager : MonoBehaviour
{
    public SoundHandler music, ambience;

    public float timePerSource = 20;

    // Start is called before the first frame update
    void Start()
    {
        music.source.volume = 1;
        ambience.source.volume = .25f;
        music.PlayRandomSound("Menu");
        ambience.PlayRandomSound("Wind");

        //StartCoroutine(Cooldown(timePerSource));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ToggleAmbience()
    {
        ambience.FadeIn(.5f);

        StartCoroutine(Cooldown(timePerSource));
    }

    private IEnumerator Cooldown(float time)
    {
        yield return new WaitForSeconds(time);

        ToggleAmbience();
    }
}
