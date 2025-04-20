using System.Collections; // ��� ������ � ����������
using System.Collections.Generic; // ��� ������ � �����������
using UnityEngine;
using UnityEngine.SceneManagement; // ��� �������� ����

public class MainMenu : MonoBehaviour
{

    //public GameObject AboutCanvas; // ������ �� ������ "� ���"
    //public GameObject InstructionBG; // ������ �� ������ � �����������

    public void LevelChoose()
    {
        SceneManager.LoadScene("LevelChoose");
    }

    public void StartLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void StartLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void QuitGame() 
    {
        Debug.Log("Game closed"); 
        Application.Quit(); 
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ToAbout()
    {
        SceneManager.LoadScene("About");
    }

    public void ToLevelOneWin()
    {
        SceneManager.LoadScene("LevelOneWin");
    }

    public void ToLevelOneDescription()
    {
        SceneManager.LoadScene("LevelOneDescription");
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
