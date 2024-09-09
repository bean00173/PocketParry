using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;

public class CombatManager : MonoBehaviour
{
    public EnemyInformation enemyInfo;
    public CameraBehaviour cameraBehaviour;
    public PlayerArmBehaviour playerArmBehaviour;
    public StanceIndicator stanceIndicator;
    public TextMeshProUGUI scoreText;
    public Transform spawnPoint;
    public GameObject tempMenuButtons;

    public static CombatManager instance;

    CinemachineImpulseSource impulseSource;

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
        enemy.onParrySuccessful.AddListener(ParryImpulse);
        cameraBehaviour.UpdateCurrentEnemy(enemy);
    }

    public void SetupGameUI()
    {
        tempMenuButtons.SetActive(false);
    }

    public void ParryImpulse()
    {
        impulseSource.GenerateImpulseWithForce(.1f);
    }
}
