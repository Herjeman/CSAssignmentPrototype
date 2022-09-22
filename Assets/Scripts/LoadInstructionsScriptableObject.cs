using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LoadInstructions", menuName = "ScriptableObjects/LoadInstructions")]

public class LoadInstructionsScriptableObject : ScriptableObject
{
    public int wormsPerPlayer;
    public List<string> playerNames;
    public List<Color> colors;

    public void Clear() //maybe use awake or validate or smth here, or just call from end game...
    {
        wormsPerPlayer = 0;
        playerNames = new List<string>();
        colors = new List<Color>();
    }
}
