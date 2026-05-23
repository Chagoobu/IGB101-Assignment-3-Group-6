using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour
{
    public Animator anim;
    public Rigidbody rb;
    public float jumpForce = 5.0f;
    public bool isGrounded = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && isGrounded)
        {
            // Use ForceMode.Impulse for an immediate burst of speed
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetBool("Jumping", true);
        }
        else if (Input.GetKeyUp("space"))
        {
            anim.SetBool("Jumping", false);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}