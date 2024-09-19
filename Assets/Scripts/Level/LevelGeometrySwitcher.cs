using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeometrySwitcher : MonoBehaviour
{
    public GameObject tutorialPrefab, aizuPrefab, okawachiyamaPrefab, shimukappuPrefab, kanazawaPrefab, himejiPrefab;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        switch (GameManager.Instance.selectedLevel.levelId)
        {
            case LevelID.tutorial: prefab = tutorialPrefab; break;
            case LevelID.aizu: prefab = aizuPrefab;  break;
            case LevelID.okawachiyama: prefab = okawachiyamaPrefab;  break;
            case LevelID.shimukappu: prefab = shimukappuPrefab;  break;
            case LevelID.kanazawa: prefab = kanazawaPrefab; break;
            case LevelID.himeji: prefab = himejiPrefab;  break;
        }

        Instantiate(prefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
