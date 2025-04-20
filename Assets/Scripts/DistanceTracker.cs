using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DistanceTracker : MonoBehaviour
{
    public float distanceTravelled = 0f; // Переменная для хранения пройденного расстояния
    
    public Text distanceText; // Ссылка на UI-элемент для отображения расстояния
    private MoveLeft moveLeftScript; // Ссылка на скрипт MoveLeft
    private PlayerController playerControllerScript;

    private float targetDistance = 1001f; // Целевое расстояние для победы



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

            // Получает текущий уровень из скрипта PlayerController
            int level = playerControllerScript.currentLevel;

            
            if (level == 1)
            {
                // Проверяем, достигнуто ли целевое расстояние для случая когда номер уровеня 1
                if (distanceTravelled >= targetDistance)
                {
                    // Завершаем игру и переходим на победный экран
                    GameOver();
                }
            }
            




        }

        // Обновляем текст на экране
        distanceText.text = "Distance: " + Mathf.Floor(distanceTravelled).ToString() + "m";

        

    }

    void GameOver()
    {
        playerControllerScript.gameOver = true; // Устанавливаем флаг завершения игры
        SceneManager.LoadScene("LevelOneWin"); // Переход на сцену с победным экраном
    }

}