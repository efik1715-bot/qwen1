using UnityEngine;

/// <summary>
/// Менеджер игры - управление состоянием игры, паузой, перезапуском
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Настройки игры")]
    [SerializeField] private float gameTime = 120f; // Время игры в секундах
    
    private bool isGameActive = true;
    private bool isPaused = false;
    private float currentTime;
    private int currentScore = 0;
    
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        // Синглтон паттерн
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        currentTime = gameTime;
        isGameActive = true;
        isPaused = false;
        
        Debug.Log("Игра началась! У вас есть " + gameTime + " секунд.");
    }

    void Update()
    {
        if (!isGameActive || isPaused) return;

        // Отсчет времени
        currentTime -= Time.deltaTime;
        
        if (currentTime <= 0)
        {
            currentTime = 0;
            EndGame();
        }

        // Пауза по нажатию Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    /// <summary>
    /// Переключение паузы
    /// </summary>
    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Debug.Log("Игра на паузе");
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Debug.Log("Игра продолжена");
        }
    }

    /// <summary>
    /// Добавление очков
    /// </summary>
    public void AddScore(int points)
    {
        if (!isGameActive) return;
        
        currentScore += points;
        Debug.Log($"Очки: {currentScore}");
    }

    /// <summary>
    /// Завершение игры
    /// </summary>
    private void EndGame()
    {
        isGameActive = false;
        Time.timeScale = 0f;
        
        Debug.Log("=== ИГРА ОКОНЧЕНА ===");
        Debug.Log($"Финальный счет: {currentScore}");
        Debug.Log($"Время: {gameTime - currentTime} секунд");
        
        // Здесь можно показать UI экрана конца игры
        // UIManager.Instance.ShowGameOverScreen(currentScore);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /// <summary>
    /// Перезапуск игры
    /// </summary>
    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Выход из игры
    /// </summary>
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    // Геттеры
    public float GetTimeRemaining() => currentTime;
    public int GetScore() => currentScore;
    public bool IsGameActive() => isGameActive;
}
