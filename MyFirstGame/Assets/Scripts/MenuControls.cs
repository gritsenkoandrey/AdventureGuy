using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    public void PlayPressed()
    {
        SceneManager.LoadScene("Level-1");
    }

    public void ExitPressed()
    {
        Application.Quit(); // метод работает только в билде
        Debug.Log("ExitPressed"); // проверяем работу
    }
}