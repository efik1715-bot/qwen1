using UnityEngine;

/// <summary>
/// Система сбора монет и подсчета очков
/// </summary>
public class CoinCollector : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private int pointsPerCoin = 10;
    [SerializeField] private AudioClip collectSound;
    
    private int totalCoins = 0;
    private int currentScore = 0;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
            
        UpdateUI();
    }

    /// <summary>
    /// Добавление монеты при столкновении
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            CollectCoin(other.gameObject);
        }
    }

    /// <summary>
    /// Сбор монеты
    /// </summary>
    private void CollectCoin(GameObject coin)
    {
        // Воспроизведение звука
        if (collectSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(collectSound);
        }
        
        // Увеличение счетчика
        totalCoins++;
        currentScore += pointsPerCoin;
        
        // Уничтожение монеты с эффектом (можно добавить партиклы)
        Destroy(coin);
        
        // Обновление UI
        UpdateUI();
        
        Debug.Log($"Монета собрана! Всего: {totalCoins}, Очки: {currentScore}");
    }

    /// <summary>
    /// Обновление интерфейса
    /// </summary>
    private void UpdateUI()
    {
        // Здесь можно подключить ваш UI систему
        // Например: UIManager.Instance.UpdateScore(currentScore, totalCoins);
    }

    /// <summary>
    /// Получение текущего счета
    /// </summary>
    public int GetScore() => currentScore;
    
    /// <summary>
    /// Получение количества собранных монет
    /// </summary>
    public int GetTotalCoins() => totalCoins;
}
