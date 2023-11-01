using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RandomizedPrefabNetworkManager : NetworkManager
{
    public GameObject[] prefabs;

    private List<int> usedIndexes = new List<int>();

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        int randomIndex = GetRandomIndex();
        GameObject player = Instantiate(prefabs[randomIndex]);
        NetworkServer.AddPlayerForConnection(conn, player);
    }

    private int GetRandomIndex()
    {
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, prefabs.Length);
        } while (usedIndexes.Contains(randomIndex));

        usedIndexes.Add(randomIndex);
        if (usedIndexes.Count == prefabs.Length)
        {
            usedIndexes.Clear();
        }

        return randomIndex;
    }
}
