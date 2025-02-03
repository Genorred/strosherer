using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInteractions : MonoBehaviour
{
    public InteractionsTextRenderer textRenderer;
    public InputManager inputManager;

    [SerializeField] LayerMask mask;
    [SerializeField] float maxDistance;

    private Camera cam;
    private Action<InputAction.CallbackContext> interactionsHandler;
    private int interactionsHandlerId;
    private bool isCached = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }

    void FixedUpdate()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance, mask))
        {
            int id = hit.collider.gameObject.GetInstanceID();
            if (interactionsHandlerId != id)
            {
                interactionsHandler = context => hit.collider.GetComponent<GatesInteraction>().Call();
                textRenderer.DisplayText(hit.collider.transform.position);

                interactionsHandlerId = id;
                isCached = false;
            }

            if (!isCached)
            {
                inputManager.onFootActions.Interact.performed += interactionsHandler;
                isCached = true;
            }
        }
        else
        {
            inputManager.onFootActions.Interact.performed -= interactionsHandler;
            isCached = false;
        }
    }
}