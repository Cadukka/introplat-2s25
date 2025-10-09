using UnityEngine;

public enum ShotType
{
    Single,
    Double,
    Triple
}

public class ShipController : MonoBehaviour
{
    public ShotType shotType;
    
    public float moveSpeed = 5;
    public float rotationSpeed = 200f;
    public Rigidbody2D rb;

    private float moveInput = 0;
    private float rotationInput = 0;
    
    public GameObject singleShotBulletPrefab;
    public GameObject doubleShotBulletPrefab;
    public Transform bulletSpawn;
    
    public float shotOffSet = 0.5f;
    
    public float fireCooldown = 1f;
    private float _cooldownTimer;
    
    
    void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        rotationInput = Input.GetAxis("Horizontal");
        
        //Check shot cooldown
        if(_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && _cooldownTimer <= 0)
        {
            _cooldownTimer = fireCooldown; // Reset cooldown
            
            if(shotType == ShotType.Double)
            {
                Instantiate(singleShotBulletPrefab, bulletSpawn.position + transform.right * shotOffSet, transform.rotation);
                Instantiate(singleShotBulletPrefab, bulletSpawn.position + transform.right * -shotOffSet, transform.rotation);
            }
            else if(shotType == ShotType.Triple)
            {
                Instantiate(doubleShotBulletPrefab, bulletSpawn.position, transform.rotation);
                Instantiate(doubleShotBulletPrefab, bulletSpawn.position + transform.right * +shotOffSet, transform.rotation);
                Instantiate(doubleShotBulletPrefab, bulletSpawn.position + transform.right * -shotOffSet, transform.rotation);
            }
            else
            {
                Instantiate(singleShotBulletPrefab, bulletSpawn.position, transform.rotation);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            shotType = ShotType.Single;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            shotType = ShotType.Double;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            shotType = ShotType.Triple;
        }
    }
    
    void FixedUpdate()
    {
        // Move the ship forward/backward
        Vector2 moveDirection = transform.up * moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveDirection);

        // Rotate the ship
        float rotation = -rotationInput * rotationSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotation);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.CompareTag("Asteroid"))
        // {
        //     Debug.Log("Collided with an asteroid!");
        //     Destroy(other.gameObject);
        //     Destroy(gameObject);
        // }
    }
}
