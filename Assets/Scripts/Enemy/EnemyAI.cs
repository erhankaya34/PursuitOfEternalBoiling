using UnityEngine;

namespace Enemy
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private float detectionRange = 10f;  
        private Rigidbody2D _rigidbody2D;
        private EnemyStats _enemyStats;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _enemyStats = GetComponent<EnemyStats>(); 
        }

        private void FixedUpdate()
        {
            MoveTowardsPlayer();
        }

        private void Update()
        {
            FacePlayer();
        }

        private void MoveTowardsPlayer()
        {
            Vector2 targetPosition = _player.transform.position;
            Vector2 currentPosition = transform.position;
            float distanceToPlayer = Vector2.Distance(currentPosition, targetPosition);

            if (distanceToPlayer <= detectionRange)  
            {
                Vector2 direction = (targetPosition - currentPosition).normalized;
                _rigidbody2D.velocity = direction * _enemyStats.Speed; 
            }
            else
            {
                _rigidbody2D.velocity = Vector2.zero; 
            }
        }

        private void FacePlayer()
        {
            if (_player.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }
}