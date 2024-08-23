using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attack
{
    public AttackType type;
    public int id;
    public int comboId;
}

public class AttackList
{
    public Attack[] attackList;
}

[System.Serializable]
public enum AttackType
{
    Farmer_TL,
    Farmer_BR,
    Farmer_TR,
    Farmer_BL,
    Farmer_T
}

public class TestAttackData : MonoBehaviour
{
    public TextAsset jsonFile;
    public static TestAttackData instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public Attack GetAttackData(AttackType type)
    //{
    //    AttackList attacksInJson = JsonUtility.FromJson<AttackList>(jsonFile.text);

    //    foreach (Attack attack in attacksInJson.attackList)
    //    {
    //        AttackType jsonType = (AttackType)Enum.Parse(typeof(AttackType), attack.type);
    //        if(jsonType == type)
    //        {
    //            return attack;
    //        }
    //    }

    //    return null;
    //}
}
