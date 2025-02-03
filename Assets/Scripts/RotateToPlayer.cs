using UnityEngine;

public class RotateToPlayer : MonoBehaviour
{
    private Transform transform;
    private Camera cam;
    public float rotationSpeed = 5f;

    [SerializeField] float y;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform = GetComponent<Transform>();
        cam = Camera.main;
    }

    void Update()
    {
        transform.LookAt(
            transform.position + cam.transform.rotation * Vector3.forward,
            cam.transform.rotation * Vector3.up);
    }
}