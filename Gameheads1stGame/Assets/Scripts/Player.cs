using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed, jumpForce, rotationSpeed;

    float yRotate;

    Rigidbody rb;
    Animator animr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animr = GetComponent<Animator>();
        yRotate = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        RotateView();

        Vector3 movement = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) movement += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) movement += -Vector3.forward;
        if (Input.GetKey(KeyCode.D)) movement += Vector3.right;
        if (Input.GetKey(KeyCode.A)) movement += -Vector3.right;

        animr.CrossFade(movement == Vector3.zero ? "Idle" : "Walk", 0, 0);
        transform.Translate(movement * speed * Time.deltaTime, Space.Self);
    }

    bool IsGrounded()
    {
        int layerMask = LayerMask.GetMask("Ground");
        Vector3 offset = new Vector3(0, -1, 0);
        return Physics.Raycast(transform.position + offset, Vector3.down, 0.6f, layerMask);
    }

    void Jump()
    {
        if (!IsGrounded()) return;
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        rb.AddForce(jumpForce * Vector3.up * 3, ForceMode.Impulse);
        animr.CrossFade("Jumping", 0, 0);
    }

    void RotateView()
    {
        if (Input.GetKey(KeyCode.LeftAlt)) return;
        yRotate += Input.GetAxis("Mouse X") * rotationSpeed;
        transform.eulerAngles = new Vector3(0, yRotate, 0);
    }

    private void OnTriggerEnter(Collider col)
    {
        transform.position = Vector3.up * 1.5f;
        rb.velocity = Vector3.zero;
    }
}
