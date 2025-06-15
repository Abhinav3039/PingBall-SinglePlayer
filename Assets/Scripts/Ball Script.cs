using UnityEngine;

public class BallController : MonoBehaviour
{
    public float baseSpeed = 1000f;
    public float speedIncrease = 150f;
    public float maxSpeed = 2000f;

    private Rigidbody2D rb;
    private float currentSpeed;
    private int hitCounter = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        currentSpeed = baseSpeed;
        LaunchBall();
    }

    void LaunchBall()
    {
        float angle = Random.Range(170f, 190f); // near-horizontal
        float radians = angle * Mathf.Deg2Rad;

        Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;

        rb.linearVelocity = direction * currentSpeed;
    }

    public void ResetBall()
    {
        transform.position = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
        hitCounter = 0;                      // Reset hit counter
        currentSpeed = baseSpeed;           // Reset speed
        Invoke(nameof(LaunchBall), 1f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            // Determine bounce direction
            Vector3 paddlePos = collision.transform.position;
            float paddleHeight = collision.collider.bounds.size.y;
            float yOffset = (transform.position.y - paddlePos.y) / (paddleHeight / 2);

            Vector2 newDirection = new Vector2(rb.linearVelocity.x > 0 ? 1 : -1, yOffset).normalized;

            // Count hit and increase speed
            hitCounter++;
            currentSpeed = Mathf.Min(baseSpeed + hitCounter * speedIncrease, maxSpeed);
            rb.linearVelocity = newDirection * currentSpeed;
        }

        if (collision.gameObject.CompareTag("LeftWall") || collision.gameObject.CompareTag("RightWall"))
        {
            Debug.Log("Ball missed! Resetting...");
            ResetBall();
        }
        if (collision.gameObject.CompareTag("LeftWall"))
        {
            ScoreManager.instance.AddAIScore();
            ResetBall();
        }

        if (collision.gameObject.CompareTag("RightWall"))
        {
            ScoreManager.instance.AddPlayerScore();
            ResetBall();
        }

    }
}
