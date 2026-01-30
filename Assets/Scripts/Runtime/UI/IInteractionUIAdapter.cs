public interface IInteractionUIAdapter {
    bool CanRender(IInteractionDriver driver);
    void Bind(IInteractionDriver driver);
    void Unbind();
}
