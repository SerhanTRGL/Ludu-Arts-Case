using LuduArtsCase.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

public class InteractionDetector : MonoBehaviour
{
    [SerializeField] private float m_InteractionRadius = 5f;
    [SerializeField] private SphereCollider m_InteractionCollider;
    private readonly List<InteractableObject> m_InteractablesInProximity = new();

    [SerializeField] private float m_ClosestObjectUpdateRate = 1f/20f;

    private InteractableObject m_ClosestInteractableObject;
    public InteractableObject ClosestInteractableObject => m_ClosestInteractableObject;

    public event Action<InteractableObject> OnClosestInteractableObjectChanged;
    private void Awake() {
        m_InteractionCollider = GetComponent<SphereCollider>();
        m_InteractionCollider.isTrigger = true;
        m_InteractionCollider.radius = m_InteractionRadius;
    }

    private void Start() {
        InvokeRepeating(nameof(GetClosestInteractableObject), 0, m_ClosestObjectUpdateRate);
    }

    private void OnTriggerEnter(Collider other) {
        
        if(other.gameObject.TryGetComponent<IInteractable>(out var interactable)) {
            var interactableObject = new InteractableObject(interactable, other.transform);
            m_InteractablesInProximity.Add(interactableObject);
            GetClosestInteractableObject();
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.TryGetComponent<IInteractable>(out var interactable)) {
            m_InteractablesInProximity.RemoveAll(obj => obj.interactable == interactable || obj.transform == null);
            GetClosestInteractableObject();
            Debug.Log(other.name + " is no longer in proximity.");
        }
    }

    private void GetClosestInteractableObject() {
        var previousClosestInteractableObject = m_ClosestInteractableObject;

        m_InteractablesInProximity.RemoveAll(obj => obj.transform == null);


        if (m_InteractablesInProximity.Count == 0) {
            Debug.Log("No interactables within reach.");
            m_ClosestInteractableObject = default;

            if(previousClosestInteractableObject.transform != m_ClosestInteractableObject.transform) {
                OnClosestInteractableObjectChanged?.Invoke(m_ClosestInteractableObject);
            }
            return;
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

        m_ClosestInteractableObject =  closestInteractableObj;
        if(previousClosestInteractableObject.transform != m_ClosestInteractableObject.transform) {
            OnClosestInteractableObjectChanged?.Invoke(m_ClosestInteractableObject);
        }
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
