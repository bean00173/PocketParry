using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DirectionalInputRegion : MonoBehaviour
{
    public ParryDirection inputDirection;
    public float colorChangeDuration;
    private Color startColor;
    private Image image;


    // Start is called before the first frame update
    void Start()
    {
        image = this.GetComponent<Image>();
        startColor = image.color;
        PlayerInput.Instance.inputHandled.AddListener(InputCheck);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InputCheck(ParryDirection dir)
    {
        if(dir == inputDirection && FinisherManager.Instance.finisherActive)
        {
            StartCoroutine(DoColorSwap());
        }
    }

    private IEnumerator DoColorSwap()
    {
        this.image.color = new Color(0, 1, 0, 1);
        yield return new WaitForSeconds(colorChangeDuration);
        this.image.color = startColor;
    }
}
