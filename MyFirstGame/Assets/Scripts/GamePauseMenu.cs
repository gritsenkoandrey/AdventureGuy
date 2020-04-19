using UnityEngine;
using UnityEngine.SceneManagement;


public class GamePauseMenu : MonoBehaviour
{
    #region Fields

    private bool _isPaused = false;

    [SerializeField] private GameObject _pauseMenuUI;
    [SerializeField] private AudioSource _audioMainCamera;
    [SerializeField] private AudioSource _audioSound;


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
        _audioMainCamera.volume = 0.1f;
        _audioSound.volume = 1.0f;
        _isPaused = false;
    }

    private void Pause()
    {
        // говорит о том, что это будут кнопки и мы должны с ними как то взаимодействовать
        _pauseMenuUI.SetActive(true); // во время паузы это меню должно появляться
        Time.timeScale = 0f;
        _audioMainCamera.volume = 0f;
        _audioSound.volume = 0f;
        _isPaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1.0f; // для того чтобы время начало двигаться дальше
        _audioMainCamera.volume = 0.1f;
        _audioSound.volume = 1.0f;
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GiveUp()
    {
        Time.timeScale = 1.0f;
        _audioMainCamera.volume = 0.1f;
        _audioSound.volume = 1.0f;
        SceneManager.LoadScene(3);
    }

    public void LoadLevelOne()
    {
        Time.timeScale = 1.0f;
        _audioMainCamera.volume = 0.1f;
        _audioSound.volume = 1.0f;
        SceneManager.LoadScene(4);
    }

    public void LoadLevelTwo()
    {
        Time.timeScale = 1.0f;
        _audioMainCamera.volume = 0.1f;
        _audioSound.volume = 1.0f;
        SceneManager.LoadScene(5);
    }

    #endregion

    // чтобы кнопки продолжали анимацию, нужно в Animator выбрать Unscale Time.
}