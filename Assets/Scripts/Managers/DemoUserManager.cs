using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DemoUserManager : MonoBehaviour
{
    public static DemoUserManager instance;
    public Button enterButton;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckInfo(string value)
    {
        if(value != "")
        {
            enterButton.interactable = true;
            GameManager.Instance.StorePlayerName(value);
        }
        else
        {
            enterButton.interactable = false;
        }
    }
}
