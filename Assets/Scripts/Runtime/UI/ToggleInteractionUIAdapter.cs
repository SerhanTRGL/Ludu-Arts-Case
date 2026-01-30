using System;
using TMPro;
using UnityEngine;

public class ToggleInteractionUIAdapter : MonoBehaviour, IInteractionUIAdapter {

    ToggleInteractionDriver m_Driver;
    public TMP_Text text;
    public bool CanRender(IInteractionDriver driver) => driver is ToggleInteractionDriver;
    public void Bind(IInteractionDriver driver) {
        m_Driver = (ToggleInteractionDriver)driver;

        
        m_Driver.OnToggled += Reset;
        Reset();
        gameObject.SetActive(true);
    }

    private void Reset() {
        text.text = m_Driver.IsOn ? "Press to toggle off" : "Press to toggle on";  
    }

    public void Unbind() {
        gameObject.SetActive(false);
        if (m_Driver == null) return;

        m_Driver.OnToggled -= Reset;
        m_Driver = null;
    }
}
