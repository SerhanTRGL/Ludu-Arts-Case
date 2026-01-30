using LuduArtsCase.Core;

public class ToggleInteractionDriver : IInteractionDriver {

    private bool m_IsOn;
    private IInteractable m_Target;
    
    public bool IsComplete => false;

    public void Start(IInteractable target) {
        m_Target = target;
        m_IsOn = !m_IsOn;
        if (m_IsOn) m_Target.Begin();
        else m_Target.End();
    }

    public void Stop() {}

    public void Update(float deltaTime) {}
}
