using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DynamicLayoutGroupBackground : MonoBehaviour
{
    public GameObject contentGroup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSize()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentGroup.GetComponent<RectTransform>());

        float minDist = contentGroup.transform.GetChild(0).transform.position.x;
        float maxDist = contentGroup.transform.GetChild(contentGroup.transform.childCount - 1).transform.position.x;

        float dist = maxDist - minDist;

        this.GetComponent<RectTransform>().sizeDelta = new Vector2(dist, this.GetComponent<RectTransform>().sizeDelta.y);
    }
}
