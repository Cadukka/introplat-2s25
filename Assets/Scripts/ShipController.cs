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
    
    //Shooting
    public float fireCooldown = 1f;
    private float _cooldownTimer;

    //DoubleSpeed
    float doubleSpeed;
    float initialSpeed;

    //ForceField
    [SerializeField] GameObject forceField;
    [SerializeField] float timerFieldInitial;
    float timerField;
    private void Start()
    {
        doubleSpeed = moveSpeed * 2; //Define Double Speed as the souble of initial speed
        initialSpeed = moveSpeed; //Define initial speed, to get it back later
        forceField.SetActive(false); //Shield is deactivated
        timerField = timerFieldInitial;
    }
    void Update()
    {
        _moveInput = Input.GetAxis("Vertical");
        _rotationInput = Input.GetAxis("Horizontal");
        
        //Check shot cooldown
        if(_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;
        }
        
        //Shoot
        if (Input.GetKeyDown(KeyCode.Space) && _cooldownTimer <= 0)
        {
            _cooldownTimer = fireCooldown; // Reset cooldown
            Instantiate(bulletPrefab, bulletSpawn.position, transform.rotation);
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) )
        {
            moveSpeed = doubleSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            moveSpeed = initialSpeed;
        }

        // Timer for ForceField
        timerField -= Time.deltaTime;
        if(timerField <= 0)
        {
            forceField.SetActive(false);
        }
    }
    
    void FixedUpdate()
    {
        // Move the ship forward/backward
        Vector2 moveDirection = transform.up * _moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveDirection);

        // Rotate the ship
        float rotation = -_rotationInput * rotationSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotation);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check for collision with asteroids
        if (other.CompareTag("Asteroid"))
        {
            Debug.Log("Collided with an asteroid!");
            Destroy(other.gameObject);

            if(forceField.gameObject.activeInHierarchy)
            {
                forceField.SetActive(false);
                return;
            }
            Destroy(gameObject);
        }


        if (other.CompareTag("Invincible"))
        {
            timerField = timerFieldInitial;
            forceField.SetActive(true);
        }
    }
}
