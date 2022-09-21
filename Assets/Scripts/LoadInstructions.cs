using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LoadInstructions", menuName = "LoadInstructions")]

public class LoadInstructions : ScriptableObject
{
    public List<Player> players;

    public string message;
}
