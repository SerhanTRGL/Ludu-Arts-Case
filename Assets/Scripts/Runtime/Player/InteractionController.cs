using LuduArtsCase.Core;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(InteractionDetector))]
public class InteractionController : MonoBehaviour
{
    private InteractionDetector m_Detector;

    private IInteractionDriver m_CurrentDriver;
    private IInteractable m_CurrentTarget;

    [SerializeField] private InputActionReference m_InteractionActionReference;
    private InputAction m_InteractionAction;

    public InteractableObject CurrentInteractionObject;
    private void Awake() {
        m_Detector = GetComponent<InteractionDetector>();

        m_InteractionAction = m_InteractionActionReference.action;
        m_InteractionAction.Enable();
        m_InteractionAction.performed += TryStartInteraction;
    }

    private void Update() {
        Tick(Time.deltaTime);
        if (m_InteractionAction.WasReleasedThisDynamicUpdate()) {
            if(m_CurrentDriver != null) {
                m_CurrentDriver.Stop();
                Clear();
            }
        }
    }
    public void Tick(float deltaTime) {
        if (m_CurrentDriver == null) return;

        m_CurrentDriver.Update(deltaTime);

        if(m_CurrentDriver.IsComplete || !m_Detector.IsInProximity(m_CurrentTarget)) {
            m_CurrentDriver.Stop();
            Clear();
        }
    }

    private void TryStartInteraction(InputAction.CallbackContext _) {
        InteractableObject closestInteractableObject = m_Detector.GetClosestInteractableObject();

        if (closestInteractableObject.transform == null) return;

        TryStartInteraction(closestInteractableObject.interactable);
        
        CurrentInteractionObject = closestInteractableObject;
    }

    public void TryStartInteraction(IInteractable target) {
        if (m_CurrentDriver != null) return;

        m_CurrentTarget = target;
        m_CurrentDriver = target.GetOrCreateDriver();
        m_CurrentDriver.Start(m_CurrentTarget);
    }


    private void Clear() {
        m_CurrentDriver = null;
        m_CurrentTarget = null;
    }

}
