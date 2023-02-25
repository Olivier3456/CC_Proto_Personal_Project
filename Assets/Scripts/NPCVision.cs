using UnityEngine;

public class NPCVision : MonoBehaviour
{
    [SerializeField] private float viewDistance = 50.0f;   // La distance maximale � laquelle l'ennemi peut voir
    [SerializeField] private float viewAngle = 180.0f;     // L'angle de vision de l'ennemi

    [SerializeField] private Transform target;           // La cible que l'ennemi cherche � voir

    private void Update()
    {
        if (CanSeeTarget())
        {
            Debug.Log("Je vois la cible !");
            
        }
    }

    private bool CanSeeTarget()
    {
        if (target == null)
        {
            // Si la cible n'est pas d�finie, on ne peut pas la voir
            return false;
        }

        // V�rifier la distance entre l'ennemi et la cible
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget > viewDistance)
        {
            // Si la cible est trop �loign�e, on ne peut pas la voir
            return false;
        }

        // V�rifier l'angle entre l'ennemi et la cible
        Vector3 directionToTarget = target.position - transform.position;
        float angleToTarget = Vector3.Angle(directionToTarget, transform.forward);
        if (angleToTarget > viewAngle / 2)
        {
            // Si la cible est en dehors de l'angle de vision, on ne peut pas la voir
            return false;
        }

        // V�rifier s'il y a des obstacles entre l'ennemi et la cible
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, directionToTarget, out hitInfo, distanceToTarget))
        {
            if (hitInfo.collider.gameObject != target.gameObject)
            {
                // Si l'obstacle rencontr� n'est pas la cible, on ne peut pas la voir
                return false;
            }
        }

        // Si aucune condition n'est remplie, la cible est visible
        return true;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
