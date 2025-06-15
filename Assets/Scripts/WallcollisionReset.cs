using UnityEngine;

public class WallCollisionReset : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            BallController ballController = collision.gameObject.GetComponent<BallController>();
            if (ballController != null)
            {
                ballController.ResetBall();
            }
            else
            {
                Debug.LogWarning("BallController script not found on the Ball!");
            }
        }
    }
}
