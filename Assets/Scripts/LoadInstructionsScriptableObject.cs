using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LoadInstructions", menuName = "ScriptableObjects/LoadInstructions")]

public class LoadInstructionsScriptableObject : ScriptableObject
{
    public List<Player> players;
    public string message;
}
