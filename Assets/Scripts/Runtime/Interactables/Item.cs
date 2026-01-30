using LuduArtsCase.Core;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable {
    public IInteractionDriver CreateDriver() => new InstantInteractionDriver();
    public void Begin() {
        Debug.Log("Picked up a " + name);
    }

    public void End() {
        transform.position = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        Destroy(gameObject);
    }

    public void Tick(float deltaTime) {}
}
