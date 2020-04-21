using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;


public class GamePauseMenu : MonoBehaviour
{
    #region Fields

    private bool _isPaused = false;

    private float _volumeAudioManager = 0.4f;
    private float _volumeMainCamera = 0.1f;

    // задержка звука
    private float _soundDalay = 1.0f;

    [SerializeField] private GameObject _pauseMenuUI;
    [SerializeField] private AudioSource _audioMainCamera;
    [SerializeField] private AudioSource _audioManager;

    #endregion


    #region UnityMethod

    private void Update()
    {
        GamePause();
    }

    #endregion


    #region Method

    public void GamePause()
    {
        // for PC
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

        // for Android
        //if(CrossPlatformInputManager.GetButtonDown("Cancel"))
        //{
        //    if (_isPaused)
        //    {
        //        Resume();
        //    }
        //    else
        //    {
        //        Pause();
        //    }
        //}
    }

    public void Resume()
    {
        _pauseMenuUI.SetActive(false); // во время продолжения игры меню не должно отображаться
        Time.timeScale = 1.0f;
        SoundMainCamera();
        Invoke(nameof(SoundAudioManager), _soundDalay);
        _isPaused = false;
    }

    private void Pause()
    {
        // говорит о том, что это будут кнопки и мы должны с ними как то взаимодействовать
        _pauseMenuUI.SetActive(true); // во время паузы это меню должно появляться
        Time.timeScale = 0f;
        _audioMainCamera.volume = 0f;
        _audioManager.volume = 0f;
        _isPaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1.0f; // для того чтобы время начало двигаться дальше
        SoundMainCamera();
        Invoke(nameof(SoundAudioManager), _soundDalay);
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GiveUp()
    {
        Time.timeScale = 1.0f;
        SoundMainCamera();
        Invoke(nameof(SoundAudioManager), _soundDalay);
        SceneManager.LoadScene(3);
    }

    public void LoadLevelOne()
    {
        Time.timeScale = 1.0f;
        SoundMainCamera();
        Invoke(nameof(SoundAudioManager), _soundDalay);
        SceneManager.LoadScene(4);
    }

    public void LoadLevelTwo()
    {
        Time.timeScale = 1.0f;

        SceneManager.LoadScene(5);
    }

    private void SoundAudioManager()
    {
        _audioManager.volume = _volumeAudioManager;
    }

    private void SoundMainCamera()
    {
        _audioMainCamera.volume = _volumeMainCamera;
    }

    #endregion

    // чтобы кнопки продолжали анимацию, нужно в Animator выбрать Unscale Time.
}