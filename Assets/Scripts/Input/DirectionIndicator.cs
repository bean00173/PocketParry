using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionIndicator : MonoBehaviour
{
    public PlayerInput input;
    public ParryDirection direction;
    // Start is called before the first frame update
    void Start()
    {
        //input.inputHandled.AddListener(StartColourChange);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInput.Instance.DoingInput)
        {
            if(this.direction == PlayerInput.Instance.InputDirection)
            {
                this.GetComponent<Image>().color = Color.green;
            }
        }
        else
        {
            this.GetComponent<Image>().color = Color.red;
        }
    }
}
