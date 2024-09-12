using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class CombatManager : MonoBehaviour
{
    public LevelInformation levelInfo; // TEMPORARILY PUBLICLY AVAILABLE WILL BE SET BY GAME MANAGER AFTER SCENE FLOW COMPLETED
    public EnemyInformation enemyInfo;
    public Transform worldSpaceCanvas;
    public CameraBehaviour cameraBehaviour;
    public PlayerArmBehaviour playerArmBehaviour;
    StanceIndicator stanceIndicator;
    public TextMeshProUGUI scoreText;
    public Transform spawnPoint;
    public GameObject tempMenuButtons;

    public static CombatManager instance;

    CinemachineImpulseSource impulseSource;

    [HideInInspector] public UnityEvent defeated;

    public int score;
    private int totalScore;

    int currentEnemyMax;

    GameObject currentEnemy;

    int enemiesBeaten;
    int levelIndex;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        impulseSource = this.GetComponent<CinemachineImpulseSource>();
        PlayerInput.Instance.inputHandled.AddListener(playerArmBehaviour.Parry);

        //SpawnEnemy("gay");

        Debug.Log(SelectEnemyToSpawn().name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SpawnEnemy(string enemyType)
    {
        EnemyBehaviour enemy = Instantiate(enemyInfo.enemies.Find((x) => x.enemyType == (EnemyType)System.Enum.Parse(typeof(EnemyType), enemyType)).prefab, spawnPoint).GetComponent<EnemyBehaviour>();
        //EnemyBehaviour enemy = Instantiate(SelectEnemyToSpawn(), spawnPoint).GetComponent<EnemyBehaviour>();
        //timingSlider.SetCurrentEnemy(enemy);
        stanceIndicator = enemy.stanceIndicator;
        currentEnemyMax = enemy.enemyStats.health;
        defeated.AddListener(enemy.Defeated);
        enemy.onParrySuccessful.AddListener(ParryImpulse);
        cameraBehaviour.UpdateCurrentEnemy(enemy);
        currentEnemy = enemy.gameObject;
    }

    public void SetupGameUI()
    {
        tempMenuButtons.SetActive(false);
    }

    public void ParryImpulse()
    {
        impulseSource.GenerateImpulseWithForce(.1f);
    }

    public void UpdateScore(int x)
    {
        score += x;
        totalScore += x;
        stanceIndicator.UpdateStanceBar(score);
        scoreText.text = totalScore.ToString();

        if (score >= currentEnemyMax)
        {
            Debug.Log("HES TAPPING HES TAPPING");
            defeated.Invoke();
        }
    }

    public void NewEnemy()
    {
        Destroy(currentEnemy);
        SpawnEnemy(enemyInfo.enemies[Random.Range(0, enemyInfo.enemies.Count)].enemyType.ToString());
        score = 0;
    }

    public bool RandomChance(float probability)
    {
        return Random.value <= probability;
    }

    public void NextStage()
    {
        if(enemiesBeaten >= levelInfo.selectableEnemies[levelIndex].spawnCount)
        {
            levelIndex++;
            enemiesBeaten = 0;
        }
    }

    private GameObject SelectEnemyToSpawn()
    {
        List<GameObject> stageMatchPrefabs = new List<GameObject>();
        foreach(EnemyInfo enemy in enemyInfo.enemies)
        {
            if(enemy.appearanceStage == levelInfo.selectableEnemies[levelIndex].stage)
            {
                if (!stageMatchPrefabs.Contains(enemy.prefab))
                {
                    stageMatchPrefabs.Add(enemy.prefab);
                }
            }
        }

        List<GameObject> difficultyMatchPrefabs = new List<GameObject>();
        foreach (EnemyInfo enemy in enemyInfo.enemies)
        {
            if (enemy.difficultyClass == levelInfo.selectableEnemies[levelIndex].difficultyType)
            {
                if (!difficultyMatchPrefabs.Contains(enemy.prefab))
                {
                    difficultyMatchPrefabs.Add(enemy.prefab);
                }
            }
        }

        List<GameObject> selectableEnemies = new List<GameObject>();
        foreach (GameObject prefab in stageMatchPrefabs)
        {
            if (difficultyMatchPrefabs.Contains(prefab))
            {
                selectableEnemies.Add(prefab);
            }
        }

        return selectableEnemies[Random.Range(0, selectableEnemies.Count)];
        //GameObject[] stageMatchPrefabs = enemyInfo.enemies.FindAll((x) => x.appearanceStage == (LevelStage)System.Enum.Parse(typeof(LevelStage), levelInfo.selectableEnemies[levelIndex].stage.ToString()));
        //GameObject[] difficultyMatchPrefabs = enemyInfo.enemies.FindAll((x) => x.difficultyClass == (DifficultyClass)System.Enum.Parse(typeof(DifficultyClass), levelInfo.selectableEnemies[levelIndex].difficultyType.ToString()));
    }
}
