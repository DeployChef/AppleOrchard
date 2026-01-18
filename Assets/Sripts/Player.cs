using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private ActorController actorController;

    private Rigidbody2D rb;
    private float moveInput;
    private bool canMove;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void EnableControls()
    {
        canMove = true;
    }

    public void DisableControls()
    {
        actorController.Stop();
        canMove = false;
        moveInput = 0f;
    }

    private void Update()
    {
        if (canMove && !DialogueManager.Instance.IsDialogueActive)
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
        else
        {
            moveInput = 0;
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
