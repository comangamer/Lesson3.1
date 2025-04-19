using UnityEngine;

public class MoveRight : MonoBehaviour
{
    public float speed = 10f; // Скорость движения объекта
    private PlayerController playerControllerScript; // Ссылка на скрипт PlayerController
    private float rightBound = 14; // Предел по оси X, за которым объект будет уничтожен
    public AudioClip powerShotCollisionSound; // Звук при столкновении с препятствием


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Находим объект с именем "Player" и получаем компонент PlayerController
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false)
        {
            // Двигаем объект вправо
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        // Проверяем, столкнулся ли объект с тегом "Obstacle"
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Уничтожаем объект при столкновении
            Destroy(gameObject);
            // Уничтожаем объект, с которым произошло столкновение
            Destroy(collision.gameObject);
            // Проигрываем звук уничтожение

        }

        if (collision.gameObject.CompareTag("Destroyer"))
        {
            // Уничтожаем объект при столкновении
            Destroy(gameObject);
        }

    }
}
