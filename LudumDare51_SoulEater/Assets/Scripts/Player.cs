using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private int souls;
    
    [SerializeField] private int maxHealth = 10;
    private int _currentHealth;

    private float _rotY, _rotX;

    [SerializeField] private Transform camTransform;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float sensetivity = 5f;

    private bool _isGrounded = true;

    private Rigidbody _rb;

    private void Start()
    {
        _currentHealth = maxHealth;
        Cursor.lockState = CursorLockMode.Locked;
        _rb = GetComponent<Rigidbody>();
        if (Camera.main != null) camTransform = Camera.main.transform;
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
        _rotX += Input.GetAxis("Mouse Y") * sensetivity;
        _rotY += Input.GetAxis("Mouse X") * sensetivity;

        _rotX = _rotX < 75f ? _rotX : 75f;
        _rotX = _rotX > -75f ? _rotX : -75f;

        camTransform.rotation = Quaternion.Euler(-_rotX, _rotY, 0f);
    }

    private void Move()
    {
        if (!_isGrounded)
            return;

        Vector3 forward = camTransform.forward;
        Vector3 right = camTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward = forward.magnitude < 1f ? forward * (1f / forward.magnitude) : forward;
        right = right.magnitude < 1f ? right * (1f / right.magnitude) : right;


        if (Input.GetKey(KeyCode.W))
        {
            _rb.AddForce(forward * (speed * Time.fixedDeltaTime), ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _rb.AddForce(-forward * (speed * Time.fixedDeltaTime), ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _rb.AddForce(right * (speed * Time.fixedDeltaTime), ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.A))
        {
            _rb.AddForce(-right * (speed * Time.fixedDeltaTime), ForceMode.VelocityChange);
        }
    }

    private void Jump()
    {
        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }

    public void AddSoul()
    {
        souls++;
    }

    public int GetSouls()
    {
        return souls;
    }

    public void ResetSouls()
    {
        souls = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}
