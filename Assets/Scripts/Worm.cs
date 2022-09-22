using UnityEngine;

public class Worm
{
    public string name;
    public Color color;
    public int wormIndex;
    
    public Worm(string newName, int index, Color teamColor)
    {
        name = newName;
        wormIndex = index;
        color = teamColor;
    }
}
