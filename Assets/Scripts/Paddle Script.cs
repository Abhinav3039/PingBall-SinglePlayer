using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleMouseControl : MonoBehaviour
{
    public InputActionAsset inputActions;
    private InputAction moveAction;
    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;

        // Find the "Move" action from the asset
        moveAction = inputActions.FindActionMap("Player").FindAction("Move");
        moveAction.Enable();
    }

    void Update()
    {
        Vector2 mouseScreenPos = moveAction.ReadValue<Vector2>();
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(mouseScreenPos.x, mouseScreenPos.y, -mainCamera.transform.position.z));

        // Only update Y position (vertical paddle movement)
        Vector3 newPos = transform.position;
        newPos.y = Mathf.Clamp(mouseWorldPos.y, -6.5f, 6.5f); // Updated limits for new board size
        transform.position = newPos;
    }
}
