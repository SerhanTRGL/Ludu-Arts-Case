using LuduArtsCase.Core;
using System;

public class ToggleInteractionDriver : IInteractionDriver {

    private bool m_IsOn;

    private IInteractable m_Target;
    
    public bool IsOn => m_IsOn;
    public bool IsComplete => false;
    public event Action OnToggled;

    public void Start(IInteractable target) {
        m_Target = target;
        m_IsOn = !m_IsOn;
        if (m_IsOn) m_Target.Begin();
        else m_Target.End();
        OnToggled?.Invoke();
    }

    public void Stop() {}

    public void Update(float deltaTime) {}
}
