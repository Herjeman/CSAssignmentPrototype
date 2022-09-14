using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnsManager : MonoBehaviour
{
    public int numberOfPlayers;
    public int numberOfTurns;
    public int playerTurn;

    public void SetNumberOfPlayers(int amount)
    {
        numberOfPlayers = amount;
    }

    public void AddTurn()
    {
        numberOfTurns++;
    }

    public void NextPlayer()
    {
        playerTurn++;
        AddTurn();
        if (playerTurn >= numberOfPlayers)
        {
            playerTurn = 0;
        }
    }
}
