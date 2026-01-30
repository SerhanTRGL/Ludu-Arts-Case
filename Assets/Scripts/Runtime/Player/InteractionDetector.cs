using LuduArtsCase.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

public class InteractionDetector : MonoBehaviour
{
    [SerializeField] private float m_InteractionRadius = 5f;
    [SerializeField] private SphereCollider m_InteractionCollider;
    private readonly List<InteractableObject> m_InteractablesInProximity = new();


    private void Awake() {
        m_InteractionCollider = GetComponent<SphereCollider>();
        m_InteractionCollider.isTrigger = true;
        m_InteractionCollider.radius = m_InteractionRadius;
    }


    private void OnTriggerEnter(Collider other) {
        
        if(other.gameObject.TryGetComponent<IInteractable>(out var interactable)) {
            var interactableObject = new InteractableObject(interactable, other.transform);
            m_InteractablesInProximity.Add(interactableObject);

            Debug.Log(other.name + " is in proximity.");
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.TryGetComponent<IInteractable>(out var interactable)) {
            var removedObj = m_InteractablesInProximity.First(obj => obj.interactable == interactable);
            m_InteractablesInProximity.Remove(removedObj);
            Debug.Log(other.name + " is no longer in proximity.");
        }
    }

    public InteractableObject GetClosestInteractableObject() {

        m_InteractablesInProximity.RemoveAll(obj => obj.transform == null);


        if (m_InteractablesInProximity.Count == 0) {
            Debug.Log("No interactables within reach.");
            return default;
        }


        InteractableObject closestInteractableObj = default;
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

    public bool IsInProximity(IInteractable interactable) {
        return m_InteractablesInProximity.Any(obj => obj.interactable == interactable);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        if (m_InteractionCollider == null) {
            m_InteractionCollider = GetComponent<SphereCollider>();
        }
        Gizmos.DrawWireSphere(transform.position + m_InteractionCollider.center, m_InteractionCollider.radius);
    }

    private void OnValidate() {
        if (m_InteractionCollider == null) {
            m_InteractionCollider = GetComponent<SphereCollider>();
        }
        m_InteractionCollider.radius = m_InteractionRadius;
    }
#endif
}
