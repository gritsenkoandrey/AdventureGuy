using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameOver : MonoBehaviour
{
    public void MenuPressed()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ExitPressed()
    {
        Application.Quit();
        Debug.Log("ExitPressed");
    }
}