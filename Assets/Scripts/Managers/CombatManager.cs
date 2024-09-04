using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatManager : MonoBehaviour
{
    public EnemyInformation enemyInfo;
    public CameraBehaviour cameraBehaviour;
    public StanceIndicator stanceIndicator;
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
        EnemyBehaviour enemy = Instantiate(enemyInfo.enemies.Find((x) => x.enemyType == (EnemyType)System.Enum.Parse(typeof(EnemyType), enemyType)).prefab, spawnPoint).GetComponent<EnemyBehaviour>(); 
        timingSlider.SetCurrentEnemy(enemy);
        cameraBehaviour.UpdateCurrentEnemy(enemy);
    }

    public void SetupGameUI()
    {
        tempDetector.gameObject.SetActive(true);
        timingSlider.gameObject.SetActive(true);
        tempMenuButtons.SetActive(false);
    }
}
