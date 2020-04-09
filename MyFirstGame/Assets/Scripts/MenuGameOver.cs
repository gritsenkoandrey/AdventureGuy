using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameOver : MonoBehaviour
{
    #region Method

    public void MenuPressed()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitPressed()
    {
        Application.Quit();
    }

    #endregion
}