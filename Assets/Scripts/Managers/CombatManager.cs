using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class CombatManager : MonoBehaviour
{
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

    int currentEnemyMax;

    GameObject currentEnemy;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        impulseSource = this.GetComponent<CinemachineImpulseSource>();
        PlayerInput.Instance.inputHandled.AddListener(playerArmBehaviour.Parry);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy(string enemyType)
    {
        EnemyBehaviour enemy = Instantiate(enemyInfo.enemies.Find((x) => x.enemyType == (EnemyType)System.Enum.Parse(typeof(EnemyType), enemyType)).prefab, spawnPoint).GetComponent<EnemyBehaviour>();
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
        stanceIndicator.UpdateStanceBar(score);
        scoreText.text = score.ToString();

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
    }
}
