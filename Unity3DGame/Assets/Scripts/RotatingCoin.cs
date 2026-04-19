using UnityEngine;

/// <summary>
/// Вращающаяся монета для сбора
/// </summary>
public class RotatingCoin : MonoBehaviour
{
    [Header("Настройки вращения")]
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float bobbingSpeed = 2f;
    [SerializeField] private float bobbingHeight = 0.3f;
    
    private Vector3 startPosition;
    private float randomOffset;

    void Start()
    {
        startPosition = transform.position;
        randomOffset = Random.Range(0f, Mathf.PI * 2);
        
        // Добавление триггера если отсутствует
        if (GetComponent<SphereCollider>() == null)
        {
            SphereCollider collider = gameObject.AddComponent<SphereCollider>();
            collider.isTrigger = true;
            collider.radius = 0.5f;
        }
        
        // Добавление тега если отсутствует
        if (!CompareTag("Coin"))
        {
            gameObject.tag = "Coin";
        }
    }

    void Update()
    {
        // Вращение вокруг оси Y
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        
        // Плавное покачивание вверх-вниз
        float newY = startPosition.y + Mathf.Sin(Time.time * bobbingSpeed + randomOffset) * bobbingHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    // Отрисовка в редакторе
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
