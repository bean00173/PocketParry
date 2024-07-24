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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("TAP");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;
        DraggedDirection input = GetDragDirection(dragVectorDirection);

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

        inputHandled.Invoke(input);
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    private DraggedDirection GetDragDirection(Vector3 dragVector)
    {
        //float positiveX = Mathf.Abs(dragVector.x);
        //float positiveY = Mathf.Abs(dragVector.y);
        Vector2 quadrant = CheckQuadrant(dragVector.x, dragVector.y);

        Debug.Log($"{dragVector.x},{dragVector.y} : {quadrant}");

        //if (Mathf.Abs(dragVector.x) < Mathf.Cos((3 * Mathf.PI) / 8) && Mathf.Abs(dragVector.x) > Mathf.Cos(Mathf.PI / 8) && Mathf.Abs(dragVector.y) < Mathf.Sin((3 * Mathf.PI) / 8) && Mathf.Abs(dragVector.y) > Mathf.Sin(Mathf.PI / 8))
        //{

        //}
        

        // DraggedDirection draggedDir;
        //if (positiveX > positiveY)
        //{
        //    draggedDir = (dragVector.x > 0) ? DraggedDirection.Right : DraggedDirection.Left;
        //}
        //else
        //{
        //    draggedDir = (dragVector.y > 0) ? DraggedDirection.Up : DraggedDirection.Down;
        //}


        //Debug.Log(draggedDir);
        // return draggedDir;
        return DraggedDirection.Up;
    }

    private Vector2 CheckQuadrant(float x, float y)
    {
        return x > 0 ? y > 0 ? new Vector2(1, 1) : new Vector2(1, -1) : y > 0 ? new Vector2(-1, 1) : new Vector2(-1, -1);
    }
}
