using LuduArtsCase.Core;
using UnityEngine;

public readonly struct InteractableObject {
    public readonly IInteractable interactable;
    public readonly Transform transform;

    public InteractableObject(IInteractable i, Transform t) {
        interactable = i;
        transform = t;
    }
}