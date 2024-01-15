using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StanceIndicator : MonoBehaviour
{

    private float max;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupBar(float max)
    {
        this.max = max;
    }

    public void UpdateStanceBar(float score)
    {
        Debug.Log($"UpdateBar! Ratio : {score / max}");

        float xScale = score == 0 ? 0 : 5 * (score / max);
        this.transform.localScale = new Vector3(xScale, .25f, 1);

        this.GetComponent<Image>().color = score / max > .66 ? new Color(255, 0, 0) : score / max > .33 ? new Color(255, 132, 0) : score / max > 0 ? new Color(255, 242, 0) : Color.white;
    }
}
