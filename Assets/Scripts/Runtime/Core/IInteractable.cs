namespace LuduArtsCase.Core {
    public interface IInteractable{
        IInteractionDriver CreateDriver();

        void Begin();
        void Tick(float deltaTime);
        void End();
    }
}

