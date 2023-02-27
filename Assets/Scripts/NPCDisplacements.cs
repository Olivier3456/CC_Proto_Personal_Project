using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
public class NPCDisplacements : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private NPCVision npcVision;
    [Space(5)]
    [SerializeField] private float walkSpeed = 3.5f;
    [SerializeField] private float runSpeed = 7f;
    [Space(5)]
    private AudioSource audioSource;
    [SerializeField] private AudioClip roarSound;
    [SerializeField] private AudioClip stepSound;
    [Space(5)]
    [SerializeField] private Animator animator;

    private bool hasRoared;   
    private float roarTimer;
    private float roarDelay = 10;

    private float animatorWalkSpeed = 0.5f;
   

    void Start()
    {
        SetRandomDestinationOnTheNavMesh();
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (hasRoared)
        {
            roarTimer += Time.deltaTime;
            if (roarTimer > roarDelay) hasRoared = false;
        }


        if (npcVision.CanSeeTarget())
        {
            Debug.Log("Déplacement vers la position actuelle de la cible.");
            agent.SetDestination(npcVision.target.position);
            agent.speed = runSpeed;
            animator.speed = animatorWalkSpeed * 2;

            if (!hasRoared)
            {
                audioSource.PlayOneShot(roarSound);
                hasRoared = true;
                roarTimer = 0;
            }
        }        



        if (agent.remainingDistance < 2)
        {
            Debug.Log("Destination atteinte.");
            SetRandomDestinationOnTheNavMesh();
            animator.speed = animatorWalkSpeed;
        }
    }


    public void Step()
    {        
        audioSource.PlayOneShot(stepSound);
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


