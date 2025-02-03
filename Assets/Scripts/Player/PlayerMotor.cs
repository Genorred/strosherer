using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerMotor : MonoBehaviour
{
    [Header("References")]
    public Transform playerCam;
    private Rigidbody rb;
    private AudioSource audioSource;
    public AudioClip dashClip;
    public AudioClip initialSound;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.15f;
    [SerializeField] private float dashUpwardForce = 2f;

    private bool isGrounded;
    private bool canSecondJump = true;
    public bool isDashing;
    private Vector3 lastDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        rb.freezeRotation = true; // Prevents unwanted rotation
        lastDirection = transform.forward;
        
        audioSource.clip = initialSound;
        audioSource.Play();
    }

    private void FixedUpdate()
    {
        // Raycast to check if the player is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);
        
        if (isGrounded)
        {
            canSecondJump = true;
        }
    }

    public void ProcessMove(Vector2 input)
    {
        if (isDashing) return;

        Vector3 moveDirection = new Vector3(input.x, 0f, input.y);
        moveDirection = transform.TransformDirection(moveDirection); // Convert local to world direction

        if (input.x != 0 || input.y != 0)
        {
            lastDirection = moveDirection;
        }

        // Apply movement
        Vector3 moveVelocity = moveDirection * speed;
        rb.linearVelocity = new Vector3(moveVelocity.x, rb.linearVelocity.y, moveVelocity.z);
    }

    public void JumpHandler()
    {
        if (isGrounded)
        {
            Jump(jumpForce);
            canSecondJump = true;
        }
        else if (canSecondJump)
        {
            Jump(jumpForce * 2f);
            canSecondJump = false;
        }
    }

    private void Jump(float force)
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); // Reset Y velocity to ensure consistent jumps
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
    }

    public void Dash()
    {
        if (isDashing) return;

        audioSource.clip = dashClip;
        audioSource.Play();
        
        IEnumerator Proceed()
        {
            isDashing = true;

            Vector3 dashDirection = lastDirection.normalized;
            rb.linearVelocity = Vector3.zero; // Reset velocity before dashing
            rb.AddForce((dashDirection * dashSpeed) + (Vector3.up * dashUpwardForce), ForceMode.VelocityChange);
            
            yield return new WaitForSeconds(dashDuration);
            isDashing = false;
        }

        StartCoroutine(Proceed());
    }
}