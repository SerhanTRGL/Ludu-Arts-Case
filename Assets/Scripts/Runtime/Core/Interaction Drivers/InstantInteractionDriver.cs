using LuduArtsCase.Core;
using System;
using UnityEngine;

public class InstantInteractionDriver : IInteractionDriver {

    public bool IsComplete => true;

    public void Start(IInteractable target) {
        target.Begin();
        target.End();
    }

    public void Stop() {}

    public void Update(float deltaTime) {}
}
