using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.AI;

public class EnemyMovement : NetworkBehaviour
{
    private NavMeshAgent navMeshAgent;
    private GameObject player;


    public GameObject FindClosestPlyaer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject closestPlayer = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = player;
            }
        }

        return closestPlayer;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindClosestPlyaer();

        // Set the destination of the NavMeshAgent to the closest player's position
        //navMeshAgent.SetDestination(player.transform.position);
    }

    void Update()
    {
        // Only update movement on the server
        if (!isServer)
            return;

        // If the player is dead, stop moving
        //if (player == null)
        //{
        //    navMeshAgent.isStopped = true;
        //    return;
        //}

        // Update the closest player's position
        player = FindClosestPlyaer();

        // Set the destination of the NavMeshAgent to the closest player's position
        navMeshAgent.SetDestination(player.transform.position);


    }
}
