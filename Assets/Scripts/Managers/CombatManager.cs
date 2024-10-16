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
    public PlayerHealth playerHealth;
    StanceIndicator stanceIndicator;
    public TextMeshProUGUI scoreText;
    public Transform spawnPoint;
    public GameObject tempMenuButtons;
    public TMP_InputField playerNameInput;

    public Button exitButton, quitButton;

    public static CombatManager instance;

    public LevelProgressMarker progressMarker;

    CinemachineImpulseSource impulseSource;

    [HideInInspector] public UnityEvent enemyDefeated;
    public UnityEvent onGameLose = new UnityEvent();
    public UnityEvent onGameWin = new UnityEvent();

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
        if (levelInfo.levelId == LevelID.tutorial)
        {
            PlayerInput.Instance.inputHandled.AddListener(TutorialManager.instance.ReceivePlayerInput);
            PlayGame();
        }
        else
        {
            Invoke(nameof(PlayGame), 2.0f);
        }
        //GameManager.Instance.UpdateLevelReference(exitButton, quitButton);

        scoreText.gameObject.SetActive(this.levelInfo.endless);
        playerHealth.onPlayerDefeat.AddListener(GameLose);
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

        if (levelInfo.levelId == LevelID.tutorial)
        {
            if(enemy is TutorialEnemyBehaviour tutorialEnemy) TutorialManager.instance.SetEnemy(tutorialEnemy);
        }

        //EnemyBehaviour enemy = Instantiate(SelectEnemyToSpawn(), spawnPoint).GetComponent<EnemyBehaviour>();
        //timingSlider.SetCurrentEnemy(enemy);
        stanceIndicator = enemy.stanceIndicator;
        currentEnemyMax = enemy.enemyStats.health;
        enemyDefeated.AddListener(enemy.Defeated);
        //enemyDefeated.AddListener(TutorialManager.instance.TutorialFinished);
        playerHealth.SetEnemy(enemy);
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
        stanceIndicator.UpdateStanceBar(score);

        if (score >= currentEnemyMax)
        {
            Debug.Log("HES TAPPING HES TAPPING");
            enemyDefeated.Invoke();
        }
    }

    public void NewEnemy()
    {
        enemiesBeaten++;
        playerHealth.EnemyBeaten();
        progressMarker.UpdateSelectedChild(enemiesBeaten);

        if (GameManager.Instance.selectedLevelId == LevelID.tutorial)
        {
            TutorialManager.instance.TutorialFinished();
        }
        else if (!levelInfo.endless && enemiesBeaten >= levelInfo.selectableEnemies[levelIndex].spawnCount)
        {
            levelIndex++;
            if (levelIndex > levelInfo.selectableEnemies.Count - 1)
            {
                Debug.Log("GAME OVER YOU WIN LETS GOOOO");
                GameWin();
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
            if(levelInfo.endless) scoreText.text = enemiesBeaten.ToString();
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

    public void GameWin()
    {
        GameManager.Instance.levelBeaten = true;
        onGameWin.Invoke();
    }

    public void GameLose()
    {
        if(!this.levelInfo.endless)
        {
            GameManager.Instance.levelBeaten = false;
        }
        
        currentEnemy.SendMessage("PlayerDefeated");
        onGameLose.Invoke();
    }

    public void LeaveLevel()
    {
        GameManager.Instance.ExitLevel();
    }

    public void ReplayLevel()
    {
        GameManager.Instance.LoadLevel();
    }

    public void Quit()
    {
        GameManager.Instance.Quit();
    }

    public void UploadHighScore()
    {
        LeaderboardManager.instance.UploadEntry(playerNameInput.text, enemiesBeaten);
    }

}
