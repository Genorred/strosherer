using UnityEngine;
using UnityEngine.Serialization;

public class tracePlayer : MonoBehaviour
{
    private Rigidbody rb;
    [FormerlySerializedAs("player")] public Rigidbody playerRb;
    public PlayerMotor playerMotor;
    public float speed = 5f;
    public float maxDistance = 0.45f;
    public float rotationSpeed = 5f;
    private bool waitShoving;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if (rb.linearVelocity.magnitude <= speed)
        // {
        //     Vector3 direction = playerRb.position - rb.position;
        //     direction.y = 0;
        //     Vector3 moveDirection = Vector3.MoveTowards(rb.position, playerRb.position, speed * Time.fixedDeltaTime);
        //
        //     if (direction.magnitude > maxDistance)
        //     {
        //         rb.MovePosition(moveDirection);
        //     }
        //
        //     Quaternion targetRotation = Quaternion.LookRotation(direction);
        //     rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        // }
        // if (rb.linearVelocity.magnitude <= speed)
        // {
        if (playerMotor.isDashing)
        {
            Vector3 direction = (playerRb.position - rb.position);
            direction.y = 0; 
            Vector3 movingDirection = direction.normalized;
            float distance = direction.magnitude;
        
            if (distance > maxDistance)
            {
                rb.linearVelocity = movingDirection * speed;
            }
        
            if (rb.linearVelocity.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }
        // }
    }
}