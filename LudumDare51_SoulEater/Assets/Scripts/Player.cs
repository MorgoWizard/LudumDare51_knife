using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private int souls;
    
    [SerializeField] private int maxHealth = 10;
    private float _currentHealth;

    private float _rotY, _rotX;

    [SerializeField] private TextMeshProUGUI soulOutput;

    [SerializeField] private Transform camTransform;
    [SerializeField] public HealthBar healthBar;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float sensetivity = 5f;
    [SerializeField] private GameObject deathScreen;

    private bool _isGrounded = true;

    [SerializeField] private float regenTime = 3f;
    private bool canRegen = true;

    private Rigidbody _rb;

    private void Start()
    {
        _currentHealth = (float)maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        Cursor.lockState = CursorLockMode.Locked;
        _rb = GetComponent<Rigidbody>();
        if (Camera.main != null) camTransform = Camera.main.transform;
    }

    private void Update()
    {
        Rotate();
        Jump();
        if (canRegen && _currentHealth < maxHealth)
        {
            _currentHealth += Time.deltaTime;
        }
        healthBar.SetHealth(_currentHealth);
        //Attack();
    }

    private void FixedUpdate()
    {
        Move();
        soulOutput.text = souls + "";
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
        souls += Random.Range(1,7);
    }

    public int GetSouls()
    {
        return souls;
    }

    public void ResetSouls()
    {
        souls = 0;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if (canRegen == false)
            StopCoroutine(RegenerationCooldown());
        canRegen = false;
        StartCoroutine(RegenerationCooldown());

        if (_currentHealth <= 0)
        {
            deathScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private IEnumerator RegenerationCooldown()
    {
        yield return new WaitForSeconds(regenTime);
        canRegen = true;
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
