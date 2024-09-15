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

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in mapContent.transform)
        {
            Debug.Log(child.gameObject.name);

            try
            {
                if (child.GetComponent<MapLevel>().isUnlocked)
                {
                    targetPos = child.GetComponent<RectTransform>().localPosition;
                }
            }
            catch (Exception e)
            {
                Debug.Log($"{e.GetType()}, no maplevel component bruzza");
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

    //public void AutoSnapNearest()
    //{
    //    foreach(Transform child in mapContent.transform)
    //    {
    //        if(child.GetComponent<MapLevel>());
    //    }
    //}

    public void RecenterButtonAction()
    {
        if (!playingTrans)
        {
            if (Vector2.Distance(-1 * mapContent.anchoredPosition, targetPos) < 50)
            {
                ReturnToLast();
                recenterButton.transform.GetChild(0).gameObject.SetActive(false);
                recenterButton.transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                ReCenter();
                recenterButton.transform.GetChild(0).gameObject.SetActive(true);
                recenterButton.transform.GetChild(1).gameObject.SetActive(true);
            }
        }
        
    }

    public void CheckProximity()
    {
        if(Vector2.Distance(mapContent.anchoredPosition, -1 * targetPos) > 25 && !playingTrans)
        {
            recentering = false;
            recenterButton.transform.GetChild(0).gameObject.SetActive(false);
            recenterButton.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    private IEnumerator LerpToPos(Vector2 targetPosition, bool self)
    {

        playingTrans = true;

        float time = 0;
        float duration = 1f;

        while (time < duration)
        {
            Debug.Log($"{time}, {duration}");
            if(time > duration * .4f)
            {
                Debug.Log("Good enough girlies!!");
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
