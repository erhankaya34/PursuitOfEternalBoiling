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
        [SerializeField] private GameObject elementPrefab; // Optional
        [SerializeField] private GameObject damagePopupPrefab; // Reference to the damage popup prefab

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
            set { _health = Mathf.Max(0, value); } // Health value cannot be negative
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _enemyAnimationManager = GetComponent<EnemyAnimationManager>();
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                StartCoroutine(DelayedDeath());
            }
            else
            {
                StartCoroutine(FlashDamageEffect());
                if (damagePopupPrefab != null)
                {
                    var popup = Instantiate(damagePopupPrefab, transform.position, Quaternion.identity);
                    popup.GetComponent<DamagePopup>().Setup(damage);
                }
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
