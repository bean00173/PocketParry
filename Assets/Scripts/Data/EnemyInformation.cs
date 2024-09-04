using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyInfo
{
    public GameObject prefab;
    public EnemyType enemyType;
    public DifficultyClass difficultyClass;
}

public enum EnemyType
{
    Farmer_Spear,
    Farmer_Sword,
    Town_Spear,
    Town_Sword,
    Ronin_Spear,
    Ronin_Sword,
    City_Guard_Spear,
    City_Guard_Sword,
    Lord_Spear,
    Lord_Sword
}

public enum DifficultyClass
{
    Standard,
    Miniboss,
    Boss
}

[CreateAssetMenu(fileName = "AttackInformation", menuName = "ScriptableObjects/EnemyInformation")]
public class EnemyInformation : ScriptableObject
{
    public List<EnemyInfo> enemies;
}

