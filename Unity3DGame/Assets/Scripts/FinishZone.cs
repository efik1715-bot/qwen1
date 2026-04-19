using UnityEngine;

/// <summary>
/// Зона финиша - завершает уровень при достижении
/// </summary>
public class FinishZone : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private float timeBonus = 10f; // Бонус времени за достижение финиша
    [SerializeField] private int finishPoints = 100; // Очки за финиш
    
    private bool playerReached = false;

    void Start()
    {
        // Добавление триггера если отсутствует
        if (GetComponent<BoxCollider>() == null)
        {
            BoxCollider collider = gameObject.AddComponent<BoxCollider>();
            collider.isTrigger = true;
        }
        
        // Визуализация в редакторе
        Debug.Log("Зона финиша создана. Доберитесь до неё чтобы завершить уровень!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerReached) return;
        
        if (other.CompareTag("Player"))
        {
            ReachFinish();
        }
    }

    private void ReachFinish()
    {
        playerReached = true;
        
        Debug.Log("=== УРОВЕНЬ ЗАВЕРШЕН! ===");
        Debug.Log($"Бонус времени: {timeBonus} сек");
        Debug.Log($"Очки за финиш: {finishPoints}");
        
        // Добавление бонусов через GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(finishPoints);
            // Можно добавить бонусное время если нужно
        }
        
        // Эффект победы (можно добавить партиклы, звук и т.д.)
        ShowVictoryEffect();
        
        // Переход на следующий уровень или рестарт через некоторое время
        Invoke(nameof(LoadNextLevel), 3f);
    }

    private void ShowVictoryEffect()
    {
        // Здесь можно добавить:
        // - Частицы конфетти
        // - Звук победы
        // - Анимацию
        // - UI сообщение
        
        Debug.Log("🎉 ПОБЕДА! 🎉");
    }

    private void LoadNextLevel()
    {
        // Загрузка следующего уровня
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
        // Для демонстрации - рестарт текущей сцены
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartGame();
        }
    }

    // Отрисовка зоны финиша в редакторе
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(3f, 3f, 3f));
    }
}
