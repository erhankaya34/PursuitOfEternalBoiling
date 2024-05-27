using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class EnemyStats : MonoBehaviour
    {
        [SerializeField] private float _speed = 3f;
        [SerializeField] private int _health = 100;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Material originalMaterial;
        [SerializeField] private Material damagedMaterial;
        [SerializeField] private GameObject elementPrefab;
        [SerializeField] private GameObject damagePopupPrefab;

        private Rigidbody2D _rigidbody;
        private EnemyAnimationManager _enemyAnimationManager;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public int Health
        {
            get { return _health; }
            set { _health = Mathf.Max(0, value); }
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _enemyAnimationManager = GetComponent<EnemyAnimationManager>();
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;

            if (damagePopupPrefab != null)
            {
                var popup = Instantiate(damagePopupPrefab, transform.position, Quaternion.identity);
                popup.GetComponent<DamagePopup>().Setup(damage);
            }

            if (DamageCameraShake.Instance != null)
            {
                DamageCameraShake.Instance.TriggerShake();
                if (damage >= 80)
                {
                    DamageCameraShake.Instance.TriggerHighImpactEffect(transform); // Trigger the high impact effect
                }
            }

            if (Health <= 0)
            {
                StartCoroutine(DelayedDeath());
            }
            else
            {
                StartCoroutine(FlashDamageEffect());
            }
        }

        private IEnumerator FlashDamageEffect()
        {
            spriteRenderer.material = damagedMaterial;
            yield return new WaitForSeconds(0.3f);
            spriteRenderer.material = originalMaterial;
        }

        private IEnumerator DelayedDeath()
        {
            _speed = 0;
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.isKinematic = true;
            GetComponent<Collider2D>().enabled = false;
            
            _enemyAnimationManager.Die();
            DropElement();
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }

        private void DropElement()
        {
            if (elementPrefab != null)
            {
                Instantiate(elementPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
