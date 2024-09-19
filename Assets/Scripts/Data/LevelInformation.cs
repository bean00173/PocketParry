using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SelectableEnemy
{
    public DifficultyClass difficultyType;
    public LevelStage stage;
    public EnemyType[] enemyTypes;
    public int spawnCount;
}

[System.Serializable]
public enum LevelStage
{
    stageOne,
    stageTwo,
    stageThree
}

[System.Serializable]
public enum LevelID
{
    tutorial,
    aizu,
    okawachiyama,
    shimukappu,
    kanazawa,
    himeji
}

[CreateAssetMenu(fileName = "LevelInformation", menuName = "ScriptableObjects/LevelInformation")]
public class LevelInformation : ScriptableObject
{
    public LevelID levelId;
    public bool endless;
    public List<SelectableEnemy> selectableEnemies = new List<SelectableEnemy>();
}
