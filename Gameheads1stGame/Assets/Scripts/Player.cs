using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed, jumpForce;

    bool grounded;

    Rigidbody rb;
    Animator animr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animr = GetComponent<Animator>();
        grounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        if (transform.position.y < -10) transform.position = Vector3.up * 1.5f;
        Vector3 movement = Vector3.zero;
        if (grounded)
        {
            if (Input.GetKey(KeyCode.W)) movement += Vector3.forward;
            if (Input.GetKey(KeyCode.S)) movement += -Vector3.forward;
            if (Input.GetKey(KeyCode.D)) movement += Vector3.right;
            if (Input.GetKey(KeyCode.A)) movement += -Vector3.right;
        }
        if (movement == Vector3.zero && grounded)
        {
            animr.CrossFade("Idle", 0, 0);
            return;
        }
        transform.forward = (transform.forward + movement.normalized).normalized;
        transform.Translate(0, 0, speed * Time.deltaTime);
        animr.CrossFade("Walking", 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;
        animr.CrossFade("Walking", 0, 0);
    }

    void Jump()
    {
        if (!grounded) return;
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        rb.AddForce(jumpForce * Vector3.up * 3, ForceMode.Impulse);
        grounded = false;
        animr.CrossFade("Jumping", 0, 0);
    }
}
