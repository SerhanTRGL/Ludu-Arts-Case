using LuduArtsCase.Core;
using UnityEngine;

public class ToggleItem : MonoBehaviour, IInteractable {

    private IInteractionDriver m_Driver;
    public IInteractionDriver GetOrCreateDriver() {
        m_Driver ??= new ToggleInteractionDriver();

        return m_Driver;
    }

    public void Begin() {
        transform.localScale = Vector3.one * 1.5f;
    }


    public void End() {
        transform.localScale = Vector3.one * 0.5f;
    }

    public void Tick(float deltaTime) {}

    private void Start() {
        //Start from IsOn=false;
        End();
    }
}
