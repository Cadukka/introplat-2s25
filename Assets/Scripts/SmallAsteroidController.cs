using UnityEngine;
using Random = UnityEngine.Random;

public class SmallAsteroidController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 moveDirection;
    private float _rotationSpeed;

    // Reference of Big Asteroid
    AsteroidController bigAsteroid;
    
    void Start()
    {
        moveDirection = bigAsteroid.moveDirection * new Vector2(45,45);
        _rotationSpeed = Random.Range(-100f, 100f);
    }

    void FixedUpdate()
    {
        rb.angularVelocity = _rotationSpeed;
        rb.velocity = moveDirection * moveSpeed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
