using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinisherManager : MonoBehaviour
{
    public static FinisherManager Instance;

    public float minLength, maxLength;
    private float length;

    public GameObject arrowPrefab;

    FinisherArrow lastArrow;
    int arrowIndex;
    bool complete;

    bool inputPassed = true;

    bool testing;

    public bool finisherActive { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerInput.Instance.inputHandled.AddListener(InputCheck);
        //GenerateFinisherPuzzle();
    }

    // Update is called once per frame
    void Update()
    {
        if (finisherActive)
        {
            if (arrowIndex >= this.transform.childCount)
            {
                lastArrow = null;
                //complete = false;
                arrowIndex = 0;
                ResetUI();
                if (testing)
                {
                    GenerateFinisherPuzzle();
                }
                else if(CombatManager.instance.playerArmBehaviour.attacking == true)
                {
                    //CombatManager.instance.playerArmBehaviour.finish = true;
                    CombatManager.instance.playerArmBehaviour.GetComponent<Animator>().SetTrigger("finisher");
                    CombatManager.instance.playerArmBehaviour.GetComponent<Animator>().ResetTrigger("Parry");
                    CombatManager.instance.playerArmBehaviour.attacking = false;
                }
            }

            if (PlayerInput.Instance.DoingInput && inputPassed) InputCheck(PlayerInput.Instance.InputDirection);
            else inputPassed = !PlayerInput.Instance.DoingInput;
        }   
    }

    public void FinisherComplete()
    {
        finisherActive = false;
        CombatManager.instance.NewEnemy();
        CombatManager.instance.playerArmBehaviour.finish = false;
    }

    public void StopFinisher()
    {
        CombatManager.instance.playerArmBehaviour.finish = true;
        CombatManager.instance.playerArmBehaviour.attacking = false;


        finisherActive = false;
        ResetUI();
    }

    public void StartFinisher(bool testing)
    {
        this.testing = testing;

        CombatManager.instance.playerArmBehaviour.attacking = true;

        GenerateFinisherPuzzle();
        finisherActive = true;
    }

    private void GenerateFinisherPuzzle()
    {
        this.GetComponent<Image>().color = new Color(this.GetComponent<Image>().color.r, this.GetComponent<Image>().color.g, this.GetComponent<Image>().color.b, .4f);

        length = Random.Range(minLength, maxLength);

        for (int i = 0; i < length; i++)
        {
            FinisherArrow arrow = Instantiate(arrowPrefab, this.transform).GetComponent<FinisherArrow>();
            arrow.direction = SelectDirection(arrow);

        }
    }

    private void InputCheck(ParryDirection dir)
    {
        if (finisherActive)
        {
            inputPassed = false;

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
        this.GetComponent<Image>().color = new Color(this.GetComponent<Image>().color.r, this.GetComponent<Image>().color.g, this.GetComponent<Image>().color.b, 0);
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        arrowIndex = 0;
    }
}
