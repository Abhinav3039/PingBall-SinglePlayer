using UnityEngine;

public class BallResetter : MonoBehaviour
{
    public BallController ballController;

    public void ManualResetBall()
    {
        if (ballController != null)
        {
            ballController.ResetBall(); // This is the correct method call
        }
    }
}
