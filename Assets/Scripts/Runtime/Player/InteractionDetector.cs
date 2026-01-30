using LuduArtsCase.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

public class InteractionDetector : MonoBehaviour
{
    [SerializeField] private SphereCollider m_InteractionCollider;


    private void Awake() {
        m_InteractionCollider = GetComponent<SphereCollider>();
        m_InteractionCollider.isTrigger = true;
    }


    private readonly HashSet<InteractableObject> m_InteractablesInProximity = new();
    private void OnTriggerEnter(Collider other) {
        
        if(other.gameObject.TryGetComponent<IInteractable>(out var interactable)) {
            var interactableObject = new InteractableObject(interactable, other.transform);
            m_InteractablesInProximity.Add(interactableObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.TryGetComponent<IInteractable>(out var interactable)) {
            m_InteractablesInProximity.RemoveWhere(obj => obj.interactable == interactable);
        }
    }

    public InteractableObject GetClosestInteractable() {
        if(m_InteractablesInProximity.Count == 0) {
            Debug.LogWarning("No interactables within reach.");
            return default;
        }

        InteractableObject closestInteractableObj = m_InteractablesInProximity.ElementAt(0);
        float closestDist = float.MaxValue;
        foreach(InteractableObject interactableObj in m_InteractablesInProximity) {
            float dist = Vector3.Distance(transform.position, interactableObj.transform.position);
            if (dist < closestDist) {
                closestDist = dist;
                closestInteractableObj = interactableObj;
            }
        }

        return closestInteractableObj;
        
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        if (m_InteractionCollider == null) {
            m_InteractionCollider = GetComponent<SphereCollider>();
        }
        Gizmos.DrawWireSphere(m_InteractionCollider.center, m_InteractionCollider.radius);
    }
#endif
}
