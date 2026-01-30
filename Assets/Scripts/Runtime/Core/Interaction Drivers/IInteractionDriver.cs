using LuduArtsCase.Core;

public interface IInteractionDriver
{
    bool IsComplete { get; }

    void Start(IInteractable target);
    void Update(float deltaTime);
    void Stop();
}
