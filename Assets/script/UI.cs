using UnityEngine;
using UnityEngine.UI;

public class Ui : MonoBehaviour
{
    public Image healthBar;
    public Image healthImage;
    public PlayerControl scriptJoueur;
    public Sprite[] healthSprites;
    public Text coinsText;

    private int totalCoinsCollected = 0;

    private void OnEnable()
    {
        Collectible.OnCollect += HandleCollectEvent;
    }

    private void OnDisable()
    {
        Collectible.OnCollect -= HandleCollectEvent;
    }


    public void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        float healthPercentage = (float)currentHealth / maxHealth;

        healthBar.fillAmount = healthPercentage;

        int spriteIndex = Mathf.FloorToInt(healthPercentage * healthSprites.Length);
        spriteIndex = Mathf.Clamp(spriteIndex, 0, healthSprites.Length - 1);
        healthImage.sprite = healthSprites[spriteIndex];
    }


    private void HandleCollectEvent(int collectedPoints)
    {
        totalCoinsCollected += collectedPoints;
        UpdateCoinsUI();
    }


    private void UpdateCoinsUI()
    {

        if (coinsText != null)
        {
            coinsText.text = " X " + totalCoinsCollected;
        }
        else
        {
            Debug.LogWarning("La référence à l'objet Text n'est pas définie dans l'éditeur Unity.");
        }
    }
}
