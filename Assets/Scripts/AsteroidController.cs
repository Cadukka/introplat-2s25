using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] public int life = 2;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public Vector2 moveDirection;
    private float _rotationSpeed;

    [SerializeField] GameObject smallAsteroid;
    
    void Start()
    {
        moveDirection = (Random.insideUnitCircle * 5f).normalized;
        _rotationSpeed = Random.Range(-100f, 100f);
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

        Instantiate(smallAsteroid, transform.position, Quaternion.identity);
        Instantiate(smallAsteroid, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
