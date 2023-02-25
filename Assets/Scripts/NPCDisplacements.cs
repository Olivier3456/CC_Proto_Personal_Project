using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCDisplacements : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private NPCVision npcVision;

    [SerializeField] private float walkSpeed = 3.5f;
    [SerializeField] private float runSpeed = 7f;
    
    

    void Start()
    {
        SetRandomDestinationOnTheNavMesh();
    }


    void Update()
    {
        if (npcVision.CanSeeTarget())
        {
            Debug.Log("Déplacement vers la position actuelle de la cible.");
            agent.SetDestination(npcVision.target.position);
            agent.speed = runSpeed;
        }


        if (agent.remainingDistance < 2)
        {
            Debug.Log("Destination atteinte.");
            SetRandomDestinationOnTheNavMesh();
        }
    }


    private void SetRandomDestinationOnTheNavMesh()
    {
        Vector3 newRandomDest = new Vector3(Random.Range(-50f, 50f), 0.5f, Random.Range(-50f, 50f));


        // Vérifie si on peut trouver un point sur le NavMesh à moins de 2m de newRandomDest
        NavMeshHit hit;
        if (NavMesh.SamplePosition(newRandomDest, out hit, 2.0f, NavMesh.AllAreas))
        {
            // Si oui, définit la destination sur le NavMeshAgent
            agent.SetDestination(hit.position);
            agent.speed = walkSpeed;
            Debug.Log("Nouvelle destination random de l'agent : " + hit.position);
        }
        else SetRandomDestinationOnTheNavMesh();
    }
}


