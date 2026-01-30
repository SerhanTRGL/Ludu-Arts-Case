using LuduArtsCase.Core;
using System.Threading;
using UnityEngine;

public class HoldInteractionDriver : IInteractionDriver {
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
    }

    public void Update(float deltaTime) {
        m_Timer += deltaTime;
        m_Target.Tick(deltaTime);
    }

    public void Stop() {
        m_Target.End();
    }
}
