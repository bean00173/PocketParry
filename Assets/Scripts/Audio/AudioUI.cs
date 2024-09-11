using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SoundHandler))]
public class AudioUI : MonoBehaviour
{
    AudioSource _source;
    SoundHandler _handler;
    // Start is called before the first frame update
    void Start()
    {
        _source = this.GetComponent<AudioSource>();
        _handler = this.GetComponent<SoundHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        _handler.PlaySound("Click_01");
    }
    public void ClickPlay()
    {
        _handler.PlaySound("Click");
    }
    public void Enter()
    {
        _handler.PlaySound("Hover");
    }
}
