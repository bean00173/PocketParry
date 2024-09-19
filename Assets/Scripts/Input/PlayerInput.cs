using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum ParryDirection
{
    Up,
    LeftUp,
    RightUp,
    Down,
    LeftDown,
    RightDown,
    Right,
    Left
}

public class PlayerInput : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{

    [HideInInspector] public UnityEvent<ParryDirection> inputHandled;
    //[HideInInspector] public UnityEvent inputStarted;
    //[HideInInspector] public UnityEvent<ParryDirection> directionPredict;
    //ParryDirection input;

    public static PlayerInput Instance;
    public float parryTime;

    //float dragStart, dragEnd;
    public float InputTime { get; private set; }

    public bool DoingInput { get; private set; }
    public ParryDirection InputDirection { get; private set; }

    private InputSetting[] inputSettings = new InputSetting[8];

    float m_tr_fill;
    float m_tr_angle;
    float m_tl_fill;
    float m_tl_angle;
    float m_br_fill;
    float m_br_angle;
    float m_bl_fill;
    float m_bl_angle;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadSettings(GameManager.Instance.ReturnSavedInputSettings());
    }

    public void OnDrag(PointerEventData eventData)
    {
        //dragStart = Time.time;
        //inputStarted.Invoke();

        //Debug.Log(dragStart);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("INPUT STARTED");
        //dragStart = Time.time;
        //inputStarted.Invoke();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //dragEnd = Time.time;

        //if (gradient > 2 || gradient < -2) input = CheckTop(eventData) ? ParryDirection.Up : ParryDirection.Down;
        //else if (gradient < 2 && gradient > .5) input = CheckRightSide(eventData) ? ParryDirection.RightUp : ParryDirection.LeftDown;
        //else if (gradient < .5 && gradient > -.5) input = CheckRightSide(eventData) ? ParryDirection.Right : ParryDirection.Left;
        //else if (gradient < -.5 && gradient > -2) input = CheckRightSide(eventData) ? ParryDirection.RightDown : ParryDirection.LeftUp;

        //float timeSinceDragMiddle = ((dragEnd - dragStart) / 2);

        //Debug.Log($"Player Swiped : {input} | Drag Start : {dragStart}, Drag End : {dragEnd}, Average Input Time : {dragEnd - timeSinceDragMiddle}");

        InputDirection = CalculateDirection(eventData);
        Debug.Log(InputDirection);
        DoingInput = true;
        InputTime = Time.time;
        inputHandled.Invoke(InputDirection);
        Invoke(nameof(InputEnd), parryTime);
    }

    private ParryDirection CalculateDirection(PointerEventData data)
    {
        float gradient = (data.position.y - data.pressPosition.y) / (data.position.x - data.pressPosition.x);

        Debug.Log(gradient);

        //if (gradient > 2 || gradient < -2) return CheckTop(data) ? ParryDirection.Up : ParryDirection.Down;
        //else if (gradient < 2 && gradient > .5) return CheckRightSide(data) ? ParryDirection.RightUp : ParryDirection.LeftDown;
        //else if (gradient < .5 && gradient > -.5) return CheckRightSide(data) ? ParryDirection.Right : ParryDirection.Left;
        //else if (gradient < -.5 && gradient > -2) return CheckRightSide(data) ? ParryDirection.RightDown : ParryDirection.LeftUp;
        //else return ParryDirection.Up;


        if (CheckRightSide(data))
        {
            if (gradient < m_tr_angle && gradient > m_tr_fill)
            {
                return ParryDirection.RightUp;
            }
            else if (gradient < m_tr_fill && gradient > m_br_angle)
            {
                return ParryDirection.Right;
            }
            else if (gradient < m_br_angle && gradient > m_br_fill)
            {
                return ParryDirection.RightDown;
            }
            else if (gradient > m_tr_angle)
            {
                return ParryDirection.Up;
            }
            else
            {
                return ParryDirection.Down;
            }
        }
        else
        {

            if (gradient < m_bl_angle && gradient > m_bl_fill)
            {
                return ParryDirection.LeftDown;
            }
            else if (gradient < m_bl_fill && gradient > m_tl_angle)
            {
                return ParryDirection.Left;
            }
            else if (gradient < m_tl_angle && gradient > m_tl_fill)
            {
                return ParryDirection.LeftUp;
            }
            else if (gradient > m_bl_angle)
            {
                return ParryDirection.Down;
            }
            else
            {
                return ParryDirection.Up;
            }
        }
    }

    private void InputEnd()
    {
        DoingInput = false;
    }

    private bool CheckRightSide(PointerEventData data)
    {
        return data.position.x > data.pressPosition.x;
    }

    private bool CheckTop(PointerEventData data)
    {
        return data.position.y > data.pressPosition.y;
    }

    public void LoadSettings(InputSetting[] settings)
    {
        inputSettings = settings;

        m_tl_fill = inputSettings[0].value;
        m_tl_angle = inputSettings[1].value;
        m_tr_fill = inputSettings[2].value;
        m_tr_angle = inputSettings[3].value;
        m_bl_fill = inputSettings[4].value;
        m_bl_angle = inputSettings[5].value;
        m_br_fill = inputSettings[6].value;
        m_br_angle = inputSettings[7].value;

    }
}
