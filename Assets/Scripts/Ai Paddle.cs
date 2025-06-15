using UnityEngine;

public class AIPaddleController : MonoBehaviour
{
    public Transform ball;             // Assign Ball GameObject in Inspector
    private float moveSpeed;           // Will be set based on difficulty
    public float activationX = 0f;     // Only follow ball if it crosses this x-position

    private float paddleHalfHeight;
    private float maxBoardY = 7f;
    private float minBoardY = -7f;

    void Start()
    {
        // Set AI speed based on selected difficulty
        switch (GameSettings.currentDifficulty)
        {
            case Difficulty.Easy:
                moveSpeed = 8f;
                break;
            case Difficulty.Medium:
                moveSpeed = 15f;
                break;
            case Difficulty.Hard:
                moveSpeed = 20f;
                break;
        }

        // Automatically get the paddle's height (half of it)
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        if (col != null)
        {
            paddleHalfHeight = col.bounds.extents.y;
        }
        else
        {
            paddleHalfHeight = 0.5f; // fallback default
        }
    }

    void Update()
    {
        if (ball == null) return;

        // Only follow the ball if it's on the right side of the board
        if (ball.position.x < activationX) return;

        float targetY = ball.position.y;
        float newY = Mathf.MoveTowards(transform.position.y, targetY, moveSpeed * Time.deltaTime);

        // Clamp movement within board bounds
        float maxY = maxBoardY - paddleHalfHeight;
        float minY = minBoardY + paddleHalfHeight;
        newY = Mathf.Clamp(newY, minY, maxY);

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
