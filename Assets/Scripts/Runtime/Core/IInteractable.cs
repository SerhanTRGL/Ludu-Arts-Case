namespace LuduArtsCase.Core {
    public interface IInteractable{
        IInteractionDriver GetOrCreateDriver();

        void Begin();
        void Tick(float deltaTime);
        void End();
    }
}

