using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEvent : MonoBehaviour
{
    public UnityEvent onCollide = new UnityEvent();
    private bool canDoSound;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<AudioSource>().volume = .05f;
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
            onCollide.Invoke();
            Destroy(this);
        }
    }
}
