using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isDraggingItem = false;
    private GameObject draggedItem;
    [SerializeField] float moveSpeed = 1f;
    private Rigidbody2D _playerRigidbody;
    private Animator _playerAnimator;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleMovement();
        HandleShooting();
        HandleInteraction();
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized * Time.deltaTime * moveSpeed;
        Vector2 newPosition = _playerRigidbody.position + movement;
        
        bool isMoving = movement.magnitude > 0f;

        _playerRigidbody.MovePosition(newPosition);  
        _playerAnimator.SetBool("isMoving", isMoving);

    }

    private void HandleShooting()
    {
        // Fare tıklaması ile ateş etme
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // Ateş etme kodu
    }

    private void HandleInteraction()
    {
        // E tuşu ile nesne toplama ve bırakma işlemleri
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isDraggingItem)
            {
                PickUpItem();
            }
            else
            {
                DropItem();
            }
        }

        // Mouse ile envanterden nesne sürükleme
        if (isDraggingItem && Input.GetMouseButton(0))
        {
            DragItem();
        }
    }

    private void PickUpItem()
    {
        // Nesneyi toplama işlemi
    }

    private void DropItem()
    {
        // Nesneyi bırakma işlemi
    }

    private void DragItem()
    {
        // Nesneyi sürükleme işlemi
    }
}

