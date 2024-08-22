using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum DraggedDirection
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

    [HideInInspector] public UnityEvent<DraggedDirection> inputHandled = new UnityEvent<DraggedDirection>();
    DraggedDirection input;

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float gradient = (eventData.position.y - eventData.pressPosition.y) / (eventData.position.x - eventData.pressPosition.x);

        if (gradient > 2 || gradient < -2) input = CheckTop(eventData) ? DraggedDirection.Up : DraggedDirection.Down;
        else if (gradient < 2 && gradient > .5) input = CheckRightSide(eventData) ? DraggedDirection.RightUp : DraggedDirection.LeftDown;
        else if (gradient < .5 && gradient > -.5) input = CheckRightSide(eventData) ? DraggedDirection.Right : DraggedDirection.Left;
        else if (gradient < -.5 && gradient > -2) input = CheckRightSide(eventData) ? DraggedDirection.RightDown : DraggedDirection.LeftUp;

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
