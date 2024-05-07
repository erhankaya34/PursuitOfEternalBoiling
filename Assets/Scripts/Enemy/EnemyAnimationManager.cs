using UnityEngine;

namespace Enemy
{
    public class EnemyAnimationManager : MonoBehaviour
    {
        private Animator _animator;
        private Rigidbody2D _rigidbody2D;
        private EnemyStats _stats;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _stats = GetComponent<EnemyStats>();
        }

        private void Update()
        {
            // Trigger walking anim by calculating absolute value of speed
            SetWalking(Mathf.Abs(_rigidbody2D.velocity.x)); 
        }

        private void SetWalking(float speed)
        {
            _animator.SetFloat("Speed", speed);
        }

        public void Die()
        {
            _animator.SetBool("Dead", true);
        }
    }
}