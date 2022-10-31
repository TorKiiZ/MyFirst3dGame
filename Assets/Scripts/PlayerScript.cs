using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody rigidbody;
    [SerializeField] float movementSpeed = 0.5f;
    [SerializeField] float jumpForce = 5f;

    [SerializeField]Transform groundCheck;
    [SerializeField]LayerMask ground;

    [SerializeField] AudioSource jumpSound;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

rigidbody.velocity = new Vector3(horizontInput * movementSpeed, rigidbody.velocity.y, verticalInput * movementSpeed);

                if (Input.GetButtonDown("Jump") && IsGrounded())
                {
            Jump();
        }
    }
    void Jump()
    {
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpForce, rigidbody.velocity.z);
        jumpSound.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyHead"))
        {
            Destroy(collision.transform.parent.gameObject);
            Jump();
        }
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, .1f, ground);
    }
}