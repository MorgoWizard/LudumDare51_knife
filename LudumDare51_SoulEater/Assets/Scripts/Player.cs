using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private int souls = 0;
    private int health = 10;
    private int maxHealth = 10;

    private float rotY = 0f;
    private float rotX = 0f;

    [SerializeField] private Transform camTransform;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float sensetivity = 5f;

    private bool isGrounded = true;

    private Rigidbody rb;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        camTransform = Camera.main.transform;
    }

    private void Update()
    {
        Rotate();
        Jump();
        //Attack();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Rotate()
    {
        rotX += Input.GetAxis("Mouse Y") * sensetivity;
        rotY += Input.GetAxis("Mouse X") * sensetivity;

        rotX = rotX < 75f ? rotX : 75f;
        rotX = rotX > -75f ? rotX : -75f;

        camTransform.rotation = Quaternion.Euler(-rotX, rotY, 0f);
    }

    private void Move()
    {
        if (!isGrounded)
            return;

        Vector3 forward = camTransform.forward;
        Vector3 right = camTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward = forward.magnitude < 1f ? forward * (1f / forward.magnitude) : forward;
        right = right.magnitude < 1f ? right * (1f / right.magnitude) : right;


        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(forward * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-forward * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(right * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-right * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }

    private void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
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
