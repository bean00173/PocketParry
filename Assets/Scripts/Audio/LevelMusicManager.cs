using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusicManager : MonoBehaviour
{
    SoundHandler soundHandler;

    // Start is called before the first frame update
    void Start()
    {
        soundHandler = this.GetComponent<SoundHandler>();
        soundHandler.source.volume = .5f;

        if(GameManager.Instance.selectedLevelId == LevelID.tutorial)
        {
            soundHandler.PlaySound("Tutorial");
        }
        else
        {
            soundHandler.PlayRandomSound("Level");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!soundHandler.source.isPlaying)
        {
            soundHandler.PlayRandomSound("Level");
        }
    }
}
