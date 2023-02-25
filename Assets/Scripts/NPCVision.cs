using UnityEngine;

public class NPCVision : MonoBehaviour
{
    [SerializeField] private float viewDistance = 100.0f;   // La distance maximale � laquelle l'ennemi peut voir
    [SerializeField] private float viewAngle = 180.0f;     // L'angle de vision de l'ennemi

    public Transform target;                               // La cible que l'ennemi cherche � voir

       

    public bool CanSeeTarget()
    {       
        // V�rifie l'angle entre l'ennemi et la cible
        Vector3 directionToTarget = target.position - transform.position;
        float angleToTarget = Vector3.Angle(directionToTarget, transform.forward);
        if (angleToTarget > viewAngle * 0.5f)
        {
            Debug.Log("La cible n'est pas dans le champ de vision.");
            // Si la cible est en dehors de l'angle de vision, on ne peut pas la voir
            return false;
        }

        // V�rifie s'il y a des obstacles entre l'ennemi et la cible
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, directionToTarget, out hitInfo, viewDistance))
        {
            if (hitInfo.collider.gameObject != target.gameObject)
            {
                Debug.Log("La cible est cach�e.");
                // Si l'obstacle rencontr� n'est pas la cible, on ne peut pas la voir
                return false;
            }
        }

        Debug.Log("La cible a �t� vue.");
        // Si aucune condition n'est remplie, la cible est visible
        return true;
    }   
}
