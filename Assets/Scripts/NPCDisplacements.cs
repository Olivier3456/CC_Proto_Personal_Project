using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCDisplacements : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    private Vector3 agentDestination;

    void Start()
    {
        SetRandomDestinationOnTheNavMesh();
    }


    void Update()
    {
        if (agent.remainingDistance < 2)
        {
            SetRandomDestinationOnTheNavMesh();
        }
    }


    public void SetRandomDestinationOnTheNavMesh()
    {
        Vector3 newRandomDest = new Vector3(Random.Range(-50f, 50f), 0.5f, Random.Range(-50f, 50f));


        // Vérifie si la destination est sur le NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(newRandomDest, out hit, 0.1f, NavMesh.AllAreas))
        {
            // Définit la destination sur le NavMeshAgent
            agent.SetDestination(hit.position);
            Debug.Log("Nouvelle destination de l'agent : " + hit.position);
        }
        else SetRandomDestinationOnTheNavMesh();
    }
}


