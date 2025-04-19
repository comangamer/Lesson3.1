using UnityEngine;
using UnityEngine.UI;

public class DistanceTracker : MonoBehaviour
{
    public float distanceTravelled = 0; // Переменная для хранения пройденного расстояния
    public Text distanceText; // Ссылка на UI-элемент для отображения расстояния
    private MoveLeft moveLeftScript; // Ссылка на скрипт MoveLeft
    private PlayerController playerControllerScript;


    // Метод Start вызывается перед первым кадром
    void Start()
    {
        // Находим объект с именем "DistanceText" в сцене и получаем компонент Text
        distanceText = GameObject.Find("DistanceText").GetComponent<Text>();

        // Находим объект с компонентом MoveLeft (например, фон или препятствие)
        moveLeftScript = GameObject.FindObjectOfType<MoveLeft>();

        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Метод Update вызывается каждый кадр
    void Update()
    {

        if (playerControllerScript.gameOver == false)
        { 
            // Увеличиваем пройденное расстояние на основе скорости и времени
            distanceTravelled += moveLeftScript.speed * Time.deltaTime / 3;
        }

        // Обновляем текст на экране
        distanceText.text = "Distance: " + Mathf.Floor(distanceTravelled).ToString() + "m";
    }
}