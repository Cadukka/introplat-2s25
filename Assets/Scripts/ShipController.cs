using UnityEngine;

public class ShipController : MonoBehaviour
{
    //References
    public Rigidbody2D rb;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    
    //Movement
    public float moveSpeed = 5;
    public float rotationSpeed = 200f;
    private float _moveInput = 0;
    private float _rotationInput = 0;
    private float _currentSpeed;
    
    //Shooting
    public float fireCooldown = 1f;
    private float _cooldownTimer;
    
    //Power-Ups
    public bool isInvincible = false;
    public float invincibilityDuration = 5f;
    private float _invincibilityTimer;
    
    void Update()
    {
        _moveInput = Input.GetAxis("Vertical");
        _rotationInput = Input.GetAxis("Horizontal");
        
        //Check shot cooldown
        if(_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;
        }

        if (isInvincible)
        {
            _invincibilityTimer -= Time.deltaTime;
            if (_invincibilityTimer <= 0)
            {
                isInvincible = false;
                GetComponent<SpriteRenderer>().color = Color.cyan;
            }
        }
        
        //Shoot
        if (Input.GetKeyDown(KeyCode.Space) && _cooldownTimer <= 0)
        {
            _cooldownTimer = fireCooldown; // Reset cooldown
            Instantiate(bulletPrefab, bulletSpawn.position, transform.rotation);
        }

        //Double speed when Left Shift is held
        _currentSpeed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = moveSpeed * 2;
        }
    }
    
    void FixedUpdate()
    {
        // Move the ship forward/backward
        Vector2 moveDirection = transform.up * _moveInput * _currentSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveDirection);

        // Rotate the ship
        float rotation = -_rotationInput * rotationSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotation);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.CompareTag("Asteroid"))
        // {
        //     Debug.Log("Collided with Asteroid!");
        //     Destroy(other.gameObject); //Destroy the player ship
        //     
        //     if(!isInvincible)
        //     {
        //         Destroy(gameObject); //Destroy the asteroid
        //     }
        // }
        
        if (other.CompareTag("PowerUp"))
        {
            Debug.Log("Collected a Power-Up!");
            Destroy(other.gameObject);
            isInvincible = true;
            _invincibilityTimer = invincibilityDuration;
            
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }
}
