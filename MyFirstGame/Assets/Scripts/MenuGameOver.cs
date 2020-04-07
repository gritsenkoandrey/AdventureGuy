using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameOver : MonoBehaviour
{
    public void PlayPressed()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ExitPressed()
    {
        Application.Quit();
        Debug.Log("ExitPressed");
    }
}