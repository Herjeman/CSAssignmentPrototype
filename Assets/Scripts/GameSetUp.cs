using System.Collections.Generic;
using UnityEngine;

public class GameSetUp : MonoBehaviour
{
    [SerializeField] private LoadInstructionsScriptableObject _instructions;
    private List<Player> _players;

    public List<Player> CreatePlayers()
    {
        _players = new List<Player>();
        foreach (string name in _instructions.playerNames)
        {
            Player player = new Player(name, _players.Count, _instructions.wormsPerPlayer);
            _players.Add(player);
        }
        return _players;
    }
}
