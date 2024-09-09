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

    [HideInInspector] public UnityEvent<ParryDirection> inputHandled = new UnityEvent<ParryDirection>();
    //[HideInInspector] public UnityEvent inputStarted;
    //[HideInInspector] public UnityEvent<ParryDirection> directionPredict;
    //ParryDirection input;

    public static PlayerInput Instance;
    public float parryTime = .5f;

    //float dragStart, dragEnd;

    public bool DoingInput { get; private set; }
    public ParryDirection InputDirection { get; private set; }


    private void Awake()
    {
        Instance = this;
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
        DoingInput = true;
        inputHandled.Invoke(InputDirection);
        Invoke(nameof(InputEnd), parryTime);
    }

    private ParryDirection CalculateDirection(PointerEventData data)
    {
        float gradient = (data.position.y - data.pressPosition.y) / (data.position.x - data.pressPosition.x);

        if (gradient > 2 || gradient < -2) return CheckTop(data) ? ParryDirection.Up : ParryDirection.Down;
        else if (gradient < 2 && gradient > .5) return CheckRightSide(data) ? ParryDirection.RightUp : ParryDirection.LeftDown;
        else if (gradient < .5 && gradient > -.5) return CheckRightSide(data) ? ParryDirection.Right : ParryDirection.Left;
        else if (gradient < -.5 && gradient > -2) return CheckRightSide(data) ? ParryDirection.RightDown : ParryDirection.LeftUp;
        else return ParryDirection.Up;

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

}
