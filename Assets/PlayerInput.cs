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

    Vector2 startPos;
    float[] segmentBounds = { 22.5f, 67.5f, 112.5f, 157.5f, 202.5f, 247.5f, 292.5f, 292.5f, 337.5f };
    DraggedDirection input;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;
        //DraggedDirection input = GetDragDirection(dragVectorDirection);

        //if (CheckValidParry(input))
        //{
        //    Debug.Log("Parried!");

        //    UpdateScore();
        //    //detector.material.color = Color.green;

        //    currentEnemy.Parried();
        //}
        //else
        //{
        //    //score--;
        //}



        //float radius = Mathf.Sqrt(Mathf.Pow(eventData.position.x - startPos.x, 2) + Mathf.Pow(eventData.position.y - startPos.y, 2));
        //float radius = Mathf.Sqrt(Mathf.Pow(eventData.position.x - eventData.pressPosition.x, 2) + Mathf.Pow(eventData.pressPosition.y - startPos.y, 2));

        //Debug.Log($"{eventData.position}, {eventData.pressPosition}, {radius}");
        //float angle = (180 * (Mathf.Acos(eventData.position.x / radius)) / Mathf.PI);

        ////float angle = Vector2.Angle(eventData.position, new Vector2(0, radius));

        //Debug.Log(angle);

        //if (angle > segmentBounds[7] && angle < segmentBounds[0]) input = DraggedDirection.Up;
        //else if (angle > segmentBounds[0] && angle < segmentBounds[1]) input = DraggedDirection.RightUp;
        //else if (angle > segmentBounds[1] && angle < segmentBounds[2]) input = DraggedDirection.Right;
        //else if (angle > segmentBounds[2] && angle < segmentBounds[3]) input = DraggedDirection.RightDown;
        //else if (angle > segmentBounds[3] && angle < segmentBounds[4]) input = DraggedDirection.Down;
        //else if (angle > segmentBounds[4] && angle < segmentBounds[5]) input = DraggedDirection.LeftDown;
        //else if (angle > segmentBounds[5] && angle < segmentBounds[6]) input = DraggedDirection.Left;
        //else if (angle > segmentBounds[6] && angle < segmentBounds[7]) input = DraggedDirection.LeftUp;



        float gradient = (eventData.position.y - eventData.pressPosition.y) / (eventData.position.x - eventData.pressPosition.x);

        if (gradient > 2 || gradient < -2) input = CheckTop(eventData) ? DraggedDirection.Up : DraggedDirection.Down;
        else if (gradient < 2 && gradient > .5) input = CheckRightSide(eventData) ? DraggedDirection.RightUp : DraggedDirection.LeftDown;
        else if (gradient < .5 && gradient > -.5) input = CheckRightSide(eventData) ? DraggedDirection.Right : DraggedDirection.Left;
        else if (gradient < -2 && gradient > -.5) input = CheckRightSide(eventData) ? DraggedDirection.RightDown : DraggedDirection.LeftUp;

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

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    //private DraggedDirection GetDragDirection(Vector3 dragVector)
    //{
    //    float positiveX = Mathf.Abs(dragVector.x);
    //    float positiveY = Mathf.Abs(dragVector.y);
    //    Vector2 quadrant = CheckQuadrant(dragVector.x, dragVector.y);

    //    Debug.Log($"{dragVector.x},{dragVector.y} : {quadrant}");

    //    if (Mathf.Abs(dragVector.x) < Mathf.Cos((3 * Mathf.PI) / 8) && Mathf.Abs(dragVector.x) > Mathf.Cos(Mathf.PI / 8) && Mathf.Abs(dragVector.y) < Mathf.Sin((3 * Mathf.PI) / 8) && Mathf.Abs(dragVector.y) > Mathf.Sin(Mathf.PI / 8))
    //    {

    //    }


    //    DraggedDirection draggedDir;
    //    if (positiveX > positiveY)
    //    {
    //        draggedDir = (dragVector.x > 0) ? DraggedDirection.Right : DraggedDirection.Left;
    //    }
    //    else
    //    {
    //        draggedDir = (dragVector.y > 0) ? DraggedDirection.Up : DraggedDirection.Down;
    //    }


    //    Debug.Log(draggedDir);
    //    return draggedDir;
    //    return DraggedDirection.Up;
    //}

    private Vector2 CheckQuadrant(float x, float y)
    {
        return x > 0 ? y > 0 ? new Vector2(1, 1) : new Vector2(1, -1) : y > 0 ? new Vector2(-1, 1) : new Vector2(-1, -1);
    }
}
