using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinisherManager : MonoBehaviour
{
    public float minLength, maxLength;
    private float length;

    public GameObject arrowPrefab;

    FinisherArrow lastArrow;
    int arrowIndex;
    bool complete;

    // Start is called before the first frame update
    void Start()
    {
        PlayerInput.Instance.inputHandled.AddListener(InputCheck);
        GenerateFinisherPuzzle();
    }

    // Update is called once per frame
    void Update()
    {

        if (arrowIndex >= this.transform.childCount )
        {
            lastArrow = null;
            //complete = false;
            arrowIndex = 0;
            ResetUI();
            GenerateFinisherPuzzle();
        }
    }

    private void GenerateFinisherPuzzle()
    {
        length = Random.Range(minLength, maxLength);

        for (int i = 0; i < length; i++)
        {
            FinisherArrow arrow = Instantiate(arrowPrefab, this.transform).GetComponent<FinisherArrow>();
            arrow.direction = SelectDirection(arrow);

        }
    }

    private void InputCheck(ParryDirection dir, float x)
    {
        if (dir.ToString() == this.transform.GetChild(arrowIndex).GetComponent<FinisherArrow>().direction.ToString())
        {
            this.transform.GetChild(arrowIndex).GetComponent<Image>().color = Color.green;
            arrowIndex++;
            Debug.Log(arrowIndex);
        }
        else
        {
            this.transform.GetChild(arrowIndex).GetComponent<Image>().color = Color.red;
        }
    }

    private ParryDirection SelectDirection(FinisherArrow newArrow)
    {
        if(lastArrow == null)
        {
            List<ParryDirection> directionArray = new List<ParryDirection>(8);

            foreach (ParryDirection direction in System.Enum.GetValues(typeof(ParryDirection)))
            {
                directionArray.Add(direction);
            }

            lastArrow = newArrow;
            return directionArray[Random.Range(0, 8)];
        }
        else
        {
            List<ParryDirection> directionArray = new List<ParryDirection>(15);

            foreach(ParryDirection direction in System.Enum.GetValues(typeof(ParryDirection)))
            {
                if(lastArrow.direction == direction)
                {
                    directionArray.Add(direction);
                }
                else
                {
                    for(int i = 0; i < 2; i++)
                    {
                        directionArray.Add(direction);
                    }
                }
            }

            lastArrow = newArrow;
            return directionArray[Random.Range(0, 15)];
            
        }
    }

    private void ResetUI()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
