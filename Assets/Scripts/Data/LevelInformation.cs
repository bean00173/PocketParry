using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SelectableEnemy
{
    public DifficultyClass difficultyType;
    public LevelStage stage;
    public EnemyType[] enemyTypes;
}

[System.Serializable]
public enum LevelStage
{
    stageOne,
    stageTwo,
    stageThree
}

[CreateAssetMenu(fileName = "LevelInformation", menuName = "ScriptableObjects/LevelInformation")]
public class LevelInformation : ScriptableObject
{
    public List<SelectableEnemy> selectableEnemies = new List<SelectableEnemy>();
}
