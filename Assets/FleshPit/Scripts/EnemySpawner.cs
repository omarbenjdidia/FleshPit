using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public Transform Player;
    public int NumberOfEnemiesToSpawn = 5;
    public float SpawnDelay = 1f;
    public GameObject EnemyPrefab;
    public SpawnMethod EnemySpawnMethod = SpawnMethod.RoundRobin;
    public float detectRadius = 5f; // The radius within which the player can be detected

    private NavMeshTriangulation Triangulation;
    private int enemyPrefabIndex = 0;

    private void Start()
    {
        Triangulation = NavMesh.CalculateTriangulation();
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        WaitForSeconds Wait = new WaitForSeconds(SpawnDelay);
        int SpawnedEnemies = 0;

        while (SpawnedEnemies < NumberOfEnemiesToSpawn)
        {
            if (EnemySpawnMethod == SpawnMethod.RoundRobin)
            {
                enemyPrefabIndex = SpawnedEnemies % NumberOfEnemiesToSpawn;
            }
            else if (EnemySpawnMethod == SpawnMethod.Random)
            {
                enemyPrefabIndex = Random.Range(0, NumberOfEnemiesToSpawn);
            }

            SpawnEnemy();
            SpawnedEnemies++;
            yield return Wait;
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemyObject = Instantiate(EnemyPrefab);
        Enemy enemy = enemyObject.GetComponent<Enemy>();

        int vertexIndex = Random.Range(0, Triangulation.vertices.Length);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(Triangulation.vertices[vertexIndex], out hit, 2f, 1))
        {
            enemy.Agent.Warp(hit.position);
        }
        else
        {
            Debug.LogError($"Unable to place NavMeshAgent on NavMesh. Tried to use {Triangulation.vertices[vertexIndex]}");
        }
    }

    public enum SpawnMethod
    {
        RoundRobin,
        Random
    }
}
