using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Cinemachine;
using RehtseStudio.FreeLookCamera3rdPersonCharacterController.Scripts;
using UnityEngine.AI;

public class NetworkManagerFleshPit : NetworkManager
{

    [Header("Player Prefabs")]
    public GameObject[] playerPrefabs;

    [Header("Monster Prefabs")]
    public GameObject[] monsterPrefabs;

    public GameObject selectedPlayerPrefab;
    public bool addplayer;
    private List<int> availablePrefabIndices;

    public NavMeshTriangulation Triangulation;
    public int numInstancesPerPrefab = 5;
    public bool canspawn = false;
 


    public override void OnStartServer()
    {

        base.OnStartServer();

        if (playerPrefabs.Length == 0)
        {
            Debug.LogError("No player prefabs found.");
            return;
        }

        GameObject randomPlayerPrefab = playerPrefabs[Random.Range(0, playerPrefabs.Length)];
        playerPrefab = randomPlayerPrefab;

        availablePrefabIndices = new List<int>();
        for (int i = 0; i < playerPrefabs.Length; i++)
        {
            availablePrefabIndices.Add(i);
        }

    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        //code characters
        foreach (GameObject playerPrefab in playerPrefabs)
        {
            NetworkClient.RegisterPrefab(playerPrefab);
            addplayer = true;
        }

        //code monsters
        foreach (GameObject monsterPrefab in monsterPrefabs)
        {
            NetworkClient.RegisterPrefab(monsterPrefab);
        }

    }

    public override void OnStopClient()
    {
        base.OnStopClient();

        if (NetworkClient.active && !NetworkServer.active)
        {
            Debug.Log("Client: Stopped");
        }
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        if (availablePrefabIndices.Count == 0)
        {
            Debug.LogError("No more player prefabs available.");
            return;
        }

        int index = Random.Range(0, availablePrefabIndices.Count);
        int prefabIndex = availablePrefabIndices[index];
        availablePrefabIndices.RemoveAt(index);

        GameObject selectedPlayerPrefab = playerPrefabs[prefabIndex];

        GameObject player = Instantiate(selectedPlayerPrefab);
        Debug.Log("Server: Instantiating player prefab " + selectedPlayerPrefab.name);
        NetworkServer.AddPlayerForConnection(conn, player);
       

        foreach (GameObject monsterPrefab in monsterPrefabs)
        {
            for (int i = 0; i < numInstancesPerPrefab; i++)
            {
                Vector3 randomPos = RandomNavmeshLocation(30f);
                GameObject newMonster = Instantiate(monsterPrefab, randomPos, Quaternion.identity);
                NetworkServer.Spawn(newMonster);
            }
        }


    }

    private Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, radius, 1);
        return hit.position;
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();

        Debug.Log("Connected to server!");
        
    }


    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        base.OnServerConnect(conn);
        Debug.Log("Client connected to server.");
    }


}
