using System;
using TMPro;
using UnityEngine;

public class InstantInteractionUIAdapter : MonoBehaviour, IInteractionUIAdapter
{

    public TMP_Text text;

    InstantInteractionDriver m_Driver;
    public bool CanRender(IInteractionDriver driver) => driver is InstantInteractionDriver;
    public void Bind(IInteractionDriver driver) {
        m_Driver = (InstantInteractionDriver)driver;

        Reset();
        gameObject.SetActive(true);
    }

    private void Reset() {
        text.text = "Press to interact";
    }

    public void Unbind() {
        gameObject.SetActive(false);

        if (m_Driver == null) return;
        m_Driver = null;
    }

}
