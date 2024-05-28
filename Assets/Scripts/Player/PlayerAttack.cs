using System.Collections;
using Enemy;
using UnityEngine;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        // Lightning Attack Configuration
        [Header("Lightning Attack Settings")]
        [SerializeField] private GameObject lightningEffectPrefab;
        [SerializeField] private GameObject ultimateLightningPrefab;
        [SerializeField] private float lightningEffectDuration = 0.5f;
        [SerializeField] private float attackCooldown = 0.75f;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private LayerMask enemyLayer;

        // Fire Attack Configuration
        [Header("Fire Attack Settings")]
        [SerializeField] private GameObject fireEffectPrefab;
        [SerializeField] private GameObject ashPrefab;
        [SerializeField] private LayerMask treeLayer;
        [SerializeField] private float fireDuration = 3f;

        // Attack Timing Control
        [Header("Attack Timing")]
        [SerializeField] private float lastAttackTime;
        [SerializeField] private float _lastUltimateTime = -10f;
        [SerializeField] private bool _isUltimateActive = false;

        // Component References
        [Header("Component References")]
        private Animator _animator;
        private EnemyStats _enemyStats;

        // Public Properties
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
            GameObject effectPrefab = _isUltimateActive ? ultimateLightningPrefab : lightningEffectPrefab;
            GameObject lightningEffect = Instantiate(effectPrefab, new Vector3(mousePosition.x, mousePosition.y + 2, mousePosition.z), Quaternion.identity);
            lightningEffect.SetActive(true);
            Destroy(lightningEffect, lightningEffectDuration);

            // CircleCast kullanarak genişletilmiş algılama
            float radius = 0.5f; // Algılama yarıçapını ayarlayın
            int combinedLayerMask = enemyLayer | treeLayer; // Katman maskelerini birleştir
            RaycastHit2D hit = Physics2D.CircleCast(new Vector2(mousePosition.x, mousePosition.y), radius, Vector2.up, 0.1f, combinedLayerMask);

            if (hit.collider != null)
            {
                if (hit.collider.GetComponent<EnemyStats>())
                {
                    int damage = _isUltimateActive ? Random.Range(60, 121) : Random.Range(30, 61);
                    hit.collider.GetComponent<EnemyStats>().TakeDamage(damage);
                }
                else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Tree"))
                {
                    Debug.Log("Tree Layer Detected");
                    StartCoroutine(BurnTree(hit.collider.gameObject));
                }
            }

            _animator.SetBool("isAttacking", true);
        }



        private IEnumerator BurnTree(GameObject tree)
        {
            GameObject fireEffect= Instantiate(fireEffectPrefab, tree.transform.position+ Vector3.up*2, Quaternion.identity);
            yield return new WaitForSeconds(fireDuration); 
            Destroy(tree);
            Destroy(fireEffect);
            Instantiate(ashPrefab, tree.transform.position, Quaternion.identity);
        }
    }
}