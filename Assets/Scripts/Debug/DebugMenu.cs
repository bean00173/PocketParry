using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Instance.selectedLevelId == LevelID.tutorial)
        {
            this.gameObject.SetActive(false);
        }
        else if (GameManager.Instance.selectedLevel.endless)
        {
            this.transform.GetChild(1).GetChild(0).GetChild(2).gameObject.SetActive(false);
            this.transform.parent.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 100);
        }
        else
        {
            this.transform.parent.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeatLevel()
    {
        CombatManager.instance.GameWin();
    }
}
