using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    MapLevel closestLevel;

    public UnityEvent onDemoComplete = new UnityEvent();

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

        currentLevel = levels.IndexOf(levels.Find((x) => x.levelId == GameManager.Instance.selectedLevelId));
        targetPos = GetLocalPosition(levels[currentLevel]);
        targetLevel = levels[currentLevel];
        if (Vector2.Distance(-1 * mapContent.anchoredPosition, targetPos) > 250)
        {
            mapContent.anchoredPosition = -1 * targetPos;
            targetLevel.CenteredOnScreen(true);
        }

        if (GameManager.Instance.selectedLevel != null)
        {
            if (GameManager.Instance.selectedLevel.endless)
            {
                levels.Find((x) => x.levelId == GameManager.Instance.selectedLevelId).StoreEndlessScore(GameManager.Instance.endlessScore);
            }
            if (GameManager.Instance.levelBeaten)
            {
                closestPos = GetLocalPosition(levels[currentLevel]);
                closestLevel = levels[currentLevel];

                levels.Find((x) => x.levelId == GameManager.Instance.selectedLevelId).CampaignBeaten();

                if (levels[currentLevel + 1].isUnlocked)
                {
                    currentLevel++;
                    targetPos = GetLocalPosition(levels[currentLevel]);
                    targetLevel = levels[currentLevel];
                }
                else
                {
                    onDemoComplete.Invoke(); // TEMPORARY FOR DEMO PURPOSES
                    Debug.Log("GAME OVER");
                }

            }
        }
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
                    closestLevel = level;
                    closestPos = GetLocalPosition(level);
                }
            }
        }

        if(Vector2.Distance(-1 * mapContent.anchoredPosition, closestPos) < 250)
        {
            closestLevel.CenteredOnScreen(true);
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
        //if (Vector2.Distance(mapContent.anchoredPosition, -1 * closestPos) > 25 && !playingTrans)
        //{
        //    closestLevel.CenteredOnScreen(false);
        //}

        if (Vector2.Distance(mapContent.anchoredPosition, -1 * closestPos /*targetPos */) > 25 && !playingTrans)
        {
            if(closestPos == targetPos)
            {
                recentering = false;
                SnappedToTarget(false);
            }
            //recentering = false;
            //SnappedToTarget(false);
            if(closestLevel != null)
            {
                closestLevel.CenteredOnScreen(false);
            }
        }

    }

    private IEnumerator LerpToPos(Vector2 targetPosition, bool self)
    {
        playingTrans = true;

        //if (/*!self && */recentering)
        //{
        //    if(closestPos != targetPos)
        //    {
        //        levels[currentLevel].CenteredOnScreen(!self);
        //        closestLevel.CenteredOnScreen(self);
        //    }
        //    else
        //    {
        //        levels[currentLevel].CenteredOnScreen(!self);
        //    }
        //}
        
        

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
            else if(time > duration * .2f)
            {
                if (recentering)
                {
                    if (closestPos != targetPos)
                    {
                        levels[currentLevel].CenteredOnScreen(!self);
                        if(Vector2.Distance(-1 * targetPosition, closestPos) < 250)
                        {
                            if (self)
                            {
                                closestLevel.CenteredOnScreen(self);
                            }
                        }
                        else if (!self)
                        {
                            closestLevel.CenteredOnScreen(self);
                        }

                        //closestLevel.CenteredOnScreen(self);

                    }
                    else
                    {
                        levels[currentLevel].CenteredOnScreen(!self);
                    }
                }
            }
            if(self) mapContent.anchoredPosition = Vector2.Lerp(mapContent.anchoredPosition, targetPosition, (time / duration));
            else mapContent.anchoredPosition = Vector2.Lerp(mapContent.anchoredPosition, -1 * targetPosition, (time / duration));
            time += Time.deltaTime;

            yield return null;
        }

        //if (/*!self && */recentering)
        //{
        //    if (closestPos != targetPos)
        //    {
        //        levels[currentLevel].CenteredOnScreen(!self);
        //        closestLevel.CenteredOnScreen(self);
        //    }
        //    else
        //    {
        //        levels[currentLevel].CenteredOnScreen(!self);
        //    }
        //}

        if (self) recentering = false;
        playingTrans = false;
    }
}
