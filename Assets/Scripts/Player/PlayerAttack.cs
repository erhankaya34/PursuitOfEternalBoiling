using Enemy;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject lightningEffectPrefab;
    [SerializeField] private float lightningEffectDuration = 0.5f;
    [SerializeField] private float attackCooldown = 0.75f;  
    [SerializeField] private Camera mainCamera; // Oyuncunun bakış açısını kullanmak için
    [SerializeField] private LayerMask enemyLayer; // Düşman objelerinin bulunduğu layer
    private float lastAttackTime;
    
    private Animator _animator;
    private EnemyStats _enemyStats;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyStats = GetComponent<EnemyStats>();
    }

    public void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > lastAttackTime + attackCooldown)
        {
            Shoot();
            lastAttackTime = Time.time;  
        }

        if (Input.GetMouseButtonUp(0))
        {
            _animator.SetBool("isAttacking", false);
        }
    }

    private void Shoot()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        float offset = 2.0f;
        Vector3 effectPosition = new Vector3(mousePosition.x, mousePosition.y + offset, mousePosition.z);
        
        GameObject lightningEffect = Instantiate(lightningEffectPrefab, effectPosition, Quaternion.identity);
        lightningEffect.SetActive(true);
        Destroy(lightningEffect, lightningEffectDuration);

        RaycastHit2D hit = Physics2D.Raycast(new Vector2(mousePosition.x, mousePosition.y), Vector2.up, 0.3f, enemyLayer);
        if (hit.collider != null && hit.collider.GetComponent<EnemyStats>())
        {
            int damage = Random.Range(30, 61);
            hit.collider.GetComponent<EnemyStats>().TakeDamage(damage);        
        }

        _animator.SetBool("isAttacking", true);
    }
}