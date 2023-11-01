using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.AI;

public class SpawnerEnemyNetwork : NetworkBehaviour
{
    public NetworkManagerFleshPit networkManager;
    public NavMeshTriangulation Triangulation;
    public int numInstancesPerPrefab = 10;
    private bool hasSpawned = false;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        networkManager = FindObjectOfType<NetworkManagerFleshPit>();
        if (networkManager == null)
            Debug.Log("NetworkManagerFleshPit not found.");

        Triangulation = NavMesh.CalculateTriangulation();
    }

    void Update()
    {
        if (isLocalPlayer && !hasSpawned && networkManager.canspawn)
        {
            hasSpawned = true;
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        foreach (GameObject monsterPrefab in networkManager.monsterPrefabs)
        {
            for (int i = 0; i < numInstancesPerPrefab; i++)
            {
                int vertexIndex = Random.Range(0, Triangulation.vertices.Length);

                NavMeshHit hit;
                if (NavMesh.SamplePosition(Triangulation.vertices[vertexIndex], out hit, 2f, NavMesh.AllAreas))
                {
                    CmdSpawnEnemy(numInstancesPerPrefab, vertexIndex, monsterPrefab, hit.position);
                }
            }
        }
    }

    [Command]
    private void CmdSpawnEnemy(int numInstances, int vertexIndex, GameObject monsterPrefab, Vector3 position)
    {
        for (int i = 0; i < numInstances; i++)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(position, out hit, 2f, NavMesh.AllAreas))
            {
                GameObject newMonster = Instantiate(monsterPrefab, hit.position, Quaternion.identity);
                NetworkServer.Spawn(newMonster);
                RpcSpawnEnemy(newMonster);
                
            }
        }
    }

    [ClientRpc]
    private void RpcSpawnEnemy(GameObject monster)
    {
        monster.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
    }
}
