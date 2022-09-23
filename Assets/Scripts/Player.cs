using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string playerName;
    public int playerIndex;
    public int teamSize;
    private int nextWorm;
    public Color teamColor = Color.white;
    public List<GameObject> worms;

    public Player (string name, int index, int numberOfWorms)
    {
        playerIndex = index;
        playerName = name;
        teamSize = numberOfWorms;
        worms = new List<GameObject>();
    }

    public void NextWorm()
    {
        nextWorm++;
        if (nextWorm > worms.Count - 1)
        {
            nextWorm = 0;
        }
    }

    public GameObject GetWorm()
    {

        return worms[nextWorm];
    }

    public void RemoveDeadWorm(GameObject deadWorm) 
    {
        worms.Remove(deadWorm);
    }
}
