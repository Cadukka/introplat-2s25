using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidController : MonoBehaviour
{
    [SerializeField] private GameObject smallAsteroidPrefab;
    [SerializeField] private float health = 2f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] public int life = 2;
    [SerializeField] private Rigidbody2D rb;
    public Vector2 moveDirection;
    private float _rotationSpeed;

    [SerializeField] GameObject smallAsteroid;
    
    [SerializeField] private bool isSmall = false;
    
    void Start()
    {
        if (!isSmall)
        {
            moveDirection = (Random.insideUnitCircle * 5f).normalized;
        }
        //_rotationSpeed = Random.Range(-100f, 100f);
    }
    
    public void Initialize(Vector2 direction)
    {
        moveDirection = direction;
    }

    void FixedUpdate()
    {
        rb.angularVelocity = _rotationSpeed;
        rb.velocity = moveDirection * moveSpeed;
        
    }
    
    public void AsteroidDamage()
    {
        life--;
        if(life <= 0)
        {
            AsteroidDestroyed();
        }
    }
    public void AsteroidDestroyed()
    {
        Vector3 smallAsteroidOne= new Vector3(moveDirection.x * 45, 0, 0);
        Vector3 smallAsteroidTwo = new Vector3(moveDirection.x * -45, 0, 0);
        //Vector2.Perpendicular

        var small1 = Instantiate(smallAsteroid, transform.position, Quaternion.identity).GetComponent<SmallAsteroidController>();
        small1.Initialize(this);
        var small2 = Instantiate(smallAsteroid, transform.position, Quaternion.identity).GetComponent<SmallAsteroidController>();
        small2.Initialize(this);

        Destroy(this.gameObject);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Collided with a bullet!");
            Destroy(other.gameObject);
            TakeDamage(1);
        }
    }

    private void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Destroy(gameObject);
            if(smallAsteroidPrefab != null)
            {
                SpawnSmallerAsteroids();
            }
        }
    }

    private void SpawnSmallerAsteroids()
    {
        Vector2 currentVelocity = rb.velocity;
        
        //Vector2 perpendicularDirection = new Vector2(-currentVelocity.y, currentVelocity.x).normalized;
        Vector2 perpendicularDirection = Vector2.Perpendicular(currentVelocity).normalized;
        
        Vector2 direction1 = (currentVelocity.normalized + perpendicularDirection).normalized;
        Vector2 direction2 = (currentVelocity.normalized - perpendicularDirection).normalized;

        var small1 = Instantiate(smallAsteroidPrefab, transform.position, Quaternion.identity);
        var small2 = Instantiate(smallAsteroidPrefab, transform.position, Quaternion.identity);
        
        small1.GetComponent<AsteroidController>().Initialize(direction1);
        small2.GetComponent<AsteroidController>().Initialize(direction2);
    }
}
