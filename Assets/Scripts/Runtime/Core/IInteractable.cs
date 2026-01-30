namespace LuduArtsCase.Core {
    public interface IInteractable{
        void OnInteractionStart();
        void OnInteractionUpdate(float deltaTime);
        void OnInteractionEnd();
    }
}

