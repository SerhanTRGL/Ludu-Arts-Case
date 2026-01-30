using UnityEngine;

public class InteractionUIManager : MonoBehaviour {
    [SerializeField] private InteractionDetector m_Detector;
    public IInteractionUIAdapter[] adapters;
    IInteractionUIAdapter active;
   

    private void Start() {
        if(m_Detector == null) {
            Debug.LogWarning("Missing InteractionDetector reference.");
            return;
        }

        m_Detector.OnClosestInteractableObjectChanged += UpdateBinding;
    }

    private void UpdateBinding(InteractableObject closestInteractableObject) {
        var driver = closestInteractableObject.interactable.GetOrCreateDriver();

        BindTo(driver);
    }

    public void BindTo(IInteractionDriver driver) {
        active?.Unbind();

        foreach(var a in adapters) {
            if (a.CanRender(driver)) {
                active = a;
                active.Bind(driver);
                return;
            }
        }

        active = null;
    }

}