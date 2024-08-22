using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform half in transform)
        {
            foreach(Transform row in half)
            {
                foreach(Transform rice in row)
                {
                    rice.localEulerAngles = new Vector3(0, Random.Range(0, 360), 0);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
