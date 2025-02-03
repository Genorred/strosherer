using UnityEngine;

public class GatesInteraction : Interactions
{
    public InteractionsTextRenderer textRenderer;
    protected override void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
    }
}
