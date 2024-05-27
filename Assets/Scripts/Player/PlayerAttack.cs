using System.Collections;
using Enemy;
using UnityEngine;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private GameObject lightningEffectPrefab;
        [SerializeField] private GameObject ultimateLightningPrefab;
        [SerializeField] private float lightningEffectDuration = 0.5f;
        [SerializeField] private float attackCooldown = 0.75f;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private LayerMask enemyLayer;

        private float lastAttackTime;
        private float _lastUltimateTime = -10f;
        private bool _isUltimateActive = false;

        private Animator _animator;
        private EnemyStats _enemyStats;

        // Public properties for outside access
        public bool IsUltimateActive => _isUltimateActive;
        public float LastUltimateTime => _lastUltimateTime;
        public float UltimateDuration { get; private set; } = 5f;
        public float UltimateCooldown { get; private set; } = 10f;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _enemyStats = GetComponent<EnemyStats>();
        }

        private void Update()
        {
            // Ulti aktivasyonu için gerekli koşul kontrolü
            if (Input.GetKeyDown(KeyCode.X) && Time.time > _lastUltimateTime + UltimateCooldown && !_isUltimateActive)
            {
                StartCoroutine(ActivateUltimate());
            }
            HandleShooting();
        }

        private IEnumerator ActivateUltimate()
        {
            _isUltimateActive = true; // Ulti'yi aktif hale getir
            yield return new WaitForSeconds(UltimateDuration); // Ulti süresi boyunca beklet
            _isUltimateActive = false; // Ulti süresi bittiğinde aktif durumu kapat
            _lastUltimateTime = Time.time; // Ulti'nin bittiği zamanı kaydet
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
            Vector3 effectPosition = new Vector3(mousePosition.x, mousePosition.y + 2, mousePosition.z);  
            GameObject effectPrefab = _isUltimateActive ? ultimateLightningPrefab : lightningEffectPrefab;
            GameObject lightningEffect = Instantiate(effectPrefab, effectPosition, Quaternion.identity);
            lightningEffect.SetActive(true);
            Destroy(lightningEffect, lightningEffectDuration);

            RaycastHit2D hit = Physics2D.Raycast(new Vector2(mousePosition.x, mousePosition.y), Vector2.up, 0.3f, enemyLayer);
            if (hit.collider != null && hit.collider.GetComponent<EnemyStats>())
            {
                int damage = _isUltimateActive ? Random.Range(60, 121) : Random.Range(30, 61);
                hit.collider.GetComponent<EnemyStats>().TakeDamage(damage);
            }

            _animator.SetBool("isAttacking", true);
        }
    }
}
