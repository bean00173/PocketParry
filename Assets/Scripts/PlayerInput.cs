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

public class PlayerInput : MonoBehaviour, IDragHandler, IEndDragHandler
{

    [HideInInspector] public UnityEvent<ParryDirection> inputHandled = new UnityEvent<ParryDirection>();
    ParryDirection input;

    public static PlayerInput Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float gradient = (eventData.position.y - eventData.pressPosition.y) / (eventData.position.x - eventData.pressPosition.x);

        if (gradient > 2 || gradient < -2) input = CheckTop(eventData) ? ParryDirection.Up : ParryDirection.Down;
        else if (gradient < 2 && gradient > .5) input = CheckRightSide(eventData) ? ParryDirection.RightUp : ParryDirection.LeftDown;
        else if (gradient < .5 && gradient > -.5) input = CheckRightSide(eventData) ? ParryDirection.Right : ParryDirection.Left;
        else if (gradient < -.5 && gradient > -2) input = CheckRightSide(eventData) ? ParryDirection.RightDown : ParryDirection.LeftUp;

        Debug.Log(gradient);

        Debug.Log(input);
        inputHandled.Invoke(input);
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
