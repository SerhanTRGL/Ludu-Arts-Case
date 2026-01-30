using LuduArtsCase.Core;
using System;

public class HoldInteractionDriver : IInteractionDriver {

    public event Action OnHoldInteractionStart;
    public event Action<float, float> OnHoldInteractionProgress;
    public event Action OnHoldInteractionEnd;
    readonly float m_Duration;
    float m_Timer;
    IInteractable m_Target;

    public bool IsComplete => m_Timer >= m_Duration;

    public HoldInteractionDriver(float duration) {
        m_Duration = duration;
    }

    public void Start(IInteractable target) {
        m_Target = target;
        m_Timer = 0f;
        target.Begin();
        OnHoldInteractionStart?.Invoke();
    }

    public void Update(float deltaTime) {
        m_Timer += deltaTime;
        m_Target.Tick(deltaTime);
        OnHoldInteractionProgress?.Invoke(m_Timer, m_Duration);
    }

    public void Stop() {
        m_Target.End();
        OnHoldInteractionEnd?.Invoke();
    }
}
