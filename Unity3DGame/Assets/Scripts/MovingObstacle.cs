using UnityEngine;

/// <summary>
/// Препятствие с движущейся платформой
/// </summary>
public class MovingObstacle : MonoBehaviour
{
    [Header("Настройки движения")]
    [SerializeField] private Vector3 moveRange = new Vector3(5f, 0f, 0f); // Диапазон движения по осям
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private bool useSineWave = true; // Плавное движение (синус) или линейное
    
    private Vector3 startPosition;
    private float timeOffset;

    void Start()
    {
        startPosition = transform.position;
        timeOffset = Random.Range(0f, Mathf.PI * 2);
        
        // Добавление коллайдера если отсутствует
        if (GetComponent<BoxCollider>() == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }
    }

    void Update()
    {
        Vector3 offset = Vector3.zero;
        
        if (useSineWave)
        {
            // Плавное синусоидальное движение
            float sineValue = Mathf.Sin(Time.time * moveSpeed + timeOffset);
            offset = moveRange * sineValue;
        }
        else
        {
            // Линейное движение туда-обратно
            float pingPongValue = Mathf.PingPong(Time.time * moveSpeed, 2f) - 1f;
            offset = moveRange * pingPongValue;
        }
        
        transform.position = startPosition + offset;
    }

    // Отрисовка диапазона движения в редакторе
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(startPosition != Vector3.zero ? startPosition : transform.position, moveRange * 2);
    }
}
