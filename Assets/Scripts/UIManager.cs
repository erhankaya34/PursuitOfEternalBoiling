using UnityEngine;
using UnityEngine.UI; // UI komponentleri için gerekli

public class UIManager : MonoBehaviour
{
    public Image ultiCooldownImage; // Ulti cooldown göstergesi için Image UI elementi
    public GameObject ultiActiveIcon; // Ulti aktif ikonu

    private PlayerAttack playerAttack; // PlayerAttack scriptine referans

    void Start()
    {
        playerAttack = FindObjectOfType<PlayerAttack>(); // PlayerAttack component'ini bul
        if (playerAttack == null)
            Debug.LogError("PlayerAttack component not found!");
        
        ultiCooldownImage.fillAmount = 0;
        ultiActiveIcon.SetActive(false);
    }

    void Update()
    {
        UpdateUltiUI();
    }

    private void UpdateUltiUI()
    {
        // Ulti aktifse, cooldown UI'ını göster
        if (playerAttack.IsUltimateActive)
        {
            ultiCooldownImage.fillAmount = (Time.time - playerAttack.LastUltimateTime) / playerAttack.UltimateDuration;
            ultiActiveIcon.SetActive(true);
        }
        else
        {
            ultiCooldownImage.fillAmount = (Time.time - playerAttack.LastUltimateTime) / playerAttack.UltimateCooldown;
            ultiActiveIcon.SetActive(false);
        }
    }
}