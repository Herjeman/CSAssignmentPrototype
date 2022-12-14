using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _teamSpawns;
    [SerializeField] private GameObject _worm;

    public List<Player> SpawnWorms(List<Player> players)
    {
        List<Player> playerList = new List<Player>();
        foreach (Player player in players)
        {
            for (int i = 0; i < player.teamSize; i++)
            {
                Transform spawnLocation = _teamSpawns[player.playerIndex].transform.GetChild(i);
                GameObject newWorm = Instantiate(_worm, spawnLocation.position, spawnLocation.rotation);

                MeshRenderer[] newWormMeshRenderers = newWorm.GetComponentsInChildren<MeshRenderer>();
                ApplyTeamColor(newWormMeshRenderers, player.teamColor);

                newWorm.GetComponent<WormController>().Init(player);
                player.worms.Add(newWorm);
                Debug.Log($"Spawned a worm at {spawnLocation.position}");
            }
        }
        return playerList;
    }

    private void ApplyTeamColor(MeshRenderer[] meshRenderers, Color teamColor)
    {
        foreach (MeshRenderer renderer in meshRenderers)
        {
            if (renderer.gameObject.tag != "Weapon")
            {
                renderer.material.color = teamColor;
            }
        }
    }
}
