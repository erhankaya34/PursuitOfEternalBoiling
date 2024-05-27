using Player;
using UnityEngine;
using UnityEngine.UI; // UI komponentleri için gerekli

public class UIManager : MonoBehaviour
{
    public Image ultiCooldownImage; // Ulti cooldown göstergesi için Image UI elementi
    public GameObject ultiActiveIcon; // Ulti aktif ikonu

    private PlayerAttack playerAttack; // PlayerAttack scriptine referans
    private CanvasGroup ultiIconCanvasGroup; // Ulti iconunun alpha değeri için

    void Start()
    {
        playerAttack = FindObjectOfType<PlayerAttack>(); // PlayerAttack component'ini bul
        if (playerAttack == null)
            Debug.LogError("PlayerAttack component not found!");

        ultiCooldownImage.fillAmount = 0;
        ultiActiveIcon.SetActive(false);
        ultiIconCanvasGroup = ultiActiveIcon.GetComponent<CanvasGroup>();
        ultiIconCanvasGroup.alpha = 1; // Başlangıçta alpha değerini 1 olarak ayarla

        // Oyun başladığında ulti'nin durumunu kontrol et
        if (playerAttack.IsUltimateActive)
        {
            ultiActiveIcon.SetActive(true);
            ultiCooldownImage.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        UpdateUltiUI();
    }

    private void UpdateUltiUI()
    {
        if (playerAttack.IsUltimateActive)
        {
            if (!ultiActiveIcon.activeSelf)
            {
                ultiActiveIcon.SetActive(true);
                ultiCooldownImage.gameObject.SetActive(false);
            }

            // Ulti süresinin sonuna yaklaştıkça alpha değerini azalt
            float remainingTime = (playerAttack.UltimateDuration - (Time.time - playerAttack.LastUltimateTime)) /
                                  playerAttack.UltimateDuration;
            ultiIconCanvasGroup.alpha = remainingTime;
        }
        else
        {
            if (ultiActiveIcon.activeSelf)
            {
                ultiActiveIcon.SetActive(false);
                ultiCooldownImage.gameObject.SetActive(true);
            }

            // Cooldown süresi boyunca cooldown barını doldur
            float elapsedCooldownTime = Time.time - playerAttack.LastUltimateTime;
            ultiCooldownImage.fillAmount = elapsedCooldownTime / playerAttack.UltimateCooldown;
            if (ultiCooldownImage.fillAmount >= 1.0f)
            {
                ultiCooldownImage.fillAmount = 1.0f;
            }
        }
    }
}