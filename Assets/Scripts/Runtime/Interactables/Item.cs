using LuduArtsCase.Core;
using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour, IInteractable {
    public static Action<ItemSO> OnPickedUpItem;


    [SerializeField] private ItemSO m_ItemSO;
    [SerializeField] private GameObject m_ItemObject;
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
        m_ItemObject = Instantiate(m_ItemSO.ItemPrefab, transform);
        m_ItemObject.transform.localPosition = Vector3.zero;
    }
}
