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

    public GameObject win;
    public Button exitButton, quitButton;

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

        this.levelInfo = GameManager.Instance.selectedLevel;
        GameManager.Instance.UpdateLevelReference(exitButton, quitButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SpawnEnemy();
        SetupGameUI();
    }


    public void SpawnEnemy()
    {
        EnemyBehaviour enemy = Instantiate(SelectEnemyToSpawn(), spawnPoint).GetComponent<EnemyBehaviour>();
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
        enemiesBeaten++;

        if (enemiesBeaten >= levelInfo.selectableEnemies[levelIndex].spawnCount)
        {
            levelIndex++;
            if(levelIndex > levelInfo.selectableEnemies.Count - 1)
            {
                Debug.Log("GAME OVER YOU WIN LETS GOOOO");
                GameOver();
            }
            else
            {
                enemiesBeaten = 0;
                Destroy(currentEnemy);
                SpawnEnemy();
                score = 0;
            } 
        }
        else
        {
            Destroy(currentEnemy);
            SpawnEnemy();
            score = 0;
        }
    }

    public bool RandomChance(float probability)
    {
        return Random.value <= probability;
    }

    private GameObject SelectEnemyToSpawn()
    {
        List<GameObject> selectableEnemies = new List<GameObject>();

        if (!levelInfo.endless)
        {
            for(int i = 0; i < levelInfo.selectableEnemies[levelIndex].enemyTypes.Length; i++)
            {
                selectableEnemies.Add(enemyInfo.enemies.Find((x) => (x.enemyType == (EnemyType)System.Enum.Parse(typeof(EnemyType), levelInfo.selectableEnemies[levelIndex].enemyTypes[i].ToString()) && (x.difficultyClass == (DifficultyClass)System.Enum.Parse(typeof(DifficultyClass), levelInfo.selectableEnemies[levelIndex].difficultyType.ToString()) && (x.appearanceStage == (LevelStage)System.Enum.Parse(typeof(LevelStage), levelInfo.selectableEnemies[levelIndex].stage.ToString()))))).prefab);
            }
        }
        else
        {
            foreach (SelectableEnemy selectable in levelInfo.selectableEnemies)
            {
                for (int i = 0; i < selectable.enemyTypes.Length; i++)
                {
                    selectableEnemies.Add(enemyInfo.enemies.Find((x) => (x.enemyType == (EnemyType)System.Enum.Parse(typeof(EnemyType), selectable.enemyTypes[i].ToString()) && (x.difficultyClass == (DifficultyClass)System.Enum.Parse(typeof(DifficultyClass), selectable.difficultyType.ToString()) && (x.appearanceStage == (LevelStage)System.Enum.Parse(typeof(LevelStage), selectable.stage.ToString()))))).prefab);
                }
            }
        }

        return selectableEnemies[Random.Range(0, selectableEnemies.Count)];
        //GameObject[] stageMatchPrefabs = enemyInfo.enemies.FindAll((x) => x.appearanceStage == (LevelStage)System.Enum.Parse(typeof(LevelStage), levelInfo.selectableEnemies[levelIndex].stage.ToString()));
        //GameObject[] difficultyMatchPrefabs = enemyInfo.enemies.FindAll((x) => x.difficultyClass == (DifficultyClass)System.Enum.Parse(typeof(DifficultyClass), levelInfo.selectableEnemies[levelIndex].difficultyType.ToString()));
    }

    public void GameOver()
    {
        GameManager.Instance.levelBeaten = true;
        win.SetActive(true);
    }

}
