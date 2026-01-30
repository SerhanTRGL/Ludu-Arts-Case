using UnityEngine;

public class InteractionUIManager : MonoBehaviour {
    public Canvas interactionsUICanvas;
    [SerializeField] private InteractionDetector m_Detector;
    public IInteractionUIAdapter[] adapters;
    IInteractionUIAdapter active;
   
    private void Start() {
        interactionsUICanvas = GetComponentInParent<Canvas>();
        if(m_Detector == null) {
            Debug.LogWarning("Missing InteractionDetector reference.");
            return;
        }

        adapters = new IInteractionUIAdapter[transform.childCount];

        for(int i = 0; i < transform.childCount; i++) {
            var child = transform.GetChild(i);
            child.gameObject.SetActive(false);
            var adapter = child.GetComponent<IInteractionUIAdapter>();
            adapters[i] = adapter;
        }

        UpdateBinding(m_Detector.ClosestInteractableObject);

        m_Detector.OnClosestInteractableObjectChanged += UpdateBinding;
    }
    private void OnDestroy() {
        m_Detector.OnClosestInteractableObjectChanged -= UpdateBinding;
    }

    private void UpdateBinding(InteractableObject closestInteractableObject) {
        BindTo(closestInteractableObject);
    }

    public void BindTo(InteractableObject interactableObject) {
        active?.Unbind();

        if (interactableObject.interactable == null) {
            active = null;
            return;
        }
        
        var driver = interactableObject.interactable.GetOrCreateDriver();

        foreach (var a in adapters) {
            if (a.CanRender(driver)) {
                active = a;
                active.Bind(driver);
                interactionsUICanvas.transform.position = new Vector3(interactableObject.transform.position.x, interactableObject.transform.position.y + 2, interactableObject.transform.position.z);
                return;
            }
        }

        active = null;
    }

}