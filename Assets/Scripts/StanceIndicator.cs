using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StanceIndicator : MonoBehaviour
{

    private float max;
    private Transform enemyHeadBone;
    public Vector3 offset;
    public float followSpeed = 2f;
    public float followMargin = 0.5f;

    float time;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = enemyHeadBone.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position = enemyHeadBone.position + offset;

        if(time < 1.0f)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, enemyHeadBone.position + offset, time / 1.0f);
            time += Time.deltaTime * followSpeed;
        }
        else
        {
            this.transform.position = enemyHeadBone.position + offset;
        }
        //if(Vector3.Distance(this.transform.position, enemyHeadBone.position + offset) > followMargin)
        //{
        //    float time = 0;
        //    float t = time / followSpeed;

        //    time += Time.deltaTime;
        //}
    }

    public void SetupBar(float max, Transform headBone)
    {
        this.max = max;
        this.transform.SetParent(CombatManager.instance.worldSpaceCanvas);
        enemyHeadBone = headBone;
    }

    public void UpdateStanceBar(float score)
    {
        Debug.Log($"UpdateBar! Ratio : {score / max}");

        float xScale = score == 0 ? 0 : (score / max);
        xScale = xScale * .025f;
        this.transform.localScale = new Vector3(xScale, .0025f, .01f);

        this.GetComponent<Image>().color = score / max > .66 ? new Color(255, 0, 0) : score / max > .33 ? new Color(255, 132, 0) : score / max > 0 ? new Color(255, 242, 0) : Color.white;
    }
}
