using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    private bool isDraggingItem = false;
    private GameObject draggedItem;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] private GameObject lightningEffectPrefab;
    [SerializeField] private float lightningEffectDuration = 0.5f;

    private Vector3 lastMovementDirection = Vector3.right;
    private Rigidbody2D _playerRigidbody;
    private Animator _playerAnimator;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleShooting();
        HandleInteraction();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void LateUpdate()
    {
        SaveTheDirection();
    }

    private void HandleMovement()
    {
        //controlling of movement while player attacking
        if (_playerAnimator.GetBool("isAttacking"))
            return;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;
        Vector2 newPosition = _playerRigidbody.position + movement * Time.deltaTime * moveSpeed;

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

        _playerRigidbody.MovePosition(newPosition);
        _playerAnimator.SetBool("isMoving", isMoving);
    }

    private void SaveTheDirection()
    {
        if (Input.GetAxis("Horizontal") == 0f && Input.GetAxis("Vertical") == 0f) //if player is not moving
        {
            transform.localScale = new Vector3(lastMovementDirection.x, 1, 1);
        }
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetMouseButtonUp(0))
        {
            _playerAnimator.SetBool("isAttacking", false);
        }
    }

    private void Shoot()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; 
        float offset = 2.0f; 
        Vector3 effectPosition = new Vector3(mousePosition.x, mousePosition.y + offset, mousePosition.z);
        
        GameObject lightningEffect = Instantiate(lightningEffectPrefab, effectPosition, Quaternion.identity);
        
        lightningEffect.SetActive(true);
        Destroy(lightningEffect, lightningEffectDuration);

        _playerAnimator.SetBool("isAttacking", true);
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