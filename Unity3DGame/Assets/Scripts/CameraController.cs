using UnityEngine;

/// <summary>
/// Контроллер камеры от третьего лица
/// Управление: Мышь - вращение камеры, Колесо мыши - зум
/// </summary>
public class CameraController : MonoBehaviour
{
    [Header("Настройки камеры")]
    [SerializeField] private Transform target; // Цель (игрок)
    [SerializeField] private float distance = 5f; // Дистанция до игрока
    [SerializeField] private float height = 2f; // Высота камеры
    [SerializeField] private float rotationSpeed = 3f; // Скорость вращения
    
    [Header("Ограничения")]
    [SerializeField] private float minVerticalAngle = -30f;
    [SerializeField] private float maxVerticalAngle = 60f;
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private float maxDistance = 10f;

    private float horizontalAngle = 0f;
    private float verticalAngle = 10f;
    private float currentDistance;

    void Start()
    {
        if (target == null)
        {
            // Автоматический поиск игрока по тегу
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                target = player.transform;
        }
        
        currentDistance = distance;
        
        // Инициализация углов
        Vector3 currentRotation = transform.eulerAngles;
        horizontalAngle = currentRotation.y;
        verticalAngle = currentRotation.x;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Вращение камеры мышью
        if (Input.GetMouseButton(1) || Input.GetMouseButton(0)) // Правая или левая кнопка мыши
        {
            horizontalAngle += Input.GetAxis("Mouse X") * rotationSpeed;
            verticalAngle -= Input.GetAxis("Mouse Y") * rotationSpeed;
            
            // Ограничение вертикального угла
            verticalAngle = Mathf.Clamp(verticalAngle, minVerticalAngle, maxVerticalAngle);
        }

        // Зум колесом мыши
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            currentDistance -= scroll * zoomSpeed;
            currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);
        }

        // Применение вращения и позиции
        Quaternion rotation = Quaternion.Euler(verticalAngle, horizontalAngle, 0f);
        Vector3 position = target.position - rotation * Vector3.forward * currentDistance + Vector3.up * height;
        
        transform.rotation = rotation;
        transform.position = position;
        
        // Камера всегда смотрит на игрока
        transform.LookAt(target.position + Vector3.up * height * 0.5f);
    }

    // Отрисовка линии в редакторе для визуализации
    void OnDrawGizmosSelected()
    {
        if (target != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
}
