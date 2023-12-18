using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform head;
    public Rigidbody rb;
    public float lookSpeed = 5f;
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f;

    private float rotationX = 0f;

    [SerializeField] private float JumpForce = 2f;
    [SerializeField] private float ContinuousJumpForce = 0.8f;
    [SerializeField] private float FallForce = 0.2f;
    [SerializeField] private float JumpTime = 0.20f;


    private bool jumpPressed = false;
    private bool jumpHeld = false;
    private bool jumpReleased = false;
    private bool isJumping = false;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Look();
        Move();
    }

    private void FixedUpdate()
    {
        if (jumpPressed) OnJumpPressed();
        if (jumpHeld && isJumping) ContinuousJump();
        if (rb.velocity.y < 0f) ApplyFallForce();
        if (jumpReleased) OnJumpReleased();
    }

    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        transform.Rotate(Vector3.up * mouseX);

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -30f, 30f);
        head.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        Vector3 movement = (transform.right * horizontal + transform.forward * vertical).normalized;
        rb.velocity = new Vector3((movement * speed).x, rb.velocity.y, (movement * speed).z);

        if (Input.GetKeyDown(KeyCode.Space)) 
        { 
            jumpPressed = true;
            jumpHeld = true; 
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpPressed = false;
            jumpHeld = false;
            jumpReleased = true;
        }


        //rb.MovePosition(rb.position + movement * speed * Time.deltaTime); // very bad
    }

    private void OnJumpPressed()
    {
        jumpPressed = false;
        if (Physics.Raycast(new Ray(transform.position, Vector3.down), 1.1f, 1 << 0)) 
        {
            isJumping = true;
            rb.velocity += Vector3.up * JumpForce;
            IEnumerator stopJumpRoutine = StopContinuousJump(JumpTime);
            StartCoroutine(stopJumpRoutine);
        }
    }

    private void ContinuousJump() 
    {
        Debug.Log("Applying continuous jump force");
        rb.velocity += Vector3.up * ContinuousJumpForce;
    }

    private void ApplyFallForce() 
    {
        rb.velocity += Vector3.down * FallForce;
    }

    private void OnJumpReleased() 
    {
        jumpReleased = false;
        if (rb.velocity.y > 0) rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
    }

    IEnumerator StopContinuousJump(float jumpTime) 
    {
        Debug.Log("Stopping continuous jump force routine started");
        yield return new WaitForSeconds(jumpTime);
        Debug.Log("Stopping continuous jump force");
        isJumping = false;
    }
}
