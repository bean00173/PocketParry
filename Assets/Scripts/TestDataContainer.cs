using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TestDataContainer", menuName = "ScriptableObjects/TestDataContainer")]
public class TestDataContainer : ScriptableObject
{
    public Attack[] attacks;
}
