using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum enemyStatus
{
    next,
    defeated,
    current
}

public class ProgressIcon : MonoBehaviour
{
    //public bool defeated;
    //public bool current;
    //public bool next;

    public enemyStatus status;
    public Image overlay;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(status != enemyStatus.next)
        {
            overlay.gameObject.SetActive(true);
            LoadOverlay();
        }
        else
        {
            overlay.gameObject.SetActive(false);
        }
    }

    private void LoadOverlay()
    {
        overlay.sprite = Resources.Load<Sprite>($"UI/{status}");
    }
}
