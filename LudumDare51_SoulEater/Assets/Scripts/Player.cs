using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]private int souls = 0;
    private int health = 10;
    private int maxHealth = 10;

    private float rotY = 0f;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float sensetivity = 5f;

    private bool isGrounded = true;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Rotate();
        //Move();
        Jump();
        //Attack();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Rotate()
    {
        rotY += Input.GetAxis("Mouse X");
        transform.rotation = Quaternion.Euler(0f, rotY, 0f);
    }

    private void Move()
    {
        if (!isGrounded)
            return;

        //rb.velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.right * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-transform.right * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }

    private void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }

    private void Attack()
    {

    }

    public void AddSoul()
    {
        souls++;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
