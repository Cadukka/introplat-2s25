using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float lifetime = 2f;
    public Rigidbody2D rb;
    
    private void Start()
    {
        Destroy(gameObject, lifetime); 
    }

    private void FixedUpdate()
    {
        Vector2 moveDirection = transform.up * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveDirection);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            Debug.Log("Collided with an asteroid!");
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
