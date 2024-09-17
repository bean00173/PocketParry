using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GradientHandle : MonoBehaviour, IDragHandler
{
    public float gradient;
    public float angle;
    public HandleType handleType;

    public Image quadrantImg;

    // Start is called before the first frame update
    void Start()
    {
        angle = InitialAngleCalculation();
        //SettingsManager.Instance.StoreAngle(handleType, angle);
        this.gradient = SettingsManager.Instance.ReturnDefaultHandleGradient(handleType);
        this.transform.localPosition = UpdatePosBasedOnGradient();
        //ResetHandle();
    }

    // Update is called once per frame
    void Update()
    {
        SettingsManager.Instance.StoreGradient(handleType, gradient);
        SettingsManager.Instance.StoreAngle(handleType, angle);
    }

    public void OnDrag(PointerEventData data)
    {
        this.transform.localPosition = CalculateCirclePosition(RecenterPosition(data.position));
        //this.transform.position = CalculateCirclePosition(data.position);
    }

    //public void ResetHandle()
    //{
    //    this.transform.localPosition = UpdatePosBasedOnGradient();
    //    UpdateImage();
    //}

    private Vector2 UpdatePosBasedOnGradient()
    {
        float angle = Mathf.Atan(gradient);

        float posX = 250 * Mathf.Cos(angle);
        float posY = 250 * Mathf.Sin(angle);

        if(this.handleType == HandleType.tl_fill || this.handleType == HandleType.tl_angle || this.handleType == HandleType.bl_fill || this.handleType == HandleType.bl_angle ? true : false)
        {
            return new Vector2(-posX, -posY);
        }
        else
        {
            return new Vector2(posX, posY);
        }
        
    }

    private Vector2 RecenterPosition(Vector2 pos)
    {
        float recenteredY;
        float recenteredX;

        if(pos.y == Screen.height / 2)
        {
            recenteredY = 0;
        }
        else if(pos.y > Screen.height / 2)
        {
            recenteredY = pos.y - Screen.height / 2;
        }
        else
        {
            recenteredY = -1 * (Screen.height / 2 - pos.y);
        }

        if (pos.x == Screen.width / 2)
        {
            recenteredX = 0;
        }
        else if (pos.x > Screen.width / 2)
        {
            recenteredX = pos.x - Screen.width / 2;
        }
        else
        {
            recenteredX = -1 * (Screen.width / 2 - pos.x);
        }

        return new Vector2(recenteredX, recenteredY);
    }

    private float InitialAngleCalculation()
    {
        Vector2 center = new Vector2(0, 0);
        Vector2 pos = this.transform.localPosition;

        float dist = Vector2.Distance(pos, center);
        float deltaX = center.x - pos.x;

        return CalculateAngleToCenter(deltaX, dist);
    }

    private Vector2 CalculateCirclePosition(Vector2 pos)
    {
        Vector2 center = new Vector2(0, 0);
        float dist = Vector2.Distance(pos, center);

        float deltaY = center.y - pos.y;
        float deltaX = center.x - pos.x;

        if (!CheckValidAdjustment(deltaX, deltaY))
        {
            return this.transform.localPosition;
        }

        gradient = deltaY / deltaX;
        angle = CalculateAngleToCenter(deltaX, dist);

        float distRatio = Mathf.Abs(250 / dist);

        float intersectX = -1 * distRatio * deltaX;
        float intersectY = -1 * distRatio * deltaY;

        UpdateImage();

        return new Vector2(intersectX, intersectY);
    }

    private bool CheckValidAdjustment(float deltaX, float deltaY)
    {
        float projectedGradient = deltaY / deltaX;

        if (CheckSisterHandleGradient(projectedGradient))
        {
            if (handleType == HandleType.tl_fill || handleType == HandleType.br_fill)
            {
                return projectedGradient > -10 && projectedGradient < -1; ;
            }
            else if (handleType == HandleType.tl_angle || handleType == HandleType.br_angle)
            {
                return projectedGradient > -1 && projectedGradient < -.1; ;
            }
            else if (handleType == HandleType.tr_angle || handleType == HandleType.bl_angle)  
            {
                return projectedGradient > 1 && projectedGradient < 10;
            }
            else if (handleType == HandleType.tr_fill || handleType == HandleType.bl_fill) 
            {
                return projectedGradient > .1 && projectedGradient < 1;
            }
        }

        return false;
    }

    private float CalculateAngleToCenter(float deltaX, float dist)
    {
        return Mathf.Rad2Deg * Mathf.Abs(Mathf.Asin(deltaX / dist));
    }

    private bool CheckSisterHandleGradient(float projected)
    {
        float sisterGrad = SettingsManager.Instance.ReturnSisterHandleGradient(handleType);
        if (handleType == HandleType.tl_angle || handleType == HandleType.tr_angle || handleType == HandleType.br_angle || handleType == HandleType.bl_angle) 
        {
            return projected > sisterGrad + .1f;
        }
        else if (handleType == HandleType.tl_fill || handleType == HandleType.tr_fill || handleType == HandleType.br_fill || handleType == HandleType.bl_fill) 
        {
            return projected < sisterGrad - .1f;
        }

        return false;
    }

    private bool CheckIfFillType()
    {
        return this.handleType == HandleType.tr_fill || this.handleType == HandleType.tl_fill || this.handleType == HandleType.br_fill || this.handleType == HandleType.bl_fill ? true : false;
    }

    private void UpdateImage()
    {

        Debug.Log(angle);

        if (CheckIfFillType())
        {
            float maxFill = SettingsManager.Instance.ReturnSisterAngle(handleType);
            float minFill = angle;
            // check direction then depending on direction keep angle same or 90 - angle 
            if (this.handleType == HandleType.tl_fill || this.handleType == HandleType.br_fill)
            {
                float range = (90 - minFill) - (90 - maxFill);
                //float fill = (range / 90) / 4;
                //if(quadrantImg.fillAmount != fill)
                //{
                //    quadrantImg.fillAmount = fill;
                //}
                quadrantImg.fillAmount = (range / 90) / 4;
            }
            else
            {
                float range = minFill - maxFill;
                //float fill = (range / 90) / 4;
                //if (quadrantImg.fillAmount != fill)
                //{
                //    quadrantImg.fillAmount = fill;
                //}
                quadrantImg.fillAmount = (range / 90) / 4;
            }


        }
        else
        {
            float maxFill = angle;
            float minFill = SettingsManager.Instance.ReturnSisterAngle(handleType);

            Vector3 rotation = quadrantImg.rectTransform.eulerAngles;
            
            if (this.handleType == HandleType.tl_angle || this.handleType == HandleType.br_angle)
            {
                float range = (90 - minFill) - (90 - maxFill);
                quadrantImg.rectTransform.rotation = Quaternion.Euler(rotation.x, rotation.y, angle);
                //float fill = (range / 90) / 4;
                //if (quadrantImg.fillAmount != fill)
                //{
                //    quadrantImg.fillAmount = fill;
                //}
                quadrantImg.fillAmount = (range / 90) / 4;
            }
            else
            {
                float range = minFill - maxFill;
                quadrantImg.rectTransform.rotation = Quaternion.Euler(rotation.x, rotation.y, 90 - angle);
                //float fill = (range / 90) / 4;
                //if (quadrantImg.fillAmount != fill)
                //{
                //    quadrantImg.fillAmount = fill;
                //}
                quadrantImg.fillAmount = (range / 90) / 4;
            }
            
        }
    }
}
