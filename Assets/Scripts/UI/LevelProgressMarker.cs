using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LevelProgressMarker : MonoBehaviour
{
    LevelInformation levelInfo;
    //List<GameObject> icons = new List<GameObject>();
    List<ProgressIcon> icons = new List<ProgressIcon>();
    public DynamicLayoutGroupBackground dlgb;

    // Start is called before the first frame update
    void Start()
    {
        levelInfo = GameManager.Instance.selectedLevel;

        if (!levelInfo.endless)
        {
            foreach (SelectableEnemy selectable in levelInfo.selectableEnemies)
            {
                for(int i = 0; i < selectable.spawnCount; i++)
                {
                    GameObject prefab = Resources.Load<GameObject>($"UI/{selectable.difficultyType}");
                    icons.Add(Instantiate(prefab, this.transform).GetComponent<ProgressIcon>());
                    
                    if(i > 0)
                    {
                        icons[i].status = enemyStatus.next;
                    }
                    else
                    {
                        icons[i].status = enemyStatus.current;
                    }
                }
            }
        }
        else
        {
            this.transform.parent.gameObject.SetActive(false);
        }

        dlgb.SetSize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSelectedChild(int enemiesBeaten)
    {
        foreach(ProgressIcon icon in icons)
        {
            if (icons.IndexOf(icon) < enemiesBeaten)
            {
                // do x mark
                icon.status = enemyStatus.defeated;
            }
            else if (icons.IndexOf(icon) > enemiesBeaten)
            {
                //gameObject.GetComponent<Image>().color = new Color(gameObject.GetComponent<Image>().color.r, gameObject.GetComponent<Image>().color.g, gameObject.GetComponent<Image>().color.b, .5f);

                icon.status = enemyStatus.next;
            }
            else
            {
                // do swords logo
                icon.status = enemyStatus.current;
            }
        }
    }
}
