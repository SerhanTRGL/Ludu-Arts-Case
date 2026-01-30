using LuduArtsCase.Core;
using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour, IInteractable {
    public static Action<ItemSO> OnPickedUpItem;
    [SerializeField] private ItemSO m_ItemSO;
    public IInteractionDriver GetOrCreateDriver() => new InstantInteractionDriver();
    public void Begin() {
        OnPickedUpItem?.Invoke(m_ItemSO);
        Debug.Log("Picked up a " + name);
    }

    public void End() {
        transform.position = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        Destroy(gameObject);
    }

    public void Tick(float deltaTime) {}


    private void Start() {
        Instantiate(m_ItemSO.ItemPrefab, Vector3.zero, Quaternion.identity, transform);
    }
}
