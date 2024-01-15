using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public enum AtkType
    {
        left,
        right,
        top
    }
    private enum DraggedDirection
    {
        Up,
        Down,
        Right,
        Left
    }

    bool parryable;
    AtkType currentAtkType;

    public Renderer detector;
    private bool attacking;

    public GameObject lIndicator, rIndicator, tIndicator;

    public StanceIndicator enemyStance;
    public TextMeshProUGUI enemyName;
    int score;

    private EnemyBehaviour currentEnemy;

    // Start is called before the first frame update
    void Start()
    {
        currentEnemy = this.GetComponentInChildren<EnemyBehaviour>();
        enemyStance.SetupBar(currentEnemy.enemyStats.health);
    }

    // Update is called once per frame
    void Update()
    {
        //if (!attacking)
        //{
        //    StartCoroutine(TestAttack());
        //}

        enemyName.text = currentEnemy.enemyStats.name;
    }


    public void ParryStart(AtkType attackType)
    {
        detector.material.color = Color.yellow;

        parryable = true;
        currentAtkType = attackType;
        Debug.Log("Parry Now!");
    }

    public void ParryEnd()
    {
        parryable = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("TAP");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;
        DraggedDirection input = GetDragDirection(dragVectorDirection);

        if (CheckValidParry(input))
        {
            Debug.Log("Parried!");

            UpdateScore();
            //detector.material.color = Color.green;

            currentEnemy.Parried();
        }
        else
        {
            //score--;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

    private DraggedDirection GetDragDirection(Vector3 dragVector)
    {
        float positiveX = Mathf.Abs(dragVector.x);
        float positiveY = Mathf.Abs(dragVector.y);
        DraggedDirection draggedDir;
        if (positiveX > positiveY)
        {
            draggedDir = (dragVector.x > 0) ? DraggedDirection.Right : DraggedDirection.Left;
        }
        else
        {
            draggedDir = (dragVector.y > 0) ? DraggedDirection.Up : DraggedDirection.Down;
        }
        Debug.Log(draggedDir);
        return draggedDir;
    }

    private bool CheckValidParry(DraggedDirection playerDrag)
    {
        if (!parryable)
        {
            return false;
        }
        else
        {
            switch (currentAtkType)
            {
                case AtkType.left: if (playerDrag == DraggedDirection.Left) return true; break;
                case AtkType.right: if (playerDrag == DraggedDirection.Right) return true; break;
                case AtkType.top: if (playerDrag == DraggedDirection.Up) return true; break;
            }
        }

        return false;
    }

    private void UpdateScore()
    {
        currentEnemy.parryCount++;
        enemyStance.UpdateStanceBar(currentEnemy.parryCount);
    }

    //private IEnumerator TestAttack() // RUDIMENTARY ATTACK CODE ---> WILL BE REPLACED WITH ENEMY BEHAVIOURS
    //{
    //    attacking = true;
    //    detector.material.color = Color.red;

    //    AtkType atkType = Random.value > .66 ? AtkType.left : Random.value > .5 ? AtkType.right : AtkType.top;
    //    switch (atkType)
    //    {
    //        case AtkType.left: lIndicator.SetActive(true); break;
    //        case AtkType.right: rIndicator.SetActive(true); break;
    //        case AtkType.top: tIndicator.SetActive(true); break;
    //    }

    //    yield return new WaitForSeconds(1.0f * Random.value);

    //    ParryStart(atkType);

    //    yield return new WaitForSeconds(1.0f);

    //    ParryEnd();

    //    detector.material.color = Color.red;

    //    lIndicator.SetActive(false);
    //    rIndicator.SetActive(false);
    //    tIndicator.SetActive(false);

    //    attacking = false;
    //}

}
