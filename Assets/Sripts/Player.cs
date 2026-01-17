using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private ActorController actorController;

    private Rigidbody2D rb;
    private float moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Читаем клавиатуру напрямую
        moveInput = 0f;

        if (Keyboard.current.aKey.isPressed)
            moveInput = -1f;
        else if (Keyboard.current.dKey.isPressed)
            moveInput = 1f;

        if (moveInput != 0)
        {
            actorController.Run();
            actorController.Turn(moveInput < 0 ? 1 : -1);
        }
        else
        {
            actorController.Stop();
        }
    }


    private void FixedUpdate()
    {
        // Движение только по X
        rb.linearVelocity = new Vector2(
            moveInput * moveSpeed,
            rb.linearVelocity.y
        );
    }
}
