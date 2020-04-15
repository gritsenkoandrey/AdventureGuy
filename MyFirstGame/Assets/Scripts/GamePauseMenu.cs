using UnityEngine;
using UnityEngine.SceneManagement;


public class GamePauseMenu : MonoBehaviour
{
    #region Fields

    private bool _isPaused = false;

    [SerializeField] private GameObject _pauseMenuUI;

    #endregion


    #region UnityMethod

    private void Update()
    {
        GamePause();
    }

    #endregion


    #region Method

    private void GamePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        _pauseMenuUI.SetActive(false); // во время продолжения игры меню не должно отображаться
        Time.timeScale = 1.0f;
        _isPaused = false;
    }

    private void Pause()
    {
        // говорит о том, что это будут кнопки и мы должны с ними как то взаимодействовать
        _pauseMenuUI.SetActive(true); // во время паузы это меню должно появляться
        Time.timeScale = 0f;
        _isPaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1.0f; // для того чтобы время начало двигаться дальше
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    #endregion

    // чтобы кнопки продолжали анимацию, нужно в Animator выбрать Unscale Time.
}