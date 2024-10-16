using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessFeatureEnabler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!CombatManager.instance.levelInfo.endless)
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
