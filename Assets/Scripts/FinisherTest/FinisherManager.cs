using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum ArrowDirection
{
    Up,
    Down,
    Left,
    Right
}

public class FinisherManager : MonoBehaviour
{
    public float minLength, maxLength;
    private float length;

    public GameObject arrowPrefab;

    FinisherArrow lastArrow;
    ArrowDirection arrowDirection;

    int arrowIndex;

    bool complete;

    // Start is called before the first frame update
    void Start()
    {
        length = Random.Range(minLength, maxLength);
        PlayerInput.Instance.inputHandled.AddListener(InputCheck);

        for (int i = 0; i < length; i++)
        {
            FinisherArrow arrow = Instantiate(arrowPrefab, this.transform).GetComponent<FinisherArrow>();
            arrow.direction = SelectDirection(arrow);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InputCheck(ParryDirection dir)
    {
        if (!complete)
        {
            if (arrowIndex >= this.transform.childCount)
            {
                complete = true;
            }
            else if (dir.ToString() == this.transform.GetChild(arrowIndex).GetComponent<FinisherArrow>().direction.ToString())
            {
                this.transform.GetChild(arrowIndex).GetComponent<Image>().color = Color.green;
                arrowIndex++;
            }
            else
            {
                this.transform.GetChild(arrowIndex).GetComponent<Image>().color = Color.red;
            }
        }
        else
        {
            Debug.Log("Finisher Over");
        }
    }

    private ArrowDirection SelectDirection(FinisherArrow newArrow)
    {
        if(lastArrow == null)
        {
            lastArrow = newArrow;
            int x = Random.Range(0, 3);
            switch (x)
            {
                case 0: return ArrowDirection.Up;
                case 1: return ArrowDirection.Down;
                case 2: return ArrowDirection.Left;
                case 3: return ArrowDirection.Right;

            }
        }
        else
        {
            List<ArrowDirection> directionArray = new List<ArrowDirection>(7);

            foreach(ArrowDirection direction in System.Enum.GetValues(typeof(ArrowDirection)))
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
            return directionArray[Random.Range(0, 7)];
            
        }


        return default;
    }
}
