using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MapManager : MonoBehaviour
{
    public RectTransform mapContent;
    public GameObject recenterButton;
    private Vector2 lastCoordinates;
    private MapLevel targetLevel;
    bool playingTrans;
    Vector2 targetPos;

    bool recentering;

    private List<MapLevel> levels = new List<MapLevel>();
    private int currentLevel;

    Vector2 closestPos;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in mapContent.transform)
        {
            try
            {
                if (child.GetComponent<MapLevel>())
                {
                    levels.Add(child.GetComponent<MapLevel>());
                }
            }
            catch (Exception e)
            {
                Debug.Log($"{e.GetType()}, no maplevel component bruzza");
            }
        }

        targetPos = GetLocalPosition(levels[currentLevel]);
    }

    // Update is called once per frame
    void Update()
    {
        if(!recentering) lastCoordinates = mapContent.anchoredPosition;
        CheckProximity();
    }

    private void ReCenter()
    {
        recentering = true;

        StartCoroutine(LerpToPos(targetPos, false));
    }

    private void ReturnToLast()
    {

        StartCoroutine(LerpToPos(lastCoordinates, true));
    }

    public void AutoSnapNearest()
    {
        foreach (MapLevel level in levels)
        {
            if(closestPos == null)
            {
                closestPos = GetLocalPosition(level);
            }
            else
            {
                float distanceNew = Vector2.Distance(-1 * mapContent.anchoredPosition, GetLocalPosition(level));
                float distanceOld = Vector2.Distance(-1 * mapContent.anchoredPosition, closestPos);

                if (distanceNew < distanceOld)
                {
                    closestPos = GetLocalPosition(level);
                }
            }
        }

        if(Vector2.Distance(-1 * mapContent.anchoredPosition, closestPos) < 250)
        {
            SnappedToTarget(closestPos == targetPos);
            StartCoroutine(LerpToPos(closestPos, false));
        }
    }

    private Vector2 GetLocalPosition(MapLevel level)
    {
        return level.GetComponent<RectTransform>().localPosition;
    }

    public void RecenterButtonAction()
    {
        if (!playingTrans)
        {
            if (Vector2.Distance(-1 * mapContent.anchoredPosition, targetPos) < 50)
            {
                Debug.Log(Vector2.Distance(targetPos, -1 * lastCoordinates));
                if (Vector2.Distance(targetPos, -1 * lastCoordinates) > 250)
                {
                    ReturnToLast();
                    SnappedToTarget(false);
                }
                else
                {
                    Debug.Log("GET OUTT!!!!");
                }
                //ReturnToLast();
                //SnappedToTarget(false);
            }
            else
            {
                ReCenter();
                SnappedToTarget(true);
            }
        }
        
    }

    private void SnappedToTarget(bool snapped)
    {
        recenterButton.transform.GetChild(0).gameObject.SetActive(snapped);
        recenterButton.transform.GetChild(1).gameObject.SetActive(snapped);
    }

    public void CheckProximity()
    {
        if (Vector2.Distance(mapContent.anchoredPosition, -1 * targetPos) > 25 && !playingTrans)
        {
            recentering = false;
            SnappedToTarget(false);
        }
    }

    private IEnumerator LerpToPos(Vector2 targetPosition, bool self)
    {

        playingTrans = true;

        float time = 0;
        float duration = 1f;

        while (time < duration)
        {
            if(time > duration * .4f)
            {
                if (self)
                {
                    mapContent.anchoredPosition = targetPosition; 
                    break;
                }
                else mapContent.anchoredPosition = -1 * targetPosition; break;
                
            }
            if(self) mapContent.anchoredPosition = Vector2.Lerp(mapContent.anchoredPosition, targetPosition, (time / duration));
            else mapContent.anchoredPosition = Vector2.Lerp(mapContent.anchoredPosition, -1 * targetPosition, (time / duration));
            time += Time.deltaTime;

            yield return null;
        }

        if(self) recentering = false;
        playingTrans = false;
    }
}
