using UnityEngine;

public class GatesInteraction : Interactions
{
    public InteractionsTextRenderer textRenderer;
    public Animator animator;
    
    private bool isOpen;

    protected override void Interact()
    {
        isOpen = !isOpen;
        animator.SetBool("isOpened", isOpen);
    }
}
