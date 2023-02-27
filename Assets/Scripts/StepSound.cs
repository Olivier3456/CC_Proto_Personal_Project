using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StepSound : MonoBehaviour
{
    [SerializeField] private NPCDisplacements npcDisplacements;

    public UnityEvent step;

   

    public void Step()
    {
        step.Invoke();
    }
}
