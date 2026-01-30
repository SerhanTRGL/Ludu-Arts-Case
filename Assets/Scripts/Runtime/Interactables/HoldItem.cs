using LuduArtsCase.Core;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class HoldItem : MonoBehaviour, IInteractable {
    [SerializeField] private float m_HoldDuration;
    [SerializeField] private float m_ElapsedTime;

    private Vector3 m_StartingScale;
    [SerializeField] private Vector3 m_TargetScale;

    private IInteractionDriver m_Driver;

    public IInteractionDriver CreateDriver() {
        m_Driver = new HoldInteractionDriver(m_HoldDuration);
        return m_Driver;
    }
       
    public void Begin() {
        Reset();
    }

    public void Tick(float deltaTime) {
        m_ElapsedTime += deltaTime;
        IncreaseSize();        
    }

    public void End() {
        if (m_Driver.IsComplete) {
            Destroy(gameObject);
        }
        else {
            Reset();
        }
    }

    private void Reset() {
        transform.localScale = Vector3.one;
        m_StartingScale = transform.localScale;
        m_ElapsedTime = 0f;
    }

    private void IncreaseSize() {
        transform.localScale = Vector3.Lerp(m_StartingScale, m_TargetScale, m_ElapsedTime/m_HoldDuration);
    }
}
