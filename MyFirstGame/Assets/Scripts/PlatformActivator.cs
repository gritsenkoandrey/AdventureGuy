using UnityEngine;


public class PlatformActivator : MonoBehaviour
{
    [SerializeField] private MovingPlatformY _platform;

    //private float _normalButtonPosition = 0.65f;
    private float _pressedButton = 0.4f;
    private float _averageButtonPosition = 0.6f;

    private bool _isActive = false;

    private void Update()
    {
        //if (_isClose == true && _platform.transform.position.y > -1.0f)
        //{
        //    _platform.transform.Translate(Vector2.down * Time.deltaTime);
        //}
        if(_isActive == true)
        {
            _platform.Speed = 3.0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && transform.position.y > _averageButtonPosition)
        {
            transform.Translate(Vector2.down * _pressedButton);
            _isActive = true;
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    // поднятие кнопки
    //    transform.position = new Vector2(transform.position.x, _normalButtonPosition);
    //}
}