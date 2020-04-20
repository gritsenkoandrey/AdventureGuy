using UnityEngine;


public class PlatformActivator : MonoBehaviour
{
    // платформа с которой взаимодействуем
    [SerializeField] private MovingPlatformY _platform;

    // нормальное расположение кнопки по координате Y
    [SerializeField] private float _normalButtonPosition;

    // сила с которой жмем на кнопку
    private float _pressedButton = 0.4f;
    private float _averageButtonPosition;

    private int _countSound = 1;

    // задаваемая скорость платформы
    [SerializeField] private float _speedActivationPlatform = 3.0f;
    private bool _isActive = false;

    private void Awake()
    {
        _averageButtonPosition = _normalButtonPosition - _pressedButton;
    }

    private void Update()
    {
        if(_isActive == true)
        {
            _platform.Speed = _speedActivationPlatform;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && transform.position.y > _averageButtonPosition)
        {
            transform.Translate(Vector2.down * _pressedButton);
            
            // чтобы звук активации происходил только 1 раз, если кнопка не поднимается, то это условие можно убрать
            if (_countSound == 1)
            {
                AudioSound._audioSound.AudioPlatform();
                _countSound--;
            }

            _isActive = true;
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    // поднятие кнопки
    //    transform.position = new Vector2(transform.position.x, _normalButtonPosition);
    //}
}