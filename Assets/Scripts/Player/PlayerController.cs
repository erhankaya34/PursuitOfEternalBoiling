using Player;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;
    private PlayerInteraction playerInteraction;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
        playerInteraction = GetComponent<PlayerInteraction>();

        if (playerMovement == null || playerAttack == null || playerInteraction == null)
        {
            Debug.LogError("PlayerController cannot find one or more player component scripts!");
        }
    }

    void Update()
    {
        playerAttack.HandleShooting();
        playerInteraction.HandleInteraction();
    }

    void FixedUpdate()
    {
        playerMovement.HandleMovement();
    }

    void LateUpdate()
    {
        playerMovement.SaveTheDirection();
    }
}