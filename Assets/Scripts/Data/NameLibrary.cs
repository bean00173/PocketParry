using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NameLibrary", menuName = "ScriptableObjects/NameLibrary")]
public class NameLibrary : ScriptableObject
{
    public List<string> firstNames = new List<string>();
    public List<string> lastNames = new List<string>();
}
