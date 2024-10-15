    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class InputSetting
{
    public HandleType type;
    public float value;

    public InputSetting(HandleType a, float b)
    {
        type = a;
        value = b;
    }
}

public enum HandleType
{
    tr_fill, 
    tr_angle, 
    tl_fill, 
    tl_angle, 
    br_fill, 
    br_angle, 
    bl_fill, 
    bl_angle,
    none
}

public class SettingsManager : MonoBehaviour
{
    [Header("Default input check values")]
    public float m_tr_fill_default = .5f;
    public float m_tr_angle_default = 2f;
    public float m_tl_fill_default = -2f;
    public float m_tl_angle_default = -.5f;
    public float m_br_fill_default = -2f;
    public float m_br_angle_default = -.5f;
    public float m_bl_fill_default = .5f;
    public float m_bl_angle_default = 2f;

    float m_tr_fill = .5f;
    float m_tr_angle = 2f;
    float m_tl_fill = -2f;
    float m_tl_angle = -.5f;
    float m_br_fill = -2f;
    float m_br_angle = -.5f;
    float m_bl_fill = .5f;
    float m_bl_angle = 2f;

    float theta_tr_fill;
    float theta_tr_angle;
    float theta_tl_fill;
    float theta_tl_angle;
    float theta_br_fill;
    float theta_br_angle;
    float theta_bl_fill;
    float theta_bl_angle;

    public TextMeshProUGUI tlfTxt, tlaTxt, trfTxt, traTxt, blfTxt, blaTxt, brfTxt, braTxt;

    public Transform buttonParent;

    public bool resetting;

    public Image top, bottom, left, right;

    //public Image tl_img, tr_img, bl_img, br_img;

    public static SettingsManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Apply();
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateImages();
        UpdateText();
        UpdateImage();
    }

    public void Apply()
    {
        GameManager.Instance.StoreInputSettings(new InputSetting(HandleType.tl_fill, m_tl_fill), new InputSetting(HandleType.tl_angle, m_tl_angle), new InputSetting(HandleType.tr_fill, m_tr_fill), new InputSetting(HandleType.tr_angle, m_tr_angle), new InputSetting(HandleType.bl_fill, m_bl_fill), new InputSetting(HandleType.bl_angle, m_bl_angle), new InputSetting(HandleType.br_fill, m_br_fill), new InputSetting(HandleType.br_angle, m_br_angle));
    }

    public void ResetSettings()
    {
        InputSetting[] savedSettings = GameManager.Instance.ReturnSavedInputSettings();

        m_tl_fill = savedSettings[0].value;
        m_tl_angle = savedSettings[1].value;
        m_tr_fill = savedSettings[2].value;
        m_tr_angle = savedSettings[3].value;
        m_bl_fill = savedSettings[4].value;
        m_bl_angle = savedSettings[5].value;
        m_br_fill = savedSettings[6].value;
        m_br_angle = savedSettings[7].value;

        foreach (Transform child in buttonParent)
        {
            try
            {
                child.GetComponent<GradientHandle>().ResetHandle();
            }
            catch (System.Exception e)
            {
                Debug.LogWarning($"{e.GetType()} : No component of type GradientHandle");
            }
        }
    }

    public void Default()
    {
        ResetDefault(true, HandleType.tr_fill);
        foreach (Transform child in buttonParent)
        {
            try
            {
                child.GetComponent<GradientHandle>().ResetHandle();
            }
            catch (System.Exception e)
            {
                Debug.LogWarning($"{e.GetType()} : No component of type GradientHandle");
            }
        }
    }

    //private void UpdateImages()
    //{

    //}

    private void UpdateImage()
    {
        top.rectTransform.rotation = Quaternion.Euler(top.rectTransform.rotation.x, top.rectTransform.rotation.y, theta_tl_fill);
        top.fillAmount = ((theta_tr_angle + theta_tl_fill) / 90) / 4;

        bottom.rectTransform.rotation = Quaternion.Euler(top.rectTransform.rotation.x, top.rectTransform.rotation.y, theta_br_fill);
        bottom.fillAmount = ((theta_bl_angle + theta_br_fill) / 90) / 4;

        left.rectTransform.rotation = Quaternion.Euler(top.rectTransform.rotation.x, top.rectTransform.rotation.y, theta_bl_fill);
        left.fillAmount = ((theta_tl_angle + theta_bl_fill) / 90) / 4;

        right.rectTransform.rotation = Quaternion.Euler(top.rectTransform.rotation.x, top.rectTransform.rotation.y, theta_tr_fill);
        right.fillAmount = ((theta_br_angle + theta_tr_fill) / 90) / 4;
    }

        //if (CheckIfFillType())
        //{
        //    float maxFill = SettingsManager.Instance.ReturnSisterAngle(handleType);
        //    float minFill = angle;
        //    // check direction then depending on direction keep angle same or 90 - angle 
        //    if (this.handleType == HandleType.tl_fill || this.handleType == HandleType.br_fill)
        //    {
        //        float range = (90 - minFill) - (90 - maxFill);
        //        //float fill = (range / 90) / 4;
        //        //if(quadrantImg.fillAmount != fill)
        //        //{
        //        //    quadrantImg.fillAmount = fill;
        //        //}
        //        quadrantImg.fillAmount = (range / 90) / 4;
        //    }
        //    else
        //    {
        //        float range = minFill - maxFill;
        //        //float fill = (range / 90) / 4;
        //        //if (quadrantImg.fillAmount != fill)
        //        //{
        //        //    quadrantImg.fillAmount = fill;
        //        //}
        //        quadrantImg.fillAmount = (range / 90) / 4;
        //    }


        //}
        //else
        //{
        //    float maxFill = angle;
        //    float minFill = SettingsManager.Instance.ReturnSisterAngle(handleType);

        //    Vector3 rotation = quadrantImg.rectTransform.eulerAngles;

        //    if (this.handleType == HandleType.tl_angle || this.handleType == HandleType.br_angle)
        //    {
        //        float range = (90 - minFill) - (90 - maxFill);
        //        quadrantImg.rectTransform.rotation = Quaternion.Euler(rotation.x, rotation.y, angle);
        //        //float fill = (range / 90) / 4;
        //        //if (quadrantImg.fillAmount != fill)
        //        //{
        //        //    quadrantImg.fillAmount = fill;
        //        //}
        //        quadrantImg.fillAmount = (range / 90) / 4;
        //    }
        //    else
        //    {
        //        float range = minFill - maxFill;
        //        quadrantImg.rectTransform.rotation = Quaternion.Euler(rotation.x, rotation.y, 90 - angle);
        //        //float fill = (range / 90) / 4;
        //        //if (quadrantImg.fillAmount != fill)
        //        //{
        //        //    quadrantImg.fillAmount = fill;
        //        //}
        //        quadrantImg.fillAmount = (range / 90) / 4;
        //    }

        //}

    public void StoreGradient(HandleType type, float gradient)
    {
        if (type == HandleType.tl_fill) m_tl_fill = gradient;
        else if (type == HandleType.tl_angle) m_tl_angle = gradient;
        else if (type == HandleType.tr_fill) m_tr_fill = gradient;
        else if (type == HandleType.tr_angle) m_tr_angle = gradient;
        else if (type == HandleType.bl_angle) m_bl_angle = gradient;
        else if (type == HandleType.bl_fill) m_bl_fill = gradient;
        else if (type == HandleType.br_angle) m_br_angle = gradient;
        else m_br_fill = gradient;
    }

    public void StoreAngle(HandleType type, float angle)
    {
        if (type == HandleType.tl_fill) theta_tl_fill = angle;
        else if (type == HandleType.tl_angle) theta_tl_angle = angle;
        else if (type == HandleType.tr_fill) theta_tr_fill = angle;
        else if (type == HandleType.tr_angle) theta_tr_angle = angle;
        else if (type == HandleType.bl_angle) theta_bl_angle = angle;
        else if (type == HandleType.bl_fill) theta_bl_fill = angle;
        else if (type == HandleType.br_angle) theta_br_angle = angle;
        else theta_br_fill = angle;

    }

    public void ResetDefault(bool all, HandleType type)
    {
        
        if (!all)
        {
            if (type == HandleType.tl_fill) m_tl_fill = m_tl_fill_default;
            else if (type == HandleType.tl_angle) m_tl_angle = m_tl_angle_default;
            else if (type == HandleType.tr_fill) m_tr_fill = m_tr_fill_default;
            else if (type == HandleType.tr_angle) m_tr_angle = m_tr_angle_default;
            else if (type == HandleType.bl_angle) m_bl_angle = m_bl_angle_default;
            else if (type == HandleType.bl_fill) m_bl_fill = m_bl_fill_default;
            else if (type == HandleType.br_angle) m_br_angle = m_br_angle_default;
            else m_br_fill = m_br_fill_default;
        }
        else
        {
            m_tl_fill = m_tl_fill_default;
            m_tl_angle = m_tl_angle_default;
            m_tr_fill = m_tr_fill_default;
            m_tr_angle = m_tr_angle_default;
            m_bl_angle = m_bl_angle_default;
            m_bl_fill = m_bl_fill_default;
            m_br_angle = m_br_angle_default;
            m_br_fill = m_br_fill_default;
        }
    }

    public float ReturnSavedHandleGradient(HandleType type)
    {
        if (type == HandleType.tl_fill) return m_tl_fill;
        else if (type == HandleType.tl_angle) return m_tl_angle;
        else if (type == HandleType.tr_fill) return m_tr_fill;
        else if (type == HandleType.tr_angle) return m_tr_angle;
        else if (type == HandleType.bl_angle) return m_bl_angle;
        else if (type == HandleType.bl_fill) return m_bl_fill;
        else if (type == HandleType.br_angle) return m_br_angle;
        else return m_br_fill;
    }

    public float ReturnSisterHandleGradient(HandleType type)
    {
        if (type == HandleType.tl_fill) return m_tl_angle;
        else if (type == HandleType.tl_angle) return m_tl_fill ;
        else if (type == HandleType.tr_fill) return m_tr_angle;
        else if (type == HandleType.tr_angle) return m_tr_fill;
        else if (type == HandleType.bl_fill) return m_bl_angle;
        else if (type == HandleType.bl_angle) return m_bl_fill;
        else if (type == HandleType.br_fill) return m_br_angle;
        else return m_br_fill;
    }

    public float ReturnSisterAngle(HandleType type)
    {
        if (type == HandleType.tl_fill) return theta_tl_angle;
        else if (type == HandleType.tl_angle) return theta_tl_fill;
        else if (type == HandleType.tr_fill) return theta_tr_angle;
        else if (type == HandleType.tr_angle) return theta_tr_fill;
        else if (type == HandleType.bl_fill) return theta_bl_angle;
        else if (type == HandleType.bl_angle) return theta_bl_fill;
        else if (type == HandleType.br_fill) return theta_br_angle;
        else return theta_br_fill;
    }

    public void UpdateText()
    {
        tlfTxt.text = $" TLF : {m_tl_fill}";
        tlaTxt.text = $" TLA : {m_tl_angle}";
        trfTxt.text = $" TRF : {m_tr_fill}";
        traTxt.text = $" TRA : {m_tr_angle}";
        blfTxt.text = $" BLF : {m_bl_fill}";
        blaTxt.text = $" BLA : {m_bl_angle}";
        brfTxt.text = $" BRF : {m_br_fill}";
        braTxt.text = $" BRA : {m_br_angle}";
    }
}
