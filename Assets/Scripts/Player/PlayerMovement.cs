using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Vector3 lastMovementDirection = Vector3.right;
    private Rigidbody2D rb;
    private Animator animator;

    private PlayerAttack playerAttack;  // Ulti durumu için PlayerAttack class'ına erişim

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();  // PlayerAttack component'ini al
    }

    public void HandleMovement()
    {
        if (animator.GetBool("isAttacking"))
            return;

        float speedMultiplier = playerAttack.IsUltimateActive ? 1.5f : 1f;  
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;
        Vector2 newPosition = rb.position + movement * Time.deltaTime * moveSpeed * speedMultiplier;

        bool isMoving = movement.magnitude > 0f;

        if (horizontalInput < 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            lastMovementDirection = Vector3.left;
        }
        else if (horizontalInput > 0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            lastMovementDirection = Vector3.right;
        }

        rb.MovePosition(newPosition);
        animator.SetBool("isMoving", isMoving);
    }


    public void SaveTheDirection()
    {
        if (Input.GetAxis("Horizontal") == 0f && Input.GetAxis("Vertical") == 0f)
        {
            transform.localScale = new Vector3(lastMovementDirection.x, 1, 1);
        }
    }
}