using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatManager : MonoBehaviour
{
    public EnemyInformation enemyInfo;
    public TimingSlider timingSlider;
    public TextMeshProUGUI scoreText;
    public Transform spawnPoint;
    public GameObject tempMenuButtons;
    public Image tempDetector;

    public static CombatManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy(string enemyType)
    {
        GameObject enemy = Instantiate(enemyInfo.enemies.Find((x) => x.enemyType == (EnemyType)System.Enum.Parse(typeof(EnemyType), enemyType)).prefab, spawnPoint); 
        timingSlider.SetCurrentEnemy(enemy.GetComponent<EnemyBehaviour>());
    }

    public void SetupGameUI()
    {
        tempDetector.gameObject.SetActive(true);
        timingSlider.gameObject.SetActive(true);
        tempMenuButtons.SetActive(false);
    }
}
