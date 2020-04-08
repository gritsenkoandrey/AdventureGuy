using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class Settings : MonoBehaviour
{
    #region Fields

    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Dropdown _dropdown;

    private Resolution[] _resolution;
    private List<string> _resolutionsList;

    private bool _isFullScreen;

    #endregion


    #region UnityMethod

    private void Awake()
    {
        // для изменения разрешения
        _resolutionsList = new List<string>(); // создаем новый список
        _resolution = Screen.resolutions; // получаем разрешения
        foreach(var i in _resolution)
        {
            _resolutionsList.Add(i.width +"x"+ i.height); // пробегаем по массиву из полученных разрешений
        }
        _dropdown.ClearOptions(); // очищаем список dropdown
        _dropdown.AddOptions(_resolutionsList); // записываем разрешение в список
    }

    #endregion


    #region Method
    // метод управления разрешением экрана
    public void FullScreenToggle()
    {
        _isFullScreen = !_isFullScreen;
        Screen.fullScreen = _isFullScreen;
    }

    // метод управления звуком
    public void AudioVolume(float sliderValue)
    {
        _audioMixer.SetFloat("masterVolume", sliderValue);
    }

    // метод управления качеством изображения
    public void Quality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }

    // метод для смены разрешения
    public void Resolution(int r)
    {
        Screen.SetResolution(_resolution[r].width, _resolution[r].height, _isFullScreen);
    }

    #endregion
}