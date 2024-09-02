using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinisherArrow : MonoBehaviour
{
    public ArrowDirection direction;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = this.GetComponent<Image>();
        image.sprite = Resources.Load<Sprite>($"Sprites/{direction}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
