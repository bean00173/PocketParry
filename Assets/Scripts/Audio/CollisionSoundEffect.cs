using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEvent : MonoBehaviour
{
    private bool canDoSound;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<AudioSource>().volume = .05f;

        this.GetComponent<SoundHandler>().source = this.GetComponent<AudioSource>();

        Invoke(nameof(DoSound), .5f);
        Invoke(nameof(ForceDisable), 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DoSound()
    {
        canDoSound = true;
    }

    void ForceDisable()
    {
        Destroy(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (canDoSound)
        {
            this.GetComponent<SoundHandler>().PlayRandomSound("Blood_Impact");
            Destroy(this);
        }
    }
}
