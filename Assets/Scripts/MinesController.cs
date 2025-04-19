using UnityEngine;
using UnityEngine.UI;

public class MinesController : MonoBehaviour
{

    public int minesPassed = 0; // Переменная для хранения количества пройденных мин
    public Text minesText; // Ссылка на UI-элемент для отображения количества пройденных мин

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        minesText = GameObject.Find("MinesPassed").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        minesText.text = "Mines Passed: " + minesPassed.ToString();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            minesPassed++;
            minesText.text = "Mines Passed: " + minesPassed.ToString();

        }
    }
}
