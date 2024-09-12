using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ParticleSystemAutoDestroy : MonoBehaviour
{

    private ParticleSystem _ps;
    private AudioSource source;


    public void Start()
    {
        _ps = GetComponent<ParticleSystem>();
        try
        {
            source = GetComponent<AudioSource>();
        }
        catch (Exception e)
        {
            Debug.LogWarning($"{e.GetType()} This effect has no audio source");
        }
    }

    public void FixedUpdate()
    {
        if (_ps && !_ps.IsAlive() && !source.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}