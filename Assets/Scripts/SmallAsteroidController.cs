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
    
    public void Initialize(AsteroidController big)
    {
        bigAsteroid = big;
        //moveDirection = bigAsteroid.moveDirection + Quaternion.Angle(moveDirection);

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
