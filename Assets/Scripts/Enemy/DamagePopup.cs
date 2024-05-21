using TMPro;
using UnityEngine;

namespace Enemy
{
    public class DamagePopup : MonoBehaviour
    {
        public float disappearTimer = 1f;
        public Color textColor;
        public float moveSpeed = 1f;
        public Vector3 moveVector = new Vector3(0, 1, 0);

        private TextMeshPro textMesh;
        private float disappearSpeed = 3f;

        private void Awake()
        {
            textMesh = GetComponent<TextMeshPro>();
        }

        public void Setup(int damageAmount)
        {
            textMesh.SetText(damageAmount.ToString());
            textColor = textMesh.color;
        }

        private void Update()
        {
            transform.position += moveVector * moveSpeed * Time.deltaTime;
            disappearTimer -= Time.deltaTime;
            if (disappearTimer < 0)
            {
                textColor.a -= disappearSpeed * Time.deltaTime;
                textMesh.color = textColor;
                if (textColor.a <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}

