using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 moveDirection;
    private float _rotationSpeed;
    
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

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
