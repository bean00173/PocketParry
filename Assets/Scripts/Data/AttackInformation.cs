using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttackInfo
{
    public AnimationClip clip;
    public ParryDirection parryDirection;
    public bool canCombo;
    public bool isInstaKill;
}

[CreateAssetMenu(fileName = "AttackInformation", menuName = "ScriptableObjects/AttackInformation")]
public class AttackInformation : ScriptableObject
{
    public List<AttackInfo> attackInfo;
}
