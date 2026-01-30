using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoldInteractionUIAdapter : MonoBehaviour, IInteractionUIAdapter {

    public GameObject progressRoot;
    public TMP_Text text;
    public Image bar; 

    HoldInteractionDriver m_Driver;


    public bool CanRender(IInteractionDriver driver) => driver is HoldInteractionDriver;

    public void Bind(IInteractionDriver driver) {
        m_Driver = (HoldInteractionDriver)driver;

        m_Driver.OnHoldInteractionStart += OnStart;
        m_Driver.OnHoldInteractionProgress += OnProgress;
        m_Driver.OnHoldInteractionEnd += OnEnd;

        Reset();
        gameObject.SetActive(true);
    }

    public void Unbind() {
        gameObject.SetActive(false);

        if (m_Driver == null) return;
        m_Driver.OnHoldInteractionStart -= OnStart;
        m_Driver.OnHoldInteractionProgress -= OnProgress;
        m_Driver.OnHoldInteractionEnd -= OnEnd;

        m_Driver = null;
    }

    private void OnStart() {
        Reset();
    }

    private void Reset() {
        text.text = "Hold to interact";
        gameObject.SetActive(true);
        bar.fillAmount = 0f;
    }

    private void OnProgress(float timer, float duration) {
        bar.fillAmount = timer / duration;
        text.text = $"{Mathf.Ceil(duration - timer)}s remaining";
    }
    private void OnEnd() {
        if (m_Driver.IsComplete) {
            gameObject.SetActive(false);
        }
        else {
            OnStart();
        }
    }
}
