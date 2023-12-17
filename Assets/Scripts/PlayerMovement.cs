using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform head;
    public Rigidbody rb;
    public float lookSpeed = 5f;
    public float moveSpeed = 2.5f;
    public float sprintSpeed = 5f;

    private float rotationX = 0f;

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

        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(new Ray(transform.position, Vector3.down), 1.1f, 1 << 0))
            rb.velocity += Vector3.up * 5;

        //rb.MovePosition(rb.position + movement * speed * Time.deltaTime); // very bad
    }
}
