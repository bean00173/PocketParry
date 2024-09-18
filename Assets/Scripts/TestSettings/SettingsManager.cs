using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Transform buttonParent;

    public bool resetting;

    //public Image tl_img, tr_img, bl_img, br_img;

    public static SettingsManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateImages();
        
    }

    public void Apply()
    {
        GameManager.Instance.StoreInputSettings(m_tl_fill, m_tl_angle, m_tr_fill, m_tr_angle, m_bl_fill, m_bl_angle, m_br_fill, m_br_angle);
    }

    public void Reset()
    {
        resetting = true;

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

        resetting = false;
    }

    //private void UpdateImages()
    //{

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

    public float ReturnDefaultHandleGradient(HandleType type)
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
}
