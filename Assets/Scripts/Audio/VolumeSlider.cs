using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public AudioType type;
    private Slider slider;

    float vol;

    private void Awake()
    {
        slider = this.GetComponent<Slider>();

        switch (type)
        {
            case AudioType.player: AudioManager.instance.masterMixer.GetFloat("playerVol", out vol); break;
            case AudioType.enemy: AudioManager.instance.masterMixer.GetFloat("enemyVol", out vol); break;
            case AudioType.music: AudioManager.instance.masterMixer.GetFloat("musicVol", out vol); break;
            case AudioType.ambient: AudioManager.instance.masterMixer.GetFloat("ambientVol", out vol); break;
            case AudioType.effects: AudioManager.instance.masterMixer.GetFloat("fxVol", out vol); break;
            case AudioType.master: AudioManager.instance.masterMixer.GetFloat("masterVol", out vol); break;
        }

        slider.value = vol;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
