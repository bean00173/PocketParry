using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Button playBtn;

    private void Awake()
    {
        Instance = this; 
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.UpdateReferences(playBtn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
